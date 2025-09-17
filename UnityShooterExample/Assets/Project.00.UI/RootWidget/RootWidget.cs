#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.TreeMachine.Pro;
    using UnityEngine;
    using UnityEngine.Framework;

    public class RootWidget : RootWidgetBase<RootWidgetView> {

        public RootWidget(IDependencyProvider provider) : base( provider ) {
            this.View = new RootWidgetView();
            this.View.OnSubmitEvent += OnSubmit;
            this.View.OnCancelEvent += OnCancel;
        }
        public override void Dispose() {
            this.View.Dispose();
            base.Dispose();
        }

        internal void AddChild(MainWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void AddChild(GameWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void AddChild(LoadingWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void AddChild(UnloadingWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void AddChild(WarningDialogWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void AddChild(ErrorDialogWidget widget) {
            this.Node.AddChild( widget.Node, null );
        }
        internal void Clear() {
            _ = this.Node.RemoveChildren( i => i.Widget() is not (DialogWidget or InfoDialogWidget or WarningDialogWidget or ErrorDialogWidget), null, (node, arg) => node.Widget().Dispose() );
        }

        protected override void Sort(List<NodeBase> children) {
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
            }
            ;
        }

    }
}
