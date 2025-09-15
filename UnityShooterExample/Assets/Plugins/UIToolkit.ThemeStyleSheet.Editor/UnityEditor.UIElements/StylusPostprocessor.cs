#nullable enable
namespace UnityEditor.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class StylusPostprocessor : AssetPostprocessor {

        // CreateAsset
        [MenuItem( "Assets/Create/UI Toolkit/Stylus" )]
        public static void CreateAsset() {
            ProjectWindowUtil.CreateAssetWithContent( "New Stylus.styl", "" );
        }

        // OnPostprocessAllAssets
        public static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom) {
            foreach (var imported_ in imported) {
                OnAssetImported( imported_ );
            }
            foreach (var deleted_ in deleted) {
                OnAssetDeleted( deleted_ );
            }
            foreach (var (moved_, movedFrom_) in moved.Zip( movedFrom, (a, b) => (a, b) )) {
                OnAssetMoved( moved_, movedFrom_ );
            }
        }
        private static void OnAssetImported(string path) {
            if (IsStylus( path ) && IsSupported( path )) {
                CompileStylus( path, Path.ChangeExtension( path, ".uss" ) );
                AssetDatabase.ImportAsset( Path.ChangeExtension( path, ".uss" ) );
            }
        }
        private static void OnAssetDeleted(string path) {
            if (IsStylus( path )) {
                _ = AssetDatabase.DeleteAsset( Path.ChangeExtension( path, ".uss" ) );
            }
        }
        private static void OnAssetMoved(string newPath, string oldPath) {
            if (IsStylus( oldPath )) {
                _ = AssetDatabase.MoveAsset( Path.ChangeExtension( oldPath, ".uss" ), Path.ChangeExtension( newPath, ".uss" ) );
            }
        }

        // Helpers
        private static void CompileStylus(string src, string dist) {
            try {
                NodeJS.Run( "Assets/Plugins/UIToolkit.ThemeStyleSheet.Editor/StylusCompiler.js", src, dist );
            } catch (Win32Exception) {
                Debug.LogWarning( $"Can not compile '{src}', probably the Node.js is not installed" );
            }
        }
        private static bool IsStylus(string path) {
            return Path.GetExtension( path ) == ".styl";
        }
        private static bool IsSupported(string path) {
            return !Path.GetFileName( path ).StartsWith( "_" );
        }

    }
}
