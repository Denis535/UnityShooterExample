#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class AudioSettingsWidget : UIWidgetBase2<AudioSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.AudioSettings AudioSettings => Application.AudioSettings;

        public AudioSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View2 = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            View2.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            if (argument is DeactivateReason.Submit) {
                AudioSettings.MasterVolume = View2.MasterVolume.value;
                AudioSettings.MusicVolume = View2.MusicVolume.value;
                AudioSettings.SfxVolume = View2.SfxVolume.value;
                AudioSettings.GameVolume = View2.GameVolume.value;
                AudioSettings.Save();
            } else {
                AudioSettings.Load();
            }
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
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
