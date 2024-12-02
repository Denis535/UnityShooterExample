#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [UxmlElement]
    public partial class Card : VisualElement {

        public Header? Header => Children().OfType<Header>().FirstOrDefault();
        public Content? Content => Children().OfType<Content>().FirstOrDefault();
        public Footer? Footer => Children().OfType<Footer>().FirstOrDefault();

        public Card() {
            AddToClassList( "card" );
        }

    }
    [UxmlElement]
    public partial class Header : VisualElement {

        public Header() {
            AddToClassList( "header" );
        }

    }
    [UxmlElement]
    public partial class Content : VisualElement {

        public Content() {
            AddToClassList( "content" );
        }

    }
    [UxmlElement]
    public partial class Footer : VisualElement {

        public Footer() {
            AddToClassList( "footer" );
        }

    }
}
