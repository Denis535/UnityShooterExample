#if UNITY_EDITOR
#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor( typeof( ProgramBase ), true )]
    public class ProgramEditor : Editor {

        // Target
        protected ProgramBase Target => (ProgramBase) target;

        // OnInspectorGUI
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            Target.OnInspectorGUI();
        }
        public override bool RequiresConstantRepaint() {
            return EditorApplication.isPlaying;
        }

    }
}
#endif
