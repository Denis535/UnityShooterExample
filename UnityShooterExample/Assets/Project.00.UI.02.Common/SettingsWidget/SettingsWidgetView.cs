#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : MediumWidgetViewBase {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public TabView TabView { get; }
        public Tab ProfileSettingsTab { get; }
        public Tab VideoSettingsTab { get; }
        public Tab AudioSettingsTab { get; }
        public Footer Footer { get; }
        public Button Okey { get; }
        public Button Back { get; }

        public SettingsWidgetView() : base( "settings-widget-view" ) {
            this.Add(
                  this.Card = VisualElementFactory.Card().Children(
                      this.Header = VisualElementFactory.Header().Children(
                         this.Title = VisualElementFactory.Label( "Settings" )
                       ),
                      this.Content = VisualElementFactory.Content().Children(
                          this.TabView = VisualElementFactory.TabView().Class( "no-outline" ).Class( "grow-1" ).Children(
                              this.ProfileSettingsTab = VisualElementFactory.Tab( "Profile Settings" ),
                              this.VideoSettingsTab = VisualElementFactory.Tab( "Video Settings" ),
                              this.AudioSettingsTab = VisualElementFactory.Tab( "Audio Settings" )
                           )
                       ),
                      this.Footer = VisualElementFactory.Footer().Children(
                         this.Okey = VisualElementFactory.Submit( "Ok" ),
                          this.Back = VisualElementFactory.Cancel( "Back" )
                       )
                   )
               );
            this.OnValidate( evt => {
                this.Okey.SetValid(
                     this.ProfileSettingsTab.Descendants().All( i => i.IsValidSelf() ) &&
                     this.VideoSettingsTab.Descendants().All( i => i.IsValidSelf() ) &&
                     this.AudioSettingsTab.Descendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override bool TryAddView(ViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                this.ProfileSettingsTab.Add( profileSettings );
                return true;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                this.VideoSettingsTab.Add( videoSettings );
                return true;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                this.AudioSettingsTab.Add( audioSettings );
                return true;
            }
            return false;
        }
        protected override bool TryRemoveView(ViewBase view) {
            if (view is ProfileSettingsWidgetView) {
                this.ProfileSettingsTab.Clear();
                return true;
            }
            if (view is VideoSettingsWidgetView) {
                this.VideoSettingsTab.Clear();
                return true;
            }
            if (view is AudioSettingsWidgetView) {
                this.AudioSettingsTab.Clear();
                return true;
            }
            return false;
        }

    }
}
