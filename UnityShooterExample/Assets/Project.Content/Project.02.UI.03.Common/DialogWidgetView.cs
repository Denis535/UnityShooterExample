#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    internal interface IDialogWidgetView {
        Card Card { get; }
        Header Header { get; }
        Content Content { get; }
        Footer Footer { get; }
        Label Title { get; }
        Label Message { get; }
    }
    public class DialogWidgetView : DialogWidgetViewBase, IDialogWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }

        public DialogWidgetView() : base( "dialog-widget-view" ) {
            Add(
                Card = VisualElementFactory.DialogCard().Children(
                    Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                )
            );
            RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static void PlayAnimation(AttachToPanelEvent evt) {
            var target = (VisualElement) evt.target;
            var animation = ValueAnimation<float>.Create( target, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }

    }
    public class InfoDialogWidgetView : InfoDialogWidgetViewBase, IDialogWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }

        public InfoDialogWidgetView() : base( "info-dialog-widget-view" ) {
            Add(
                Card = VisualElementFactory.InfoDialogCard().Children(
                    Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                )
            );
            RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static void PlayAnimation(AttachToPanelEvent evt) {
            var target = (VisualElement) evt.target;
            var animation = ValueAnimation<float>.Create( target, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }

    }
    public class WarningDialogWidgetView : WarningDialogWidgetViewBase, IDialogWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }

        public WarningDialogWidgetView() : base( "warning-dialog-widget-view" ) {
            Add(
                Card = VisualElementFactory.WarningDialogCard().Children(
                    Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                )
            );
            RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static void PlayAnimation(AttachToPanelEvent evt) {
            var target = (VisualElement) evt.target;
            var animation = ValueAnimation<float>.Create( target, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }

    }
    public class ErrorDialogWidgetView : ErrorDialogWidgetViewBase, IDialogWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }

        public ErrorDialogWidgetView() : base( "error-dialog-widget-view" ) {
            Add(
                Card = VisualElementFactory.ErrorDialogCard().Children(
                    Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                        Title = VisualElementFactory.Label( null )
                    ),
                    Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).Children(
                            Message = VisualElementFactory.Label( null )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                )
            );
            RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        private static void PlayAnimation(AttachToPanelEvent evt) {
            var target = (VisualElement) evt.target;
            var animation = ValueAnimation<float>.Create( target, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }

    }
}
