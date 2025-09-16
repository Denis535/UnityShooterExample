#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class WidgetBase2 : WidgetBase {

        // System
        protected IDependencyContainer Container { get; }

        // Constructor
        public WidgetBase2(IDependencyContainer container) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ViewableWidgetBase2<TView> : ViewableWidgetBase<TView> where TView : notnull, ViewBase {

        // System
        protected IDependencyContainer Container { get; }

        // Constructor
        public ViewableWidgetBase2(IDependencyContainer container) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // ShowSelf
        protected virtual void ShowSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.ShowViewRecursive( this.View );
        }
        protected virtual void HideSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.HideViewRecursive( this.View );
        }

        // ShowView
        protected virtual void ShowView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = TryShowView( this, view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected virtual void HideView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = TryHideView( this, view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // ShowViewRecursive
        protected virtual void ShowViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = TryShowViewRecursive( this, view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected virtual void HideViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = TryHideViewRecursive( this, view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // Helpers
        private static bool TryShowView(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryAddView( view )) {
                return true;
            }
            return false;
        }
        private static bool TryHideView(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryRemoveView( view )) {
                return true;
            }
            return false;
        }
        // Helpers
        private static bool TryShowViewRecursive(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryAddView( view )) {
                return true;
            }
            if (widget.Node.Parent != null) {
                return TryShowViewRecursive( widget.Node.Parent.Widget(), view );
            }
            return false;
        }
        private static bool TryHideViewRecursive(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryRemoveView( view )) {
                return true;
            }
            if (widget.Node.Parent != null) {
                return TryHideViewRecursive( widget.Node.Parent.Widget(), view );
            }
            return false;
        }

    }
}
