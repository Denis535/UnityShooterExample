#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.TreeMachine.Pro;
    using UnityEngine;

    public abstract class WidgetBase : DisposableBase {

        // Node
        public Node2<WidgetBase> Node { get; }
        // Screen
        protected ScreenBase Screen {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                Assert.Operation.Message( $"Widget {this} must be active or activating or deactivating" ).Valid( this.Node.Activity is Activity.Active or Activity.Activating or Activity.Deactivating );
                return ((TreeMachine<Node2<WidgetBase>, ScreenBase>?) this.Node.Machine)!.UserData;
            }
        }

        // View
        [MemberNotNullWhen( true, "ViewBase", "View" )]
        public virtual bool IsViewable {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return false;
            }
        }
        private protected virtual ViewBase? ViewBase {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return null;
            }
        }
        protected internal virtual ViewBase? View {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.ViewBase;
            }
        }

        // Constructor
        public WidgetBase() {
            this.Node = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort
            };
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
            this.Node.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.Node.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.Node.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.Node.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Widget {this} must be inactive" ).Valid( this.Node.Activity is Activity.Inactive );
            Assert.Operation.Message( $"Widget {this} must have no children" ).Valid( this.Node.Children.All( i => i.Widget().IsDisposed ) );
            base.Dispose();
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        // OnDescendantActivate
        protected virtual void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
        }

        // Sort
        protected virtual void Sort(List<NodeBase> children) {
        }

        // ShowView
        protected internal virtual void ShowView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = this.TryShowView( view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected internal virtual void HideView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = this.TryHideView( view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // ShowViewRecursive
        protected internal virtual void ShowViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = this.TryShowViewRecursive( view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected internal virtual void HideViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = this.TryHideViewRecursive( view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // TryShowView
        private bool TryShowView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            if (this.IsViewable && this.View.TryAddView( view )) {
                return true;
            }
            return false;
        }
        private bool TryHideView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            if (this.IsViewable && this.View.TryRemoveView( view )) {
                return true;
            }
            return false;
        }

        // TryShowViewRecursive
        private bool TryShowViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            if (this.IsViewable && this.View.TryAddView( view )) {
                return true;
            }
            return this.Node.Parent?.Widget().TryShowViewRecursive( view ) ?? false;
        }
        private bool TryHideViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            if (this.IsViewable && this.View.TryRemoveView( view )) {
                return true;
            }
            return this.Node.Parent?.Widget().TryHideViewRecursive( view ) ?? false;
        }

    }
    public abstract class WidgetBase<TView> : WidgetBase where TView : notnull, ViewBase {

        private TView view = default!;

        // View
        public sealed override bool IsViewable {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return true;
            }
        }
        private protected sealed override ViewBase? ViewBase {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.View;
            }
        }
        protected internal new TView View {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return view;
            }
            init {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                view = value;
            }
        }

        // Constructor
        public WidgetBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Widget {this} must be inactive" ).Valid( this.Node.Activity is Activity.Inactive );
            Assert.Operation.Message( $"Widget {this} must have no children" ).Valid( this.Node.Children.All( i => i.Widget().IsDisposed ) );
            Assert.Operation.Message( $"Widget {this} must be released" ).Valid( this.View.IsDisposed );
            base.Dispose();
        }

        // ShowSelf
        protected virtual void ShowSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.Node.Parent.Widget().ShowViewRecursive( this.View );
        }
        protected virtual void HideSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.Node.Parent.Widget().HideViewRecursive( this.View );
        }

    }
    public static class NodeExtensions {
        public static WidgetBase Widget(this NodeBase node) {
            return ((Node2<WidgetBase>) node).UserData;
        }
        public static T Widget<T>(this NodeBase node) where T : WidgetBase {
            return (T) ((Node2<WidgetBase>) node).UserData;
        }
    }
}
