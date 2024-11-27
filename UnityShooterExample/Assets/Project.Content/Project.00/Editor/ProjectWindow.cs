#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEditor.ColorfulProjectWindow;
    using UnityEngine;

    [InitializeOnLoad]
    public class ProjectWindow : ProjectWindowBase2 {

        static ProjectWindow() {
            new ProjectWindow();
        }

        public ProjectWindow() : base( GetPackagePaths(), GetModulePaths() ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnGUI(string guid, Rect rect) {
            base.OnGUI( guid, rect );
        }

        protected override bool IsPackage(string path, [NotNullWhen( true )] out string? name, [NotNullWhen( true )] out string? rest) {
            return base.IsPackage( path, out name, out rest );
        }
        protected override bool IsAssembly(string path, [NotNullWhen( true )] out string? name, [NotNullWhen( true )] out string? rest) {
            return base.IsAssembly( path, out name, out rest );
        }
        protected override bool IsAssets(string path, string name, string rest) {
            return base.IsAssets( path, name, rest );
        }
        protected override bool IsResources(string path, string name, string rest) {
            return base.IsResources( path, name, rest );
        }
        protected override bool IsSources(string path, string name, string rest) {
            return base.IsSources( path, name, rest );
        }

        protected override void DrawPackageElement(Rect rect, string path, string name, string rest) {
            base.DrawPackageElement( rect, path, name, rest );
        }
        protected override void DrawAssemblyElement(Rect rect, string path, string name, string rest) {
            base.DrawAssemblyElement( rect, path, name, rest );
        }
        protected override void DrawAssemblyContentElement(Rect rect, string path, string name, string rest) {
            base.DrawAssemblyContentElement( rect, path, name, rest );
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
                    if (rest.StartsWith( "Assets.Project.00" )) {
                        rect.xMin += 101;
                        rect.width = 18;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.StartsWith( "Assets.Project.01.UI" )) {
                        rect.xMin += 101;
                        rect.width = 31;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.StartsWith( "Assets.Project.05.App" )) {
                        rect.xMin += 101;
                        rect.width = 44;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (rest.StartsWith( "Assets.Project.06.Game" )) {
                        rect.xMin += 101;
                        rect.width = 54;
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
                    if (rest.StartsWith( "Project.00" )) {
                        rect.xMin += 60;
                        rect.width = 18;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.StartsWith( "Project.01.UI" )) {
                        rect.xMin += 60;
                        rect.width = 31;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.StartsWith( "Project.02.UI" )) {
                        rect.xMin += 60;
                        rect.width = 33;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.StartsWith( "Project.05.App" )) {
                        rect.xMin += 60;
                        rect.width = 44;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (rest.StartsWith( "Project.06.Game" )) {
                        rect.xMin += 60;
                        rect.width = 54;
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
                .Append( "Assets/Plugins/Denis535.Addressables.Extensions" )
                .Append( "Assets/Plugins/Denis535.Addressables.SourceGenerator" )
                .Append( "Assets/Plugins/Denis535.CleanArchitectureGameFramework" )
                .Append( "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Additions" )
                .Append( "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Internal" )
                .Append( "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Editor" )
                .Append( "Assets/Plugins/Denis535.ColorfulProjectWindow" )
                .Append( "Assets/Plugins/UIToolkit.ThemeStyleSheet" )
                .Append( "Assets/Plugins/UIToolkit.ThemeStyleSheet.Editor" )
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
