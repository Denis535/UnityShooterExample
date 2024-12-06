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

        protected override void DrawElement(Rect rect, string path) {
            if (path.Equals( "Assets/Assets" ) || path.StartsWith( "Assets/Assets/" ) || path.StartsWith( "Assets/Assets." )) {
                Highlight( rect, Settings.AssetsColor, path.Count( i => i == '/' ) >= 2 );
                if (rect.height == 16) {
                    if (path.StartsWith( "Assets/Assets.Project.00" )) {
                        rect.xMin += 101;
                        rect.width = 18;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (path.StartsWith( "Assets/Assets.Project.01.UI" )) {
                        rect.xMin += 101;
                        rect.width = 31;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (path.StartsWith( "Assets/Assets.Project.05.App" )) {
                        rect.xMin += 101;
                        rect.width = 44;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                    if (path.StartsWith( "Assets/Assets.Project.06.Game" )) {
                        rect.xMin += 101;
                        rect.width = 54;
                        DrawRect( rect, Settings.AssetsColor );
                        return;
                    }
                }
                return;
            }
            base.DrawElement( rect, path );
        }
        protected override void DrawPackageElement(Rect rect, string path, string package, string content) {
            base.DrawPackageElement( rect, path, package, content );
        }
        protected override void DrawAssemblyElement(Rect rect, string path, string assembly, string content) {
            base.DrawAssemblyElement( rect, path, assembly, content );
        }

        protected override void DrawPackage(Rect rect, string path, string assembly) {
            base.DrawPackage( rect, path, assembly );
        }
        protected override void DrawAssembly(Rect rect, string path, string assembly) {
            base.DrawAssembly( rect, path, assembly );
        }
        protected override void DrawAssets(Rect rect, string path, string assembly, string content) {
            base.DrawAssets( rect, path, assembly, content );
        }
        protected override void DrawResources(Rect rect, string path, string assembly, string content) {
            base.DrawResources( rect, path, assembly, content );
        }
        protected override void DrawSources(Rect rect, string path, string assembly, string content) {
            base.DrawSources( rect, path, assembly, content );
            if (assembly is "Project" or "Project.Content" or "Project.Infrastructure" && !content.Contains( '/' )) {
                if (rect.height == 16) {
                    if (content.StartsWith( "Project.00" )) {
                        rect.xMin += 60;
                        rect.width = 18;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (content.StartsWith( "Project.01.UI" )) {
                        rect.xMin += 60;
                        rect.width = 31;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (content.StartsWith( "Project.02.UI" )) {
                        rect.xMin += 60;
                        rect.width = 33;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (content.StartsWith( "Project.05.App" )) {
                        rect.xMin += 60;
                        rect.width = 44;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (content.StartsWith( "Project.06.Game" )) {
                        rect.xMin += 60;
                        rect.width = 54;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                    if (content.StartsWith( "Project.07.Infrastructure" )) {
                        rect.xMin += 60;
                        rect.width = 98;
                        DrawRect( rect, Settings.SourcesColor );
                        return;
                    }
                }
            }
        }

        protected override bool IsPackage(string path, [NotNullWhen( true )] out string? package, [NotNullWhen( true )] out string? content) {
            return base.IsPackage( path, out package, out content );
        }
        protected override bool IsAssembly(string path, [NotNullWhen( true )] out string? assembly, [NotNullWhen( true )] out string? content) {
            return base.IsAssembly( path, out assembly, out content );
        }
        protected override bool IsAssets(string path, string assembly, string content) {
            return base.IsAssets( path, assembly, content );
        }
        protected override bool IsResources(string path, string assembly, string content) {
            return base.IsResources( path, assembly, content );
        }
        protected override bool IsSources(string path, string assembly, string content) {
            return base.IsSources( path, assembly, content );
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
