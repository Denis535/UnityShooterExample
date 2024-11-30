#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [UxmlElement]
    public partial class Card : VisualElement {
        //[Preserve]
        //public new class UxmlFactory : UxmlFactory<Card, UxmlTraits> { }

        public Header? Header => Children().OfType<Header>().FirstOrDefault();
        public Content? Content => Children().OfType<Content>().FirstOrDefault();
        public Footer? Footer => Children().OfType<Footer>().FirstOrDefault();

        public Card() {
            AddToClassList( "card" );
        }

    }
    // Header
    [UxmlElement]
    public partial class Header : VisualElement {
        //[Preserve]
        //public new class UxmlFactory : UxmlFactory<Header, UxmlTraits> { }

        public Header() {
            AddToClassList( "header" );
        }

    }
    // Content
    [UxmlElement]
    public partial class Content : VisualElement {
        //[Preserve]
        //public new class UxmlFactory : UxmlFactory<Content, UxmlTraits> { }

        public Content() {
            AddToClassList( "content" );
        }

    }
    // Footer
    [UxmlElement]
    public partial class Footer : VisualElement {
        //[Preserve]
        //public new class UxmlFactory : UxmlFactory<Footer, UxmlTraits> { }

        public Footer() {
            AddToClassList( "footer" );
        }

    }
}
