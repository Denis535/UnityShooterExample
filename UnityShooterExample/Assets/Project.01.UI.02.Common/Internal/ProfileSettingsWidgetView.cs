#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : WidgetView {

        public TextField Name { get; }

        public ProfileSettingsWidgetView(Func<string?, bool> nameValidator) : base( "profile-settings-widget-view" ) {
            Add(
                VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "margin-0px" ).Class( "grow-1" ).Children(
                    Name = VisualElementFactory.TextField( "Name", 16 ).Class( "label-width-25pc" )
                )
            );
            Name.OnValidate( evt => {
                Name.SetValid( nameValidator( Name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
