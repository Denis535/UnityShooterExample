#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public abstract class GameTotalsWidget<TView> : ViewableWidgetBase2<TView> where TView : notnull, GameTotalsWidgetView {

        protected Router Router { get; }
        protected Game2 Game { get; }

        public GameTotalsWidget(IDependencyProvider provider) : base( provider ) {
            this.Router = provider.RequireDependency<Router>();
            this.Game = provider.RequireDependency<Game2>();
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

    }
    public class GameTotalsWidget_LevelCompleted : GameTotalsWidget<GameTotalsWidgetView_LevelCompleted> {

        public GameTotalsWidget_LevelCompleted(IDependencyProvider provider) : base( provider ) {
            this.View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static GameTotalsWidgetView_LevelCompleted CreateView(GameTotalsWidget_LevelCompleted widget) {
            var view = new GameTotalsWidgetView_LevelCompleted();
            view.Continue.RegisterCallback<ClickEvent>( evt => {
                var gameInfo = widget.Game.Info with {
                    Level = widget.Game.Info.Level.GetNext()
                };
                var playerInfo = widget.Game.Player.Info;
                widget.Router.ReloadGameScene( gameInfo, playerInfo );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.NodeMutable.AddChild( new DialogWidget( widget.Provider, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ).Node, null );
            } );
            return view;
        }

    }
    public class GameTotalsWidget_LevelFailed : GameTotalsWidget<GameTotalsWidgetView_LevelFailed> {

        public GameTotalsWidget_LevelFailed(IDependencyProvider provider) : base( provider ) {
            this.View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static GameTotalsWidgetView_LevelFailed CreateView(GameTotalsWidget_LevelFailed widget) {
            var view = new GameTotalsWidgetView_LevelFailed();
            view.Retry.RegisterCallback<ClickEvent>( evt => {
                var gameInfo = widget.Game.Info;
                var playerInfo = widget.Game.Player.Info;
                widget.Router.ReloadGameScene( gameInfo, playerInfo );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.NodeMutable.AddChild( new DialogWidget( widget.Provider, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ).Node, null );
            } );
            return view;
        }

    }
    public class GameTotalsWidget_GameCompleted : GameTotalsWidget<GameTotalsWidgetView_GameCompleted> {

        public GameTotalsWidget_GameCompleted(IDependencyProvider provider) : base( provider ) {
            this.View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static GameTotalsWidgetView_GameCompleted CreateView(GameTotalsWidget_GameCompleted widget) {
            var view = new GameTotalsWidgetView_GameCompleted();
            view.Okey.RegisterCallback<ClickEvent>( evt => {
                widget.Router.UnloadGameScene();
            } );
            return view;
        }

    }
}
