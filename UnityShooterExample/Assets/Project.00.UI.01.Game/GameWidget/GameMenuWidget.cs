#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class GameMenuWidget : ViewableWidgetBase2<GameMenuWidgetView> {

        private Router Router { get; }

        public GameMenuWidget(IDependencyProvider container) : base( container ) {
            this.Router = container.RequireDependency<Router>();
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
        private static GameMenuWidgetView CreateView(GameMenuWidget widget) {
            var view = new GameMenuWidgetView();
            view.Resume.RegisterCallback<ClickEvent>( evt => {
                widget.NodeMutable.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
            } );
            view.Settings.RegisterCallback<ClickEvent>( evt => {
                widget.NodeMutable.AddChild( new SettingsWidget( widget.Provider ).Node, null );
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.NodeMutable.AddChild( new DialogWidget( widget.Provider, "Confirmation", "Are you sure?" ).OnSubmit( "Yes", () => widget.Router.UnloadGameScene() ).OnCancel( "No", null ).Node, null );
            } );
            return view;
        }

    }
}
