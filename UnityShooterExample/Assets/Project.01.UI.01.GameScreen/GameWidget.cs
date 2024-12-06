#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidget : UIWidgetBase2<GameWidgetView> {

        private Game2 Game { get; }
        private InputActions_UI Input { get; }
        private bool IsCursorVisible {
            get => UnityEngine.Cursor.lockState == CursorLockMode.None;
            set => UnityEngine.Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game2>();
            Game.OnStateChangeEvent += async i => {
                try {
                    if (i is GameState.Completed) {
                        if (Game.Player.State is PlayerState.Winner) {
                            if (Game.Info.Level.IsLast()) {
                                await Awaitable.WaitForSecondsAsync( 2.5f, DisposeCancellationToken );
                                AddChild( new GameTotalsWidget_GameCompleted( Container ) );
                            } else {
                                await Awaitable.WaitForSecondsAsync( 2.5f, DisposeCancellationToken );
                                AddChild( new GameTotalsWidget_LevelCompleted( Container ) );
                            }
                        } else if (Game.Player.State is PlayerState.Loser) {
                            await Awaitable.WaitForSecondsAsync( 2.5f, DisposeCancellationToken );
                            AddChild( new GameTotalsWidget_LevelFailed( Container ) );
                        } else {
                            throw Exceptions.Internal.NotSupported( $"PlayerState {Game.Player.State} is not supported" );
                        }
                    }
                } catch (OperationCanceledException) {
                }
            };
            View = CreateView( this );
            Input = new InputActions_UI();
            Input.UI.Cancel.performed += ctx => {
                if (View.focusController.focusedElement == null) {
                    View.Focus();
                }
            };
            AddChild( new PlayerWidget( Container ) );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            Input.Dispose();
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
            Input.Enable();
            IsCursorVisible = false;
        }
        protected override void OnDeactivate(object? argument) {
            IsCursorVisible = true;
            Input.Disable();
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
            Game.IsPaused = Children.Any( i => i is GameMenuWidget );
            IsCursorVisible = Children.Any( i => i is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
        }
        //protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        //}
        //protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        //}
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
            if (Activity is Activity_.Active) {
                IsCursorVisible = Children.Where( i => i.Activity is Activity_.Active ).Any( i => i is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
                Game.IsPaused = Children.Where( i => i.Activity is Activity_.Active ).Any( i => i is GameMenuWidget );
            }
        }

        protected override void Sort(List<UIWidgetBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }
        private static int GetOrderOf(UIWidgetBase widget) {
            return widget switch {
                PlayerWidget => 0,
                GameTotalsWidget_LevelCompleted => 1,
                GameTotalsWidget_LevelFailed => 1,
                GameTotalsWidget_GameCompleted => 1,
                GameMenuWidget => 2,
                _ => 100,
            };
        }

        public void OnUpdate() {
            foreach (var child in Children) {
                if (child is PlayerWidget playerWidget) {
                    playerWidget.OnUpdate();
                }
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            view.RegisterCallback<NavigationCancelEvent>( evt => {
                if (!widget.Children.Any( i => i is GameMenuWidget )) {
                    widget.AddChild( new GameMenuWidget( widget.Container ) );
                    evt.StopPropagation();
                }
            } );
            return view;
        }

    }
}
