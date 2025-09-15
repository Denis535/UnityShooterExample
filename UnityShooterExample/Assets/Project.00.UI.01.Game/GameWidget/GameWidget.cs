#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.TreeMachine.Pro;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidget : WidgetBase2<GameWidgetView> {

        private Game2 Game { get; }
        private UIInputProvider Input { get; }
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
                                Node.AddChild( new GameTotalsWidget_GameCompleted( Container ).Node, null );
                            } else {
                                await Awaitable.WaitForSecondsAsync( 2.5f, DisposeCancellationToken );
                                Node.AddChild( new GameTotalsWidget_LevelCompleted( Container ).Node, null );
                            }
                        } else if (Game.Player.State is PlayerState.Loser) {
                            await Awaitable.WaitForSecondsAsync( 2.5f, DisposeCancellationToken );
                            Node.AddChild( new GameTotalsWidget_LevelFailed( Container ).Node, null );
                        } else {
                            throw Exceptions.Internal.NotSupported( $"PlayerState {Game.Player.State} is not supported" );
                        }
                    }
                } catch (OperationCanceledException) {
                }
            };
            View = CreateView( this );
            Input = new UIInputProvider();
            Input.UI.Cancel.performed += ctx => {
                if (View.focusController.focusedElement == null) {
                    View.Focus();
                }
            };
            Node.AddChild( new PlayerWidget( Container ).Node, null );
        }
        public override void Dispose() {
            foreach (var child in Node.Children) {
                child.Widget().Dispose();
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

        protected override void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
            Game.IsPaused = Node.Children.Any( i => i.Widget() is GameMenuWidget );
            IsCursorVisible = Node.Children.Any( i => i.Widget() is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
        }
        //protected override void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
        //}
        //protected override void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
        //}
        protected override void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
            if (Node.Activity is Activity.Active) {
                IsCursorVisible = Node.Children.Where( i => i.Activity is Activity.Active ).Any( i => i.Widget() is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
                Game.IsPaused = Node.Children.Where( i => i.Activity is Activity.Active ).Any( i => i.Widget() is GameMenuWidget );
            }
        }

        protected override void Sort(List<NodeBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a.Widget() ), GetOrderOf( b.Widget() ) ) );
        }
        private static int GetOrderOf(WidgetBase widget) {
            return widget switch {
                PlayerWidget => 0,
                GameTotalsWidget_LevelCompleted => 1,
                GameTotalsWidget_LevelFailed => 1,
                GameTotalsWidget_GameCompleted => 1,
                GameMenuWidget => 2,
                _ => int.MaxValue,
            };
        }

        public void OnUpdate() {
            foreach (var child in Node.Children) {
                if (child.Widget() is PlayerWidget playerWidget) {
                    playerWidget.OnUpdate();
                }
            }
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            view.RegisterCallback<NavigationCancelEvent>( evt => {
                if (!widget.Node.Children.Any( i => i.Widget() is GameMenuWidget )) {
                    widget.Node.AddChild( new GameMenuWidget( widget.Container ).Node, null );
                    evt.StopPropagation();
                }
            } );
            return view;
        }

    }
}
