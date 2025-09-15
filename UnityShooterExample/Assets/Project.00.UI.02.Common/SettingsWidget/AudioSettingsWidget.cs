﻿#nullable enable
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
        private Storage.AudioSettings AudioSettings => Application.AudioSettings;

        public AudioSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
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
            if (argument is DeactivateReason.Submit) {
                AudioSettings.MasterVolume = View.MasterVolume.value;
                AudioSettings.MusicVolume = View.MusicVolume.value;
                AudioSettings.SfxVolume = View.SfxVolume.value;
                AudioSettings.GameVolume = View.GameVolume.value;
                AudioSettings.Save();
            } else {
                AudioSettings.Load();
            }
            HideSelf();
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
