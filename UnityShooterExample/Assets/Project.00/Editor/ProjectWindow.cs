#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEditor.ColorfulProjectWindow;
    using UnityEngine;

    [InitializeOnLoad]
    public class ProjectWindow : ProjectWindowBase {

        static ProjectWindow() {
            new ProjectWindow();
        }

        public ProjectWindow() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnGUI(string guid, Rect rect) {
            base.OnGUI( guid, rect );
        }

        protected override void DrawElement(Rect rect, string path) {
            base.DrawElement( rect, path );
            if (path.Equals( "Assets/Assets" ) || path.StartsWith( "Assets/Assets/" ) || path.StartsWith( "Assets/Assets." )) {
                Highlight( rect, Settings.AssetsColor, path.Count( i => i == '/' ) >= 2 );
                if (rect.height == 16 && path.Count( i => i == '/' ) == 1) {
                    if (path.Equals( "Assets/Assets.Project" )) {
                        rect.xMin += 59;
                        rect.width = 42;
                        DrawRect( rect, Settings.AssetsColor );
                    } else if (path.StartsWith( "Assets/Assets.Project.00" )) {
                        rect.xMin += 59;
                        rect.width = 60;
                        DrawRect( rect, Settings.AssetsColor );
                    } else if (path.StartsWith( "Assets/Assets.Project.01.UI" )) {
                        rect.xMin += 59;
                        rect.width = 73;
                        DrawRect( rect, Settings.AssetsColor );
                    } else if (path.StartsWith( "Assets/Assets.Project.05.App" )) {
                        rect.xMin += 59;
                        rect.width = 86;
                        DrawRect( rect, Settings.AssetsColor );
                    } else if (path.StartsWith( "Assets/Assets.Project.06.Game" )) {
                        rect.xMin += 59;
                        rect.width = 96;
                        DrawRect( rect, Settings.AssetsColor );
                    } else if (path.StartsWith( "Assets/Assets.Project.07.Infrastructure" )) {
                        rect.xMin += 59;
                        rect.width = 140;
                        DrawRect( rect, Settings.AssetsColor );
                    }
                }
            }
        }

        protected override void DrawPackage(Rect rect, string path, string package) {
            base.DrawPackage( rect, path, package );
        }
        protected override void DrawAssembly(Rect rect, string path, string? package, string assembly) {
            base.DrawAssembly( rect, path, package, assembly );
            if (rect.height == 16) {
                if (assembly.Equals( "Project" )) {
                    rect.xMin += 18;
                    rect.width = 42;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.00" )) {
                    rect.xMin += 18;
                    rect.width = 60;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.01.UI" )) {
                    rect.xMin += 18;
                    rect.width = 73;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.02.UI" )) {
                    rect.xMin += 18;
                    rect.width = 75;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.05.App" )) {
                    rect.xMin += 18;
                    rect.width = 86;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.06.Game" )) {
                    rect.xMin += 18;
                    rect.width = 96;
                    DrawRect( rect, Settings.AssemblyColor );
                } else if (assembly.StartsWith( "Project.07.Infrastructure" )) {
                    rect.xMin += 18;
                    rect.width = 140;
                    DrawRect( rect, Settings.AssemblyColor );
                }
            }
        }
        protected override void DrawAssets(Rect rect, string path, string? package, string? assembly, string content) {
            base.DrawAssets( rect, path, package, assembly, content );
        }
        protected override void DrawResources(Rect rect, string path, string? package, string? assembly, string content) {
            base.DrawResources( rect, path, package, assembly, content );
        }
        protected override void DrawSources(Rect rect, string path, string? package, string? assembly, string content) {
            base.DrawSources( rect, path, package, assembly, content );
        }

        protected override bool IsPackage(string path, [NotNullWhen( true )] out string? package, [NotNullWhen( true )] out string? content) {
            var patterns = new[] {
                "Packages/com.denis535.addressables-extensions",
                "Packages/com.denis535.addressables-source-generator",
                "Packages/com.denis535.clean-architecture-game-framework",
                "Packages/com.denis535.colorful-project-window",
                "Packages/com.denis535.uitoolkit-theme-style-sheet",
            };
            foreach (var pattern in patterns) {
                if (IsMatch( path, pattern, out package, out content )) {
                    return true;
                }
            }
            package = null;
            content = null;
            return false;
        }
        protected override bool IsAssembly(string path, string? package, [NotNullWhen( true )] out string? assembly, [NotNullWhen( true )] out string? content) {
            if (package != null) {
                var patterns = new[] {
                    // com.denis535.addressables-extensions
                    "Packages/com.denis535.addressables-extensions/Runtime/Denis535.Addressables.Extensions",
                    // com.denis535.addressables-source-generator
                    "Packages/com.denis535.addressables-source-generator/Editor/Denis535.Addressables.SourceGenerator",
                    // com.denis535.clean-architecture-game-framewor
                    "Packages/com.denis535.clean-architecture-game-framework/Runtime/Denis535.CleanArchitectureGameFramework",
                    "Packages/com.denis535.clean-architecture-game-framework/Runtime/Denis535.CleanArchitectureGameFramework.Additions",
                    "Packages/com.denis535.clean-architecture-game-framework/Runtime/Denis535.CleanArchitectureGameFramework.Internal",
                    "Packages/com.denis535.clean-architecture-game-framework/Editor/Denis535.CleanArchitectureGameFramework.Editor", 
                    // com.denis535.colorful-project-window
                    "Packages/com.denis535.colorful-project-window/Editor/Denis535.ColorfulProjectWindow", 
                    // com.denis535.uitoolkit-theme-style-sheet
                    "Packages/com.denis535.uitoolkit-theme-style-sheet/Runtime/UIToolkit.ThemeStyleSheet",
                    "Packages/com.denis535.uitoolkit-theme-style-sheet/Editor/UIToolkit.ThemeStyleSheet.Editor",
                };
                foreach (var pattern in patterns) {
                    if (IsMatch( path, pattern, out assembly, out content )) {
                        return true;
                    }
                }
            } else {
                var patterns = new[] {
                    "Assets/Project",
                    "Assets/Project.00",
                    "Assets/Project.01.UI",
                    "Assets/Project.01.UI.00.MainScreen",
                    "Assets/Project.01.UI.01.GameScreen",
                    "Assets/Project.01.UI.02.Common",
                    "Assets/Project.02.UI",
                    "Assets/Project.02.UI.00.MainScreen",
                    "Assets/Project.02.UI.01.GameScreen",
                    "Assets/Project.02.UI.02.Common",
                    "Assets/Project.05.App",
                    "Assets/Project.06.Game",
                    "Assets/Project.06.Game.Actors",
                    "Assets/Project.06.Game.Things",
                    "Assets/Project.06.Game.Worlds",
                    "Assets/Project.07.Infrastructure",
                    "Assets/Plugins/Denis535.Addressables.Extensions",
                    "Assets/Plugins/Denis535.Addressables.SourceGenerator",
                    "Assets/Plugins/Denis535.CleanArchitectureGameFramework",
                    "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Additions",
                    "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Internal",
                    "Assets/Plugins/Denis535.CleanArchitectureGameFramework.Editor",
                    "Assets/Plugins/Denis535.ColorfulProjectWindow",
                    "Assets/Plugins/UIToolkit.ThemeStyleSheet",
                    "Assets/Plugins/UIToolkit.ThemeStyleSheet.Editor",
                };
                foreach (var pattern in patterns) {
                    if (IsMatch( path, pattern, out assembly, out content )) {
                        return true;
                    }
                }
            }
            assembly = null;
            content = null;
            return false;
        }
        protected override bool IsAssets(string path, string? package, string? assembly, string content) {
            return base.IsAssets( path, package, assembly, content );
        }
        protected override bool IsResources(string path, string? package, string? assembly, string content) {
            return base.IsResources( path, package, assembly, content );
        }
        protected override bool IsSources(string path, string? package, string? assembly, string content) {
            return base.IsSources( path, package, assembly, content );
        }

    }
}
#endif
