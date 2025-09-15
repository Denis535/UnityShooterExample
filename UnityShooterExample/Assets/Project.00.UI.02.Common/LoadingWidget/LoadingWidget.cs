#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class LoadingWidget : WidgetBase2<LoadingWidgetView> {

        public LoadingWidget(IDependencyContainer container) : base( container ) {
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
        private static LoadingWidgetView CreateView(LoadingWidget widget) {
            var view = new LoadingWidgetView();
            return view;
        }

    }
}
