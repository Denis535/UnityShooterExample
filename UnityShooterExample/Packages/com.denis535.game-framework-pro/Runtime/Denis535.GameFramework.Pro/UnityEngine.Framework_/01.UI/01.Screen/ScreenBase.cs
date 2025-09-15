#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.TreeMachine.Pro;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class ScreenBase : DisposableBase {

        // Machine
        protected internal TreeMachine<Node2<WidgetBase>, ScreenBase> Machine { get; }

        // Document
        protected internal UIDocument Document { get; }
        // AudioSource
        protected internal AudioSource AudioSource { get; }

        // Constructor
        public ScreenBase(UIDocument document, AudioSource audioSource) {
            Machine = new TreeMachine<Node2<WidgetBase>, ScreenBase>( this );
            Document = document;
            AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Screen {this} must have no {Machine.Root} root" ).Valid( Machine.Root == null );
            Assert.Operation.Message( $"Screen {this} must be released" ).Valid( !Document || Document.rootVisualElement == null || Document.rootVisualElement.childCount == 0 );
            Assert.Operation.Message( $"Screen {this} must be released" ).Valid( !AudioSource || AudioSource.clip == null );
            base.Dispose();
        }

    }
}
