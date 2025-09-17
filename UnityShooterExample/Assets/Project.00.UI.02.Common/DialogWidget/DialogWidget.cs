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
    public class DialogWidget : ViewableWidgetBase2<DialogWidgetView>, IDialogWidget<DialogWidget> {

        public string? Title {
            get => this.View.Title.text;
            set {
                this.View.Title.text = value;
                this.View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => this.View.Message.text;
            set {
                this.View.Message.text = value;
                this.View.Content.SetDisplayed( value != null );
            }
        }

        public DialogWidget(IDependencyProvider provider, string? title, string? message) : base( provider ) {
            this.View = new DialogWidgetView();
            this.Title = title;
            this.Message = message;
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        public DialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }
        public DialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class InfoDialogWidget : ViewableWidgetBase2<InfoDialogWidgetView>, IDialogWidget<InfoDialogWidget> {

        public string? Title {
            get => this.View.Title.text;
            set {
                this.View.Title.text = value;
                this.View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => this.View.Message.text;
            set {
                this.View.Message.text = value;
                this.View.Content.SetDisplayed( value != null );
            }
        }

        public InfoDialogWidget(IDependencyProvider provider, string? title, string? message) : base( provider ) {
            this.View = new InfoDialogWidgetView();
            this.Title = title;
            this.Message = message;
        }
        public override void Dispose() {
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        public InfoDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }
        public InfoDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class WarningDialogWidget : ViewableWidgetBase2<WarningDialogWidgetView>, IDialogWidget<WarningDialogWidget> {

        public string? Title {
            get => this.View.Title.text;
            set {
                this.View.Title.text = value;
                this.View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => this.View.Message.text;
            set {
                this.View.Message.text = value;
                this.View.Content.SetDisplayed( value != null );
            }
        }

        public WarningDialogWidget(IDependencyProvider provider, string? title, string? message) : base( provider ) {
            this.View = new WarningDialogWidgetView();
            this.Title = title;
            this.Message = message;
        }
        public override void Dispose() {
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        public WarningDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }
        public WarningDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }

    }
    public class ErrorDialogWidget : ViewableWidgetBase2<ErrorDialogWidgetView>, IDialogWidget<ErrorDialogWidget> {

        public string? Title {
            get => this.View.Title.text;
            set {
                this.View.Title.text = value;
                this.View.Header.SetDisplayed( value != null );
            }
        }
        public string? Message {
            get => this.View.Message.text;
            set {
                this.View.Message.text = value;
                this.View.Content.SetDisplayed( value != null );
            }
        }

        public ErrorDialogWidget(IDependencyProvider provider, string? title, string? message) : base( provider ) {
            this.View = new ErrorDialogWidgetView();
            this.Title = title;
            this.Message = message;
        }
        public override void Dispose() {
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        public ErrorDialogWidget OnSubmit(string text, Action? callback) {
            var button = VisualElementFactory.Submit( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }
        public ErrorDialogWidget OnCancel(string text, Action? callback) {
            var button = VisualElementFactory.Cancel( text );
            button.RegisterCallback<ClickEvent>( evt => {
                if (button.IsValidSelf()) {
                    callback?.Invoke();
                    if (this.Node.Activity is Activity.Active) this.Node.RemoveSelf( null, (self, arg) => self.Widget().Dispose() );
                }
            } );
            this.View.Footer.Add( button );
            this.View.Footer.SetDisplayed( true );
            return this;
        }

    }
}
