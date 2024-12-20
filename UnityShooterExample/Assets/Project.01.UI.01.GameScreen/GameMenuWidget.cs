#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class GameMenuWidget : WidgetBase2<GameMenuWidgetView> {

        private Router Router { get; }

        public GameMenuWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<Router>();
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
        private static GameMenuWidgetView CreateView(GameMenuWidget widget) {
            var view = new GameMenuWidgetView();
            view.Resume.RegisterCallback<ClickEvent>( evt => {
                widget.RemoveSelf( null );
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new SettingsWidget( widget.Container ) );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ) );
            } );
            return view;
        }

    }
}
