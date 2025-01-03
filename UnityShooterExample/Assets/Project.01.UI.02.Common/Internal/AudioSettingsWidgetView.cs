﻿#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : WidgetViewBase {

        public Slider MasterVolume { get; }
        public Slider MusicVolume { get; }
        public Slider SfxVolume { get; }
        public Slider GameVolume { get; }

        public AudioSettingsWidgetView() : base( "audio-settings-widget-view" ) {
            Add(
                VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "margin-0px" ).Class( "grow-1" ).Children(
                    MasterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Class( "label-width-25pc" ),
                    MusicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Class( "label-width-25pc" ),
                    SfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Class( "label-width-25pc" ),
                    GameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Class( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
