#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public abstract class IndieViewBase : ViewBase {

        public IndieViewBase(string? name) {
            base.name = name;
            AddToClassList( "indie-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class WidgetViewBase : ViewBase {

        public WidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "widget-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class LeftWidgetViewBase : ViewBase {

        public LeftWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "left-widget-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class SmallWidgetViewBase : ViewBase {

        public SmallWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "small-widget-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class MediumWidgetViewBase : ViewBase {

        public MediumWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "medium-widget-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class LargeWidgetViewBase : ViewBase {

        public LargeWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "large-widget-view" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class DialogWidgetViewBase : ViewBase {

        public DialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayOpenDialog?.Invoke( evt ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class InfoDialogWidgetViewBase : ViewBase {

        public InfoDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "info-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayOpenInfoDialog?.Invoke( evt ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class WarningDialogWidgetViewBase : ViewBase {

        public WarningDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "warning-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayOpenWarningDialog?.Invoke( evt ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public abstract class ErrorDialogWidgetViewBase : ViewBase {

        public ErrorDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "error-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayOpenErrorDialog?.Invoke( evt ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
