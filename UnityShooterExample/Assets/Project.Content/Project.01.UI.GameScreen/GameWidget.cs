﻿#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Domain.Game_;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        private Game Game { get; }
        private InputActions_UI Input { get; }
        private bool IsCursorVisible {
            get => UnityEngine.Cursor.lockState == CursorLockMode.None;
            set => UnityEngine.Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game>();
            Input = new InputActions_UI();
            Input.UI.Cancel.performed += ctx => {
                if (View.focusController.focusedElement == null) View.Focus();
            };
            View = CreateView( this );
        }
        public override void Dispose() {
            Input.Dispose();
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            Game.OnStateChangeEvent += async state => {
                try {
                    if (state is GameState.Completed) {
                        await Awaitable.WaitForSecondsAsync( 2, DisposeCancellationToken );
                        AddChild( new TotalsWidget( Container ) );
                    }
                } catch (OperationCanceledException) {
                }
            };
            ShowSelf();
            Input.Enable();
            IsCursorVisible = false;
        }
        protected override void OnDeactivate(object? argument) {
            Input.Disable();
            IsCursorVisible = true;
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is MenuWidget );
            IsCursorVisible = Children.Any( i => i is MenuWidget or TotalsWidget );
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (State is State_.Active) {
                Game.IsPaused = Children.Where( i => i.State is State_.Active ).Any( i => i is MenuWidget );
                IsCursorVisible = Children.Where( i => i.State is State_.Active ).Any( i => i is MenuWidget or TotalsWidget );
            }
        }

        protected override void Sort(List<NodeBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( (UIWidgetBase) a ), GetOrderOf( (UIWidgetBase) b ) ) );
        }
        private static int GetOrderOf(UIWidgetBase widget) {
            return widget switch {
                TotalsWidget => 0,
                MenuWidget => 1,
                _ => 2,
            };
        }

        public void OnUpdate() {
            if (Game.Player.Camera != null) {
                View.Target.style.color = GetTargetColor( Game.Player.Camera );
            } else {
                View.Target.style.color = default;
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            view.RegisterCallback<NavigationCancelEvent>( evt => {
                if (!widget.Children.Any( i => i is MenuWidget )) {
                    widget.AddChild( new MenuWidget( widget.Container ) );
                }
            } );
            return view;
        }
        // Helpers
        private static Color GetTargetColor(Camera2 camera) {
            if (camera.Hit?.Thing != null) return Color.yellow;
            if (camera.Hit?.Enemy != null) return Color.red;
            return Color.white;
        }

    }
}
