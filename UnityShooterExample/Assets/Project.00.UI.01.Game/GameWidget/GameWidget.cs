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

    public class GameWidget : ViewableWidgetBase2<GameWidgetView> {

        private Game2 Game { get; }
        private UIInputProvider Input { get; }
        private bool IsCursorVisible {
            get => UnityEngine.Cursor.lockState == CursorLockMode.None;
            set => UnityEngine.Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidget(IDependencyProvider provider) : base( provider ) {
            this.Game = provider.RequireDependency<Game2>();
            this.Game.OnStateChangeEvent += async i => {
                try {
                    if (i is GameState.Completed) {
                        if (this.Game.Player.State is PlayerState.Winner) {
                            if (this.Game.Info.Level.IsLast()) {
                                await Awaitable.WaitForSecondsAsync( 2.5f, this.DisposeCancellationToken );
                                this.NodeMutable.AddChild( new GameTotalsWidget_GameCompleted( this.Provider ).Node, null );
                            } else {
                                await Awaitable.WaitForSecondsAsync( 2.5f, this.DisposeCancellationToken );
                                this.NodeMutable.AddChild( new GameTotalsWidget_LevelCompleted( this.Provider ).Node, null );
                            }
                        } else if (this.Game.Player.State is PlayerState.Loser) {
                            await Awaitable.WaitForSecondsAsync( 2.5f, this.DisposeCancellationToken );
                            this.NodeMutable.AddChild( new GameTotalsWidget_LevelFailed( this.Provider ).Node, null );
                        } else {
                            throw Exceptions.Internal.NotSupported( $"PlayerState {this.Game.Player.State} is not supported" );
                        }
                    }
                } catch (OperationCanceledException) {
                }
            };
            this.View = CreateView( this );
            this.Input = new UIInputProvider();
            this.Input.UI.Cancel.performed += ctx => {
                if (this.View.focusController.focusedElement == null) {
                    this.View.Focus();
                }
            };
            this.NodeMutable.AddChild( new PlayerWidget( this.Provider ).Node, null );
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.Input.Dispose();
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
            this.Input.Enable();
            this.IsCursorVisible = false;
        }
        protected override void OnDeactivate(object? argument) {
            this.IsCursorVisible = true;
            this.Input.Disable();
            this.HideSelf();
        }

        protected override void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
            this.Game.IsPaused = this.Node.Children.Any( i => i.Widget() is GameMenuWidget );
            this.IsCursorVisible = this.Node.Children.Any( i => i.Widget() is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
        }
        //protected override void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
        //}
        //protected override void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
        //}
        protected override void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
            if (this.Node.Activity is Activity.Active) {
                this.IsCursorVisible = this.Node.Children.Where( i => i.Activity is Activity.Active ).Any( i => i.Widget() is GameMenuWidget or GameTotalsWidget_LevelCompleted or GameTotalsWidget_LevelFailed or GameTotalsWidget_GameCompleted );
                this.Game.IsPaused = this.Node.Children.Where( i => i.Activity is Activity.Active ).Any( i => i.Widget() is GameMenuWidget );
            }
        }

        protected override void Sort(List<INode> children) {
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
            foreach (var child in this.Node.Children) {
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
                    widget.NodeMutable.AddChild( new GameMenuWidget( widget.Provider ).Node, null );
                    evt.StopPropagation();
                }
            } );
            return view;
        }

    }
}
