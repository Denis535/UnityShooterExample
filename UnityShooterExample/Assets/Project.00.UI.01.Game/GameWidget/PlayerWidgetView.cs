#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class PlayerWidgetView : WidgetViewBase {

        public VisualElement Target { get; }

        public PlayerWidgetView() : base( "player-widget-view" ) {
            this.Add(
                 this.Target = VisualElementFactory.Label( "+" )
                     .Class( "font-size-400pc" ).Class( "color-light" ).Class( "margin-0pc" ).Class( "border-0pc" ).Class( "position-absolute" ).Class( "left-50pc" ).Class( "top-50pc" )
                     .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
             );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
