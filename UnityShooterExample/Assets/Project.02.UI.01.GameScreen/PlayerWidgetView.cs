#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class PlayerWidgetView : WidgetView {

        public VisualElement Target { get; }

        public PlayerWidgetView() : base( "player-widget-view" ) {
            Add(
                Target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
