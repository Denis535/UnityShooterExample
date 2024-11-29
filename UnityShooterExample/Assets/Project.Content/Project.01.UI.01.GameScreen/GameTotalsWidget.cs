#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class GameTotalsWidget : UIWidgetBase2<GameTotalsWidgetView> {

        private UIRouter Router { get; }
        private Game2 Game { get; }

        public GameTotalsWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Game = container.RequireDependency<Game2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // Helpers
        private static GameTotalsWidgetView CreateView(GameTotalsWidget widget) {
            if (widget.Game.Player.State is PlayerState.Winner) {
                if (!widget.Game.Info.Level.IsLast()) {
                    var view = new GameTotalsWidgetView_LevelCompleted();
                    view.Continue.RegisterCallback<ClickEvent>( evt => {
                        var gameInfo = widget.Game.Info;
                        gameInfo = gameInfo with { Level = gameInfo.Level.GetNext() };
                        var playerInfo = widget.Game.Player.Info;
                        widget.Router.ReloadGameScene( gameInfo, playerInfo );
                    } );
                    view.Back.RegisterCallback<ClickEvent>( evt => {
                        widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                    } );
                    return view;
                } else {
                    var view = new GameTotalsWidgetView_GameCompleted();
                    view.Okey.RegisterCallback<ClickEvent>( evt => {
                        widget.Router.UnloadGameScene();
                    } );
                    return view;
                }
            }
            if (widget.Game.Player.State is PlayerState.Loser) {
                var view = new GameTotalsWidgetView_LevelFailed();
                view.Retry.RegisterCallback<ClickEvent>( evt => {
                    var gameInfo = widget.Game.Info;
                    var playerInfo = widget.Game.Player.Info;
                    widget.Router.ReloadGameScene( gameInfo, playerInfo );
                } );
                view.Back.RegisterCallback<ClickEvent>( evt => {
                    widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
                } );
                return view;
            }
            throw Exceptions.Internal.NotSupported( $"PlayerState {widget.Game.Player.State} is not supported" );
        }

    }
}
