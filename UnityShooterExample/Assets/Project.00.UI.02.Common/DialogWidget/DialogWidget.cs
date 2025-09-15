#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.TreeMachine.Pro;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    internal interface IDialogWidget<TThis> where TThis : WidgetBase {
        string? Title { get; set; }
        string? Message { get; set; }
        TThis OnSubmit(string text, Action? callback);
        TThis OnCancel(string text, Action? callback);
    }
    public class DialogWidget : WidgetBase2<DialogWidgetView>, IDialogWidget<DialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public DialogWidget(IDependencyContainer container, string? title, string? message) : base( container ) {
            View = new DialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            foreach (var child in Node.Children) {
                child.Widget().Dispose();
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

        public DialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public DialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class InfoDialogWidget : WidgetBase2<InfoDialogWidgetView>, IDialogWidget<InfoDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public InfoDialogWidget(IDependencyContainer container, string? title, string? message) : base( container ) {
            View = new InfoDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        public InfoDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public InfoDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class WarningDialogWidget : WidgetBase2<WarningDialogWidgetView>, IDialogWidget<WarningDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public WarningDialogWidget(IDependencyContainer container, string? title, string? message) : base( container ) {
            View = new WarningDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        public WarningDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public WarningDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class ErrorDialogWidget : WidgetBase2<ErrorDialogWidgetView>, IDialogWidget<ErrorDialogWidget> {

        public string? Title {
            get => View.Title.text;
            set {
                View.Title.text = value;
                View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => View.Message.text;
            set {
                View.Message.text = value;
                View.Content.SetDisplayed( value != null );
            }
        }

        public ErrorDialogWidget(IDependencyContainer container, string? title, string? message) : base( container ) {
            View = new ErrorDialogWidgetView();
            Title = title;
            Message = message;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        public ErrorDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }
        public ErrorDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (Node.Activity is Activity.Active) Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            View.Footer.Add( button );
            View.Footer.SetDisplayed( true );
            return this;
        }

    }
}
