#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainMenuWidget : ViewableWidgetBase2<MainMenuWidgetView> {

        private Router Router { get; }
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => this.Application.ProfileSettings;

        public MainMenuWidget(IDependencyProvider provider) : base( provider ) {
            this.Router = provider.RequireDependency<Router>();
            this.Application = provider.RequireDependency<Application2>();
            this.View = CreateView( this );
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

        // Helpers
        private static MainMenuWidgetView CreateView(MainMenuWidget widget) {
            var view = new MainMenuWidgetView();
            view.RegisterCallbackOnce<AttachToPanelEvent>( evt => {
                widget.ShowView( CreateView_Initial( widget ) );
            } );
            return view;
        }
        private static MainMenuWidgetView_Initial CreateView_Initial(MainMenuWidget widget) {
            var view = new MainMenuWidgetView_Initial();
            view.StartGame.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_StartGame( widget ) );
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.Node.AddChild( new SettingsWidget( widget.Provider ).Node, null );
            } );
            view.Quit.RegisterCallback<ClickEvent>( evt => {
                widget.Node.AddChild( new DialogWidget( widget.Provider, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null ).Node, null );
            } );
            return view;
        }
        private static MainMenuWidgetView_StartGame CreateView_StartGame(MainMenuWidget widget) {
            var view = new MainMenuWidgetView_StartGame();
            view.NewGame.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_SelectLevel( widget ) );
            } );
            view.Continue.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_SelectLevel( widget ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.HideView( view );
                view.Dispose();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectLevel CreateView_SelectLevel(MainMenuWidget widget) {
            var view = new MainMenuWidgetView_SelectLevel();
            view.Level1.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_SelectCharacter( widget, GameInfo.Level_.Level1 ) );
            } );
            view.Level2.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_SelectCharacter( widget, GameInfo.Level_.Level2 ) );
            } );
            view.Level3.RegisterCallback<ClickEvent>( evt => {
                widget.ShowView( CreateView_SelectCharacter( widget, GameInfo.Level_.Level3 ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.HideView( view );
                view.Dispose();
            } );
            return view;
        }
        private static MainMenuWidgetView_SelectCharacter CreateView_SelectCharacter(MainMenuWidget widget, GameInfo.Level_ level) {
            var view = new MainMenuWidgetView_SelectCharacter();
            view.Gray.RegisterCallback<ClickEvent>( evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameInfo.Mode_.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerInfo.CharacterType_.Gray ) );
            } );
            view.Red.RegisterCallback<ClickEvent>( evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameInfo.Mode_.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerInfo.CharacterType_.Red ) );
            } );
            view.Green.RegisterCallback<ClickEvent>( evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameInfo.Mode_.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerInfo.CharacterType_.Green ) );
            } );
            view.Blue.RegisterCallback<ClickEvent>( evt => {
                widget.Router.LoadGameScene( new GameInfo( "Game", GameInfo.Mode_.None, level ), new PlayerInfo( widget.ProfileSettings.Name, PlayerInfo.CharacterType_.Blue ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.HideView( view );
                view.Dispose();
            } );
            return view;
        }

    }
}
