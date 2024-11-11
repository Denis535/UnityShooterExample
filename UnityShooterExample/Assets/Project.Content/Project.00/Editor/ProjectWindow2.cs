#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEditor.ColorfulProjectWindow;
    using UnityEngine;

    [InitializeOnLoad]
    public class ProjectWindow2 : ProjectWindow {

        private static readonly Color AssetsColor = HSVA( 060, 1.0f, 1.0f, 0.3f );
        private static readonly Color ResourcesColor = HSVA( 060, 1.0f, 1.0f, 0.3f );
        private static readonly Color SourcesColor = HSVA( 120, 1.0f, 1.0f, 0.3f );

        static ProjectWindow2() {
            new ProjectWindow2();
        }

        public ProjectWindow2() : base( GetPackagePaths(), GetModulePaths() ) {
        }

        protected override void DrawPackageItem(Rect rect, string path, string name) {
            base.DrawPackageItem( rect, path, name );
        }
        protected override void DrawAssemblyItem(Rect rect, string path, string name) {
            base.DrawAssemblyItem( rect, path, name );
        }
        protected override void DrawAssetsItem(Rect rect, string path, string name, string rest) {
            base.DrawAssetsItem( rect, path, name, rest );
            if (name is "Project" or "Project.Content" or "Project.Infrastructure" && !rest.Contains( '/' )) {
                if (rect.height == 16) {
                    if (rest.Contains( ".00" )) {
                        rect.xMin += 101;
                        rect.width = 18;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.Contains( ".01.UI" )) {
                        rect.xMin += 101;
                        rect.width = 31;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.Contains( ".05.App" )) {
                        rect.xMin += 101;
                        rect.width = 44;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.Contains( ".06.Domain" )) {
                        rect.xMin += 101;
                        rect.width = 64;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                }
            }
        }
        protected override void DrawResourcesItem(Rect rect, string path, string name, string rest) {
            base.DrawResourcesItem( rect, path, name, rest );
        }
        protected override void DrawSourcesItem(Rect rect, string path, string name, string rest) {
            base.DrawSourcesItem( rect, path, name, rest );
            if (name is "Project" or "Project.Content" or "Project.Infrastructure" && !rest.Contains( '/' )) {
                if (rect.height == 16) {
                    if (rest.Contains( ".00" )) {
                        rect.xMin += 60;
                        rect.width = 18;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.Contains( ".01.UI" )) {
                        rect.xMin += 60;
                        rect.width = 31;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.Contains( ".02.UI" )) {
                        rect.xMin += 60;
                        rect.width = 33;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.Contains( ".05.App" )) {
                        rect.xMin += 60;
                        rect.width = 44;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.Contains( ".06.Domain" )) {
                        rect.xMin += 60;
                        rect.width = 64;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                }
            }
        }

        // Helpers
        private static string[] GetPackagePaths() {
            return Enumerable.Empty<string>()
                .Append( "Packages/com.denis535.clean-architecture-game-framework" )
                .Append( "Packages/com.denis535.addressables-extensions" )
                .Append( "Packages/com.denis535.addressables-source-generator" )
                .Append( "Packages/com.denis535.colorful-project-window" )
                .Append( "Packages/com.denis535.uitoolkit-theme-style-sheet" )
                .ToArray();
        }
        private static string[] GetModulePaths() {
            return Enumerable.Empty<string>()
                .Append( "Assets/Project" )
                .Append( "Assets/Project.Content" )
                .Append( "Assets/Project.Infrastructure" )
                .Concat( AssetDatabase.GetAllAssetPaths()
                    .Where( i => Path.GetExtension( i ) is ".asmdef" or ".asmref" )
                    .Where( i => i.StartsWith( "Packages/" ) )
                    .Select( Path.GetDirectoryName )
                    .Select( i => i.Replace( '\\', '/' ) )
                    .Distinct() ).ToArray();
        }

    }
}
#endif
