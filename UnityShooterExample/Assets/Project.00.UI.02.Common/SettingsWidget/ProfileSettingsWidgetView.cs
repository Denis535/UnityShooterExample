#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : WidgetViewBase {

        public TextField Name { get; }

        public ProfileSettingsWidgetView(Func<string?, bool> nameValidator) : base( "profile-settings-widget-view" ) {
            this.Add(
                VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "margin-0px" ).Class( "grow-1" ).Children(
                    this.Name = VisualElementFactory.TextField( "Name", 16 ).Class( "label-width-25pc" )
                )
            );
            this.Name.OnValidate( evt => {
                this.Name.SetValid( nameValidator( this.Name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
