﻿#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainMenuWidget : UIWidgetBase2<MainMenuWidgetView> {

        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;

        public MainMenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            View2 = CreateView( this );
        }
        public override void Dispose() {
            View2.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static MainMenuWidgetView CreateView(MainMenuWidget widget) {
            var view = new MainMenuWidgetView();
            view.AddViewRecursive( CreateView_Initial( widget ) );
            return view;
        }
        private static MenuWidgetView_Initial CreateView_Initial(MainMenuWidget widget) {
            var view = new MenuWidgetView_Initial();
            view.StartGame.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_StartGame( widget ) );
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.Quit.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.Quit() ).OnCancel( "No", null ) );
            } );
            return view;
        }
        private static MenuWidgetView_StartGame CreateView_StartGame(MainMenuWidget widget) {
            var view = new MenuWidgetView_StartGame();
            view.NewGame.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_SelectLevel( widget ) );
            } );
            view.Continue.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_SelectLevel( widget ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                view.RemoveViewRecursive( view );
            } );
            return view;
        }
        private static MenuWidgetView_SelectLevel CreateView_SelectLevel(MainMenuWidget widget) {
            var view = new MenuWidgetView_SelectLevel();
            view.Level1.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_SelectCharacter( widget, GameInfo.Level_.Level1 ) );
            } );
            view.Level2.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_SelectCharacter( widget, GameInfo.Level_.Level2 ) );
            } );
            view.Level3.RegisterCallback<ClickEvent>( evt => {
                view.AddViewRecursive( CreateView_SelectCharacter( widget, GameInfo.Level_.Level3 ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                view.RemoveViewRecursive( view );
            } );
            return view;
        }
        private static MenuWidgetView_SelectCharacter CreateView_SelectCharacter(MainMenuWidget widget, GameInfo.Level_ level) {
            var view = new MenuWidgetView_SelectCharacter();
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
                view.RemoveViewRecursive( view );
            } );
            return view;
        }

    }
}
