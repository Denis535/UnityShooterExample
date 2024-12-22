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

    public static class ProjectMenuBar {

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
            foreach (var path in GetAssets_CSharp().Reverse()) {
                AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                Thread.Sleep( 100 );
            }
        }

        // Helpers
        private static IEnumerable<string> GetAssets_CSharp() {
            var paths = AssetDatabase.GetAllAssetPaths()
                .Where( i => i.EndsWith( ".cs" ) )
                .Select( i => new {
                    path = i,
                    dir = Path.GetDirectoryName( i ).Replace( '\\', '/' ),
                    name = Path.GetFileName( i )
                } )
                .ToList();

            var paths1 = paths
                .Where( i => {
                    return i.dir is
                        "Assets/Project.00" or
                        "Assets/Project.00/Editor";
                } )
                .OrderByDescending( i => i.name == "Launcher.cs" )
                .ThenByDescending( i => i.name == "Program.cs" )
                .ThenByDescending( i => i.name == "DebugScreen.cs" )
                .ThenByDescending( i => i.name == "ProjectMenuBar.cs" )
                .ThenByDescending( i => i.name == "ProjectWindow.cs" )
                .Select( i => i.path );

            var paths2 = paths
                .Where( i => {
                    return i.dir is
                        "Assets/Project.01.UI" or
                        "Assets/Project.01.UI.00.MainScreen" or
                        "Assets/Project.01.UI.01.GameScreen" or
                        "Assets/Project.01.UI.02.Common" or

                        "Assets/Project.01.UI/Internal" or
                        "Assets/Project.01.UI.00.MainScreen/Internal" or
                        "Assets/Project.01.UI.01.GameScreen/Internal" or
                        "Assets/Project.01.UI.02.Common/Internal";
                } )
                .OrderByDescending( i => i.name == "Theme.cs" )
                .ThenByDescending( i => i.name == "Screen.cs" )
                .ThenByDescending( i => i.name == "Router.cs" )
                .ThenByDescending( i => i.name == "MainWidget.cs" )
                .ThenByDescending( i => i.name == "MainMenuWidget.cs" )
                .ThenByDescending( i => i.name == "GameWidget.cs" )
                .ThenByDescending( i => i.name == "PlayerWidget.cs" )
                .ThenByDescending( i => i.name == "GameTotalsWidget.cs" )
                .ThenByDescending( i => i.name == "GameMenuWidget.cs" )
                .ThenByDescending( i => i.name == "DialogWidget.cs" )
                .ThenByDescending( i => i.name == "LoadingWidget.cs" )
                .ThenByDescending( i => i.name == "UnloadingWidget.cs" )
                .ThenByDescending( i => i.name == "SettingsWidget.cs" )
                .ThenByDescending( i => i.name == "ProfileSettingsWidget.cs" )
                .ThenByDescending( i => i.name == "VideoSettingsWidget.cs" )
                .ThenByDescending( i => i.name == "AudioSettingsWidget.cs" )

                .ThenByDescending( i => i.name == "MainWidgetView.cs" )
                .ThenByDescending( i => i.name == "MainMenuWidgetView.cs" )
                .ThenByDescending( i => i.name == "GameWidgetView.cs" )
                .ThenByDescending( i => i.name == "PlayerWidgetView.cs" )
                .ThenByDescending( i => i.name == "GameTotalsWidgetView.cs" )
                .ThenByDescending( i => i.name == "GameMenuWidgetView.cs" )
                .ThenByDescending( i => i.name == "DialogWidgetView.cs" )
                .ThenByDescending( i => i.name == "LoadingWidgetView.cs" )
                .ThenByDescending( i => i.name == "UnloadingWidgetView.cs" )
                .ThenByDescending( i => i.name == "SettingsWidgetView.cs" )
                .ThenByDescending( i => i.name == "ProfileSettingsWidgetView.cs" )
                .ThenByDescending( i => i.name == "VideoSettingsWidgetView.cs" )
                .ThenByDescending( i => i.name == "AudioSettingsWidgetView.cs" )

                .Select( i => i.path );

            var paths3 = paths
                .Where( i => {
                    return i.dir is
                        "Assets/Project.05.App";
                } )
                .OrderByDescending( i => i.name == "Application2.cs" )
                .ThenByDescending( i => i.name == "Storage.cs" )
                .ThenByDescending( i => i.name == "Storage.ProfileSettings.cs" )
                .ThenByDescending( i => i.name == "Storage.VideoSettings.cs" )
                .ThenByDescending( i => i.name == "Storage.AudioSettings.cs" )
                .ThenByDescending( i => i.name == "Storage.Preferences.cs" )
                .Select( i => i.path );

            var paths4 = paths
                .Where( i => {
                    return i.dir is
                        "Assets/Project.06.Game" or
                        "Assets/Project.06.Game/Internal" or
                        "Assets/Project.06.Game.Actors" or
                        "Assets/Project.06.Game.Actors/Internal" or
                        "Assets/Project.06.Game.Things" or
                        "Assets/Project.06.Game.Things/Internal" or
                        "Assets/Project.06.Game.Worlds" or
                        "Assets/Project.06.Game.Worlds/Internal";
                } )
                .OrderByDescending( i => i.name == "Game2.cs" )
                .ThenByDescending( i => i.name == "Player2.cs" )

                .ThenByDescending( i => i.name == "CharacterBase.cs" )
                .ThenByDescending( i => i.name == "PlayableCharacterBase.cs" )
                .ThenByDescending( i => i.name == "PlayableCameraBase.cs" )
                .ThenByDescending( i => i.name == "NonPlayableCharacterBase.cs" )
                .ThenByDescending( i => i.name == "WeaponBase.cs" )

                .ThenByDescending( i => i.name == "PlayerCharacter.cs" )
                .ThenByDescending( i => i.name == "PlayerCamera.cs" )
                .ThenByDescending( i => i.name == "EnemyCharacter.cs" )
                .ThenByDescending( i => i.name == "Gun.cs" )
                .ThenByDescending( i => i.name == "Bullet.cs" )
                .ThenByDescending( i => i.name == "World.cs" )

                .ThenByDescending( i => i.name == "ICharacterInputProvider.cs" )
                .ThenByDescending( i => i.name == "ICameraInputProvider.cs" )
                .ThenByDescending( i => i.name == "CharacterInputProvider.cs" )
                .ThenByDescending( i => i.name == "CameraInputProvider.cs" )

                .Select( i => i.path );

            var paths5 = paths
                .Where( i => {
                    return i.dir is
                        "Assets/Project.07.Infrastructure/Project";
                } )
                .OrderByDescending( i => i.name == "Utils.cs" )
                .ThenByDescending( i => i.name == "MoveableBody.cs" )

                .Select( i => i.path );

            return paths1.Concat( paths2 ).Concat( paths3 ).Concat( paths4 ).Concat( paths5 );
        }

    }
}
#endif
