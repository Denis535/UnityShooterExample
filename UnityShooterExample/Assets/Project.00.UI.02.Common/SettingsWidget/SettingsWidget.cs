#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class SettingsWidget : ViewableWidgetBase2<SettingsWidgetView> {

        public SettingsWidget(IDependencyProvider provider) : base( provider ) {
            this.View = CreateView( this );
            this.Node.AddChild( new ProfileSettingsWidget( provider ).Node, null );
            this.Node.AddChild( new VideoSettingsWidget( provider ).Node, null );
            this.Node.AddChild( new AudioSettingsWidget( provider ).Node, null );
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
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
            view.Okey.RegisterCallback<ClickEvent>( evt => {
                if (evt.GetTarget().IsValidSelf()) {
                    widget.Node.RemoveSelf( DeactivateReason.Submit, (self, arg) => self.Widget().Dispose() );
                }
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.Node.RemoveSelf( DeactivateReason.Cancel, (self, arg) => self.Widget().Dispose() );
            } );
            return view;
        }

    }
}
