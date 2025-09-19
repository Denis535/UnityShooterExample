#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            foreach (var child in this.Node.Children.Select( i => i.Widget() )) {
                if (child is MainWidget mainWidget) {
                    mainWidget.OnUpdate();
                } else if (child is GameWidget gameWidget) {
                    gameWidget.OnUpdate();
                }
            }
        }

        internal void ShowMainWidget(MainWidget widget) {
            this.Clear();
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowGameWidget(GameWidget widget) {
            this.Clear();
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowLoadingWidget(LoadingWidget widget) {
            this.Clear();
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowUnloadingWidget(UnloadingWidget widget) {
            this.Clear();
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowInfoDialogWidget(InfoDialogWidget widget) {
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowWarningDialogWidget(WarningDialogWidget widget) {
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void ShowErrorDialogWidget(ErrorDialogWidget widget) {
            this.NodeMutable.AddChild( widget.Node, null );
        }
        internal void Clear() {
            _ = this.NodeMutable.RemoveChildren( i => i.Widget() is MainWidget or GameWidget or LoadingWidget or UnloadingWidget, null, (node, arg) => node.Widget().Dispose() );
        }

        protected override void Sort(List<INode> children) {
            base.Sort( children );
        }
        protected override int GetOrderOf(WidgetBase widget) {
            return widget switch {
                // MainScreen
                MainWidget => 0,
                // GameScreen
                GameWidget => 0,
                // Common
                LoadingWidget => 0,
                UnloadingWidget => 0,
                _ => int.MaxValue
            };
        }

    }
}
