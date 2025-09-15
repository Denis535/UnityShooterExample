#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [UxmlElement]
    public partial class Card : VisualElement {

        public Header? Header => this.Children().OfType<Header>().FirstOrDefault();
        public Content? Content => this.Children().OfType<Content>().FirstOrDefault();
        public Footer? Footer => this.Children().OfType<Footer>().FirstOrDefault();

        public Card() {
            this.AddToClassList( "card" );
        }

    }
    [UxmlElement]
    public partial class Header : VisualElement {

        public Header() {
            this.AddToClassList( "header" );
        }

    }
    [UxmlElement]
    public partial class Content : VisualElement {

        public Content() {
            this.AddToClassList( "content" );
        }

    }
    [UxmlElement]
    public partial class Footer : VisualElement {

        public Footer() {
            this.AddToClassList( "footer" );
        }

    }
}
