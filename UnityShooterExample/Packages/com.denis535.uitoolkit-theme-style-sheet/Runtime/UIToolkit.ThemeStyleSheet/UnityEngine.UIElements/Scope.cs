#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [UxmlElement]
    public partial class ColumnScope : VisualElement {

        public ColumnScope() {
            AddToClassList( "scope" );
            AddToClassList( "column" );
        }

    }
    [UxmlElement]
    public partial class RowScope : VisualElement {

        public RowScope() {
            AddToClassList( "scope" );
            AddToClassList( "row" );
        }

    }
}
