#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class VideoSettingsWidgetView : WidgetViewBase {

        public Toggle IsFullScreen { get; }
        public PopupField<object?> ScreenResolution { get; }
        public Toggle IsVSync { get; }

        public VideoSettingsWidgetView() : base( "video-settings-widget-view" ) {
            Add(
                VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "margin-0px" ).Class( "grow-1" ).Children(
                    IsFullScreen = VisualElementFactory.ToggleField( "Full Screen" ).Class( "label-width-25pc" ),
                    ScreenResolution = VisualElementFactory.PopupField( "Screen Resolution" ).Class( "label-width-25pc" ),
                    IsVSync = VisualElementFactory.ToggleField( "V-Sync" ).Class( "label-width-25pc" )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
