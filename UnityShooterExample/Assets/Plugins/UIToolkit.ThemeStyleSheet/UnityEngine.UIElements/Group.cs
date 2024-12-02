#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [UxmlElement]
    public partial class ColumnGroup : VisualElement {

        public ColumnGroup() {
            AddToClassList( "group" );
            AddToClassList( "column" );
        }

    }
    [UxmlElement]
    public partial class RowGroup : VisualElement {

        public RowGroup() {
            AddToClassList( "group" );
            AddToClassList( "row" );
        }

    }
}
