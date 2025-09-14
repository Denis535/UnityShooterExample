#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;

    public class AboutPackageWindow : EditorWindow {

        // Constructor
        public AboutPackageWindow() {
            titleContent = new GUIContent( "About Game Framework Pro package" );
            minSize = maxSize = new Vector2( 1200, 800 );
        }

        // OnEnable
        public void OnEnable() {
            ShowUtility();
            Focus();
        }
        public void OnDisable() {
        }

        // OnGUI
        public void OnGUI() {
            HelpBox.Draw();
        }

    }
}
