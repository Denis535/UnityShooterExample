#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class RootWidget : RootWidgetBase<RootWidgetView> {

        public RootWidget(IDependencyContainer container) : base( container ) {
            View = new RootWidgetView();
            View.OnSubmitEvent += OnSubmit;
            View.OnCancelEvent += OnCancel;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        internal void AddChild(MainWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(GameWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(LoadingWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(UnloadingWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(WarningDialogWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(ErrorDialogWidget widget) {
            base.AddChild( widget, null );
        }
        internal void Clear() {
            RemoveChildren( i => i is not (DialogWidget or InfoDialogWidget or WarningDialogWidget or ErrorDialogWidget), null );
        }

        protected override void Sort(List<WidgetBase> children) {
            base.Sort( children );
        }
        protected override int GetOrderOf(WidgetBase widget) {
            return widget switch {
                // MainScreen
                MainWidget => 0,
                // GameScreen
                GameWidget => 100,
                // Common
                LoadingWidget => 200,
                UnloadingWidget => 201,
                _ => int.MaxValue
            };
        }

    }
}
