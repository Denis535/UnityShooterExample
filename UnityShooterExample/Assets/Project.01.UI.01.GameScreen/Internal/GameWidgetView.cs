#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class GameWidgetView : WidgetViewBase {

        public GameWidgetView() : base( "game-widget-view" ) {
            focusable = true;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
