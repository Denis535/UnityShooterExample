#nullable enable
namespace Project.UI {
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
            this.Add(
               this.Card = VisualElementFactory.DialogCard().Children(
                   this.Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                      this.Title = VisualElementFactory.Label( null )
                    ),
                   this.Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                        VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                          this.Message = VisualElementFactory.Label( null )
                        )
                    ),
                   this.Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                )
            );
            this.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
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
            this.Add(
               this.Card = VisualElementFactory.InfoDialogCard().Children(
                  this.Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                      this.Title = VisualElementFactory.Label( null )
                     ),
                   this.Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                         VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                           this.Message = VisualElementFactory.Label( null )
                         )
                     ),
                    this.Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                 )
             );
            this.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
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
            this.Add(
                this.Card = VisualElementFactory.WarningDialogCard().Children(
                   this.Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                        this.Title = VisualElementFactory.Label( null )
                     ),
                    this.Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                         VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                           this.Message = VisualElementFactory.Label( null )
                         )
                     ),
                   this.Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                 )
             );
            this.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
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
            this.Add(
                this.Card = VisualElementFactory.ErrorDialogCard().Children(
                    this.Header = VisualElementFactory.Header().Chain( i => i.SetDisplayed( false ) ).Children(
                         this.Title = VisualElementFactory.Label( null )
                      ),
                   this.Content = VisualElementFactory.Content().Chain( i => i.SetDisplayed( false ) ).Children(
                          VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                            this.Message = VisualElementFactory.Label( null )
                          )
                      ),
                   this.Footer = VisualElementFactory.Footer().Chain( i => i.SetDisplayed( false ) )
                  )
              );
            this.RegisterCallbackOnce<AttachToPanelEvent>( PlayAnimation );
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
