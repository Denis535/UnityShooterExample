#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [UxmlElement]
    public partial class ColumnGroup : VisualElement {

        public ColumnGroup() {
            this.AddToClassList( "group" );
            this.AddToClassList( "column" );
        }

    }
    [UxmlElement]
    public partial class RowGroup : VisualElement {

        public RowGroup() {
            this.AddToClassList( "group" );
            this.AddToClassList( "row" );
        }

    }
}
