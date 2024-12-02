#nullable enable
namespace UIToolkit.ThemeStyleSheet {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public static class PackageMenuBar {

        // CreateAsset
        [MenuItem( "Assets/Create/UI Toolkit/Pug" )]
        public static void CreateAsset_Pug() {
            ProjectWindowUtil.CreateAssetWithContent( "New Pug.pug", "" );
        }
        [MenuItem( "Assets/Create/UI Toolkit/Stylus" )]
        public static void CreateAsset_Stylus() {
            ProjectWindowUtil.CreateAssetWithContent( "New Stylus.styl", "" );
        }

        // TakeScreenshot
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Take Screenshot (Game) _F12", priority = 0 )]
        internal static void TakeScreenshot_Game() {
            var path = $"Screenshots/{Application.productName}-{DateTime.UtcNow.Ticks}.png";
            ScreenCapture.CaptureScreenshot( path, 1 );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Take Screenshot (Editor) &F12", priority = 1 )]
        internal static void TakeScreenshot_Editor() {
            var position = EditorGUIUtility.GetMainWindowPosition();
            var texture = new Texture2D( (int) position.width, (int) position.height );
            texture.SetPixels( InternalEditorUtility.ReadScreenPixel( position.position, (int) position.width, (int) position.height ) );
            var bytes = texture.EncodeToPNG();
            UnityEngine.Object.DestroyImmediate( texture );

            var path = $"Screenshots/{Application.productName}-{DateTime.UtcNow.Ticks}.png";
            Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            File.WriteAllBytes( path, bytes );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }

        // EmbedPackage
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Embed Package", priority = 100 )]
        public static void EmbedPackage() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

        // AboutPackage
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/About Package", priority = 1_000_000 )]
        public static void AboutPackage() {
            _ = EditorWindow.GetWindow<AboutPackageWindow>();
        }

    }
}
