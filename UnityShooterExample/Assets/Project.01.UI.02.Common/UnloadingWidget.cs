#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class UnloadingWidget : UIWidgetBase2<UnloadingWidgetView> {

        public UnloadingWidget(IDependencyContainer container) : base( container ) {
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
        private static UnloadingWidgetView CreateView(UnloadingWidget widget) {
            var view = new UnloadingWidgetView();
            return view;
        }

    }
}
