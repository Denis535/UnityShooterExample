#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class VideoSettingsWidget : ViewableWidgetBase2<VideoSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.VideoSettings VideoSettings => this.Application.VideoSettings;

        public VideoSettingsWidget(IDependencyProvider provider) : base( provider ) {
            this.Application = provider.RequireDependency<Application2>();
            this.View = CreateView( this );
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
            if (argument is DeactivateReason.Submit) {
                this.VideoSettings.IsFullScreen = this.View.IsFullScreen.value;
                this.VideoSettings.ScreenResolution = (Resolution) this.View.ScreenResolution.value!;
                this.VideoSettings.IsVSync = this.View.IsVSync.value;
                this.VideoSettings.Save();
            } else {
                this.VideoSettings.Load();
            }
            this.HideSelf();
        }

        // Helpers
        private static VideoSettingsWidgetView CreateView(VideoSettingsWidget widget) {
            var view = new VideoSettingsWidgetView();
            view.IsFullScreen.SetValue( widget.VideoSettings.IsFullScreen );
            view.ScreenResolution.SetValue( widget.VideoSettings.ScreenResolution, widget.VideoSettings.ScreenResolutions.Cast<object?>().ToList() );
            view.IsVSync.SetValue( widget.VideoSettings.IsVSync );
            view.IsFullScreen.RegisterCallback<ChangeEvent<bool>>( evt => {
                widget.VideoSettings.IsFullScreen = evt.newValue;
            } );
            view.ScreenResolution.RegisterCallback<ChangeEvent<object?>>( evt => {
                widget.VideoSettings.ScreenResolution = (Resolution) evt.newValue!;
            } );
            view.IsVSync.RegisterCallback<ChangeEvent<bool>>( evt => {
                widget.VideoSettings.IsVSync = evt.newValue;
            } );
            return view;
        }

    }
}
