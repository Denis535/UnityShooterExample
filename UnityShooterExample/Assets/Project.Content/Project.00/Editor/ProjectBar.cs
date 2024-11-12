#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public static class ProjectBar {

        [MenuItem( "Project/Launcher", priority = 0 )]
        public static void LoadLauncher() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "Launcher.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Main", priority = 1 )]
        public static void LoadStartup() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "Main.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Main Scene", priority = 2 )]
        public static void LoadMainScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "MainScene.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Game Scene", priority = 3 )]
        public static void LoadGameScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "GameScene.unity" );
            EditorSceneManager.OpenScene( path );
        }

        [MenuItem( "Project/World 01", priority = 100 )]
        public static void LoadWorld01() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_01.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 02", priority = 101 )]
        public static void LoadWorld02() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_02.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 03", priority = 101 )]
        public static void LoadWorld03() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_03.unity" );
            EditorSceneManager.OpenScene( path );
        }

        [MenuItem( "Project/Pre Build", priority = 200 )]
        public static void PreBuild() {
            ProjectBuilder.PreBuild();
        }
        [MenuItem( "Project/Build Development", priority = 201 )]
        public static void BuildDevelopment() {
            var path = $"Build/Development/{PlayerSettings.productName}.exe";
            ProjectBuilder.BuildDevelopment( path );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }
        [MenuItem( "Project/Build Production", priority = 202 )]
        public static void BuildProduction() {
            var path = $"Build/Production/{PlayerSettings.productName}.exe";
            ProjectBuilder.BuildProduction( path );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }

        [MenuItem( "Project/Place Player Point", priority = 300 )]
        public static void PlacePlayerPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = new GameObject( "PlayerPoint", typeof( PlayerPoint ) );
                go.transform.position = hit.point;
                Selection.activeGameObject = go;
            }
        }
        [MenuItem( "Project/Place Enemy Point", priority = 301 )]
        public static void PlaceEnemyPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = new GameObject( "EnemyPoint", typeof( EnemyPoint ) );
                go.transform.position = hit.point;
                Selection.activeGameObject = go;
            }
        }
        [MenuItem( "Project/Place Thing Point", priority = 302 )]
        public static void PlaceThingPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = new GameObject( "ThingPoint", typeof( ThingPoint ) );
                go.transform.position = hit.point;
                Selection.activeGameObject = go;
            }
        }

        [MenuItem( "Project/Embed Package/com.denis535.addressables-extensions", priority = 400 )]
        public static void EmbedPackage_AddressablesExtensions() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.addressables-extensions" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.addressables-source-generator", priority = 401 )]
        public static void EmbedPackage_AddressablesSourceGenerator() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.addressables-source-generator" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.clean-architecture-game-framework", priority = 402 )]
        public static void EmbedPackage_CleanArchitectureGameFramework() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.clean-architecture-game-framework" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.colorful-project-window", priority = 403 )]
        public static void EmbedPackage_ColorfulProjectWindow() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.colorful-project-window" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.uitoolkit-theme-style-sheet", priority = 404 )]
        public static void EmbedPackage_UIToolkitThemeStyleSheet() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

        [MenuItem( "Project/Open Assets (CSharp)", priority = 500 )]
        public static void OpenAssets_CSharp() {
            var paths = AssetDatabase.GetAllAssetPaths()
                .Where( i => i.StartsWith( "Assets/Project.Content/" ) && Path.GetExtension( i ) == ".cs" )
                .Select( i => new {
                    path = i,
                    dir = Path.GetDirectoryName( i ).Replace( '\\', '/' ),
                    name = Path.GetFileName( i )
                } )
                .OrderByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.00" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.01.UI" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.01.UI.00.MainScreen" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.01.UI.01.GameScreen" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.01.UI.02.Common" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.02.UI" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.02.UI.00.MainScreen" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.02.UI.01.GameScreen" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.02.UI.02.Common" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.05.App" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.06.Game" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.06.Game.Actors" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.06.Game.Things" ) )
                .ThenByDescending( i => i.dir.Equals( "Assets/Project.Content/Project.06.Game.Worlds" ) )
                // UI/
                .ThenByDescending( i => i.name.Equals( "UITheme.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UIScreen.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UIRouter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MainWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "GameWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "TotalsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "DialogWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LoadingWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UnloadingWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "SettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "ProfileSettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VideoSettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "AudioSettingsWidget.cs" ) )
                // UI.Internal/
                .ThenByDescending( i => i.name.Equals( "MainWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "GameWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "TotalsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "DialogWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LoadingWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UnloadingWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "SettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "ProfileSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VideoSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "AudioSettingsWidgetView.cs" ) )
                // App/
                .ThenByDescending( i => i.name.Equals( "Application2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.ProfileSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.VideoSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.AudioSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.Preferences.cs" ) )
                // Domain.Game/
                .ThenByDescending( i => i.name.Equals( "Game2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Player2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Camera2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "PlayerCharacter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "EnemyCharacter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Gun.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Bullet.cs" ) )
                .ThenByDescending( i => i.name.Equals( "World.cs" ) )
                // Misc
                .ThenBy( i => i.path )
                .Select( i => i.path )
                .ToArray();

            //Debug.Log( string.Join( "\n", paths ) );
            foreach (var path in paths.Reverse()) {
                AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                Thread.Sleep( 100 );
            }
        }

    }
}
#endif
