#nullable enable
namespace UIToolkit.ThemeStyleSheet.Samples {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class Example : MonoBehaviour {
        private enum Index {
            // None
            None,
            // Widget
            Widget,
            RootWidget,
            LeftWidget,
            SmallWidget,
            MediumWidget,
            LargeWidget,
            // Widget
            DialogWidget,
            InfoDialogWidget,
            WarningDialogWidget,
            ErrorDialogWidget,
            // View
            TabView,
            ScrollView,
            // Scope
            Scope,
            Group,
            Box,
            // Misc
            Misc
        }

        private Index index = 0;

        // Document
        private UIDocument Document { get; set; } = default!;
        // Factory
        private VisualElementFactory Factory { get; set; } = default!;

        // Awake
        public void Awake() {
            Document = GetComponent<UIDocument>();
            Factory = GetComponent<VisualElementFactory>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // OnGUI
        public void OnGUI() {
            var prevIndex = index;
            index = (Index) GUILayout.Toolbar( (int) index, Enum.GetNames( typeof( Index ) ), null, GUI.ToolbarButtonSize.FitToContents );
            // None
            if (index == Index.None && index != prevIndex) {
                Document.rootVisualElement.Clear();
            }
            // Widget
            if (index == Index.Widget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.Widget( Factory ) );
            }
            if (index == Index.RootWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.RootWidget( Factory ) );
            }
            if (index == Index.LeftWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.LeftWidget( Factory ) );
            }
            if (index == Index.SmallWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.SmallWidget( Factory ) );
            }
            if (index == Index.MediumWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.MediumWidget( Factory ) );
            }
            if (index == Index.LargeWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.LargeWidget( Factory ) );
            }
            // Widget
            if (index == Index.DialogWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.DialogWidget( Factory ) );
            }
            if (index == Index.InfoDialogWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.InfoDialogWidget( Factory ) );
            }
            if (index == Index.WarningDialogWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.WarningDialogWidget( Factory ) );
            }
            if (index == Index.ErrorDialogWidget && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.ErrorDialogWidget( Factory ) );
            }
            // TabView
            if (index == Index.TabView && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.TabView( Factory ) );
            }
            // ScrollView
            if (index == Index.ScrollView && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.ScrollView( Factory ) );
            }
            // Scope
            if (index == Index.Scope && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.Scope( Factory ) );
            }
            if (index == Index.Group && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.Group( Factory ) );
            }
            if (index == Index.Box && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.Box( Factory ) );
            }
            // Misc
            if (index == Index.Misc && index != prevIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( VisualElementFactory2.Misc( Factory ) );
            }
        }

    }
}
