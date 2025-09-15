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
            foreach (var child in Node.Children) {
                child.Widget().Dispose();
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
                widget.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.Node.AddChild( new SettingsWidget( widget.Container ).Node, null );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.Node.AddChild( new DialogWidget( widget.Container, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ).Node, null );
            } );
            return view;
        }

    }
}
