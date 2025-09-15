#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class UnloadingWidget : ViewableWidgetBase2<UnloadingWidgetView> {

        public UnloadingWidget(IDependencyContainer container) : base( container ) {
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
        private static UnloadingWidgetView CreateView(UnloadingWidget widget) {
            var view = new UnloadingWidgetView();
            return view;
        }

    }
}
