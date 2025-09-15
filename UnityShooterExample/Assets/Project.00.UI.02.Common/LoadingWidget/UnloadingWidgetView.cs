#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UnloadingWidgetView : WidgetViewBase {

        public VisualElement Background { get; }

        public UnloadingWidgetView() : base( "unloading-widget-view" ) {
            this.Add(
             this.Background = VisualElementFactory.VisualElement().Class( "unloading-widget-view-background" ).Class( "width-100pc" ).Class( "height-100pc" )
            );
            this.Background.RegisterCallbackOnce<AttachToPanelEvent>( async evt => {
                this.Background.style.unityBackgroundImageTintColor = Color.gray;
                this.Background.style.translate = new Translate( 0, 0 );
                this.Background.style.rotate = new Rotate( Angle.Degrees( 15 ) );
                this.Background.style.scale = new Scale( new Vector3( 2, 2, 1 ) );
                await Awaitable.NextFrameAsync( this.DisposeCancellationToken );
                this.Background.style.unityBackgroundImageTintColor = Color.white;
                this.Background.style.translate = new Translate( 0, 0 );
                this.Background.style.rotate = new Rotate( Angle.Degrees( 0 ) );
                this.Background.style.scale = new Scale( new Vector3( 1, 1, 1 ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
