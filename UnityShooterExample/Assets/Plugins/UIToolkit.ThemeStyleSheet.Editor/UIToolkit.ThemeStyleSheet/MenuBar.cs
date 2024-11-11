#nullable enable
namespace UIToolkit.ThemeStyleSheet {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public static class MenuBar {

        // CreateAsset
        [MenuItem( "Assets/Create/UI Toolkit/Pug" )]
        public static void CreateAsset_Pug() {
            ProjectWindowUtil.CreateAssetWithContent( "New Pug.pug", "" );
        }
        //[MenuItem( "Assets/Create/UI Toolkit/Css" )]
        //public static void CreateAsset_Css() {
        //    ProjectWindowUtil.CreateAssetWithContent( "New Css.css", "" );
        //}
        //[MenuItem( "Assets/Create/UI Toolkit/Sass" )]
        //public static void CreateAsset_Sass() {
        //    ProjectWindowUtil.CreateAssetWithContent( "New Sass.sass", "" );
        //}
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
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Make Package Embedded", priority = 100 )]
        public static void MakePackageEmbedded() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

    }
}
