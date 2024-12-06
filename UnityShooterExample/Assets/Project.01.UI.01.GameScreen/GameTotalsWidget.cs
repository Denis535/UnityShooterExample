#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public abstract class GameTotalsWidget<TView> : UIWidgetBase2<TView> where TView : notnull, GameTotalsWidgetView {

        protected UIRouter Router { get; }
        protected Game2 Game { get; }

        public GameTotalsWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Game = container.RequireDependency<Game2>();
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

    }
    public class GameTotalsWidget_LevelCompleted : GameTotalsWidget<GameTotalsWidgetView_LevelCompleted> {

        public GameTotalsWidget_LevelCompleted(IDependencyContainer container) : base( container ) {
            View = CreateView( this );
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
                widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
            } );
            return view;
        }

    }
    public class GameTotalsWidget_LevelFailed : GameTotalsWidget<GameTotalsWidgetView_LevelFailed> {

        public GameTotalsWidget_LevelFailed(IDependencyContainer container) : base( container ) {
            View = CreateView( this );
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
                widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
            } );
            return view;
        }

    }
    public class GameTotalsWidget_GameCompleted : GameTotalsWidget<GameTotalsWidgetView_GameCompleted> {

        public GameTotalsWidget_GameCompleted(IDependencyContainer container) : base( container ) {
            View = CreateView( this );
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
