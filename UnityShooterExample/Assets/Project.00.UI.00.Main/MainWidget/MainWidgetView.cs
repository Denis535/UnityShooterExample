#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainWidgetView : WidgetViewBase {

        public MainWidgetView() : base( "main-widget-view" ) {
            this.AddToClassList( "main-widget-view-background" );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
