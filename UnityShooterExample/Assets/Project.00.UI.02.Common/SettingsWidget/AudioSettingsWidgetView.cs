#nullable enable
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
            this.Add(
                  VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "margin-0px" ).Class( "grow-1" ).Children(
                     this.MasterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Class( "label-width-25pc" ),
                     this.MusicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Class( "label-width-25pc" ),
                     this.SfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Class( "label-width-25pc" ),
                    this.GameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Class( "label-width-25pc" )
                  )
              );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
