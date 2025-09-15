#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class AudioSettingsWidget : WidgetBase2<AudioSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.AudioSettings AudioSettings => this.Application.AudioSettings;

        public AudioSettingsWidget(IDependencyContainer container) : base( container ) {
            this.Application = container.RequireDependency<Application2>();
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
                this.AudioSettings.MasterVolume = this.View.MasterVolume.value;
                this.AudioSettings.MusicVolume = this.View.MusicVolume.value;
                this.AudioSettings.SfxVolume = this.View.SfxVolume.value;
                this.AudioSettings.GameVolume = this.View.GameVolume.value;
                this.AudioSettings.Save();
            } else {
                this.AudioSettings.Load();
            }
            this.HideSelf();
        }

        // Helpers
        private static AudioSettingsWidgetView CreateView(AudioSettingsWidget widget) {
            var view = new AudioSettingsWidgetView();
            view.MasterVolume.SetValue( widget.AudioSettings.MasterVolume, 0, 1 );
            view.MusicVolume.SetValue( widget.AudioSettings.MusicVolume, 0, 1 );
            view.SfxVolume.SetValue( widget.AudioSettings.SfxVolume, 0, 1 );
            view.GameVolume.SetValue( widget.AudioSettings.GameVolume, 0, 1 );
            view.MasterVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.MasterVolume = evt.newValue;
            } );
            view.MusicVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.MusicVolume = evt.newValue;
            } );
            view.SfxVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.SfxVolume = evt.newValue;
            } );
            view.GameVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.GameVolume = evt.newValue;
            } );
            return view;
        }

    }
}
