#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class GameMenuWidgetView : LeftWidgetViewBase {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Button Resume { get; }
        public Button Settings { get; }
        public Button Back { get; }

        public GameMenuWidgetView() : base( "game-menu-widget-view" ) {
            this.Add(
               this.Card = VisualElementFactory.Card().Children(
                   this.Header = VisualElementFactory.Header().Children(
                        this.Title = VisualElementFactory.Label( "Menu" )
                     ),
                   this.Content = VisualElementFactory.Content().Children(
                       this.Resume = VisualElementFactory.Resume( "Resume" ),
                       this.Settings = VisualElementFactory.Select( "Settings" ),
                       this.Back = VisualElementFactory.Back( "Back To Menu" )
                     )
                 )
             );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
