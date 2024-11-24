#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class UnloadingWidget : UIWidgetBase2<UnloadingWidgetView> {

        public UnloadingWidget(IDependencyContainer container) : base( container ) {
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
        private static UnloadingWidgetView CreateView(UnloadingWidget widget) {
            var view = new UnloadingWidgetView();
            return view;
        }

    }
}
