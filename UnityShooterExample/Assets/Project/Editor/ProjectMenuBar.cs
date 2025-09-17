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
            _ = EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Main", priority = 1 )]
        public static void LoadStartup() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "Main.unity" );
            _ = EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Main Scene", priority = 2 )]
        public static void LoadMainScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "MainScene.unity" );
            _ = EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Game Scene", priority = 3 )]
        public static void LoadGameScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "GameScene.unity" );
            _ = EditorSceneManager.OpenScene( path );
        }

        [MenuItem( "Project/World 01", priority = 100 )]
        public static void LoadWorld01() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_01.unity" );
            _ = EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 02", priority = 101 )]
        public static void LoadWorld02() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_02.unity" );
            _ = EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 03", priority = 101 )]
        public static void LoadWorld03() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_03.unity" );
            _ = EditorSceneManager.OpenScene( path );
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
            _ = UnityEditor.PackageManager.Client.Embed( "com.denis535.addressables-extensions" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.addressables-source-generator", priority = 401 )]
        public static void EmbedPackage_AddressablesSourceGenerator() {
            _ = UnityEditor.PackageManager.Client.Embed( "com.denis535.addressables-source-generator" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.game-framework-pro", priority = 402 )]
        public static void EmbedPackage_GameFrameworkPro() {
            _ = UnityEditor.PackageManager.Client.Embed( "com.denis535.game-framework-pro" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.colorful-project-window", priority = 403 )]
        public static void EmbedPackage_ColorfulProjectWindow() {
            _ = UnityEditor.PackageManager.Client.Embed( "com.denis535.colorful-project-window" );
        }

        [MenuItem( "Project/Embed Package/com.denis535.uitoolkit-theme-style-sheet", priority = 404 )]
        public static void EmbedPackage_UIToolkitThemeStyleSheet() {
            _ = UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

        [MenuItem( "Project/Open Project Assets (CSharp)", priority = 500 )]
        public static void OpenProjectAssets_CSharp() {
            foreach (var path in GetProjectAssets_CSharp().Reverse()) {
                _ = AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                Thread.Sleep( 50 );
            }
        }

        //[MenuItem( "Project/Reset Project Window", priority = 600 )]
        //public static void ResetProjectWindow() {
        //    var window = GetProjectWindow();
        //    var root = GetRootItem( window );
        //    foreach (var descendant in GetDescendants( root )) {
        //        if (descendant.displayName is "Assets" or "Packages") {
        //            SetIsExpanded( window, descendant, true );
        //        } else {
        //            SetIsExpanded( window, descendant, false );
        //        }
        //    }
        //    window.Repaint();
        //}

        //[MenuItem( "Project/F1 _F1", priority = 601 )]
        //public static async void F1() {
        //    var window = GetProjectWindow();
        //    var item = GetSelectedItems( window ).FirstOrDefault();
        //    if (item != null && IsExpanded( window, item )) {
        //        foreach (var descendant in GetDescendants( item )) {
        //            {
        //                SetSelectedItems( window, item, descendant );
        //                window.Repaint();
        //                await Task.Delay( 500 );
        //            }
        //            if (!IsExpanded( window, descendant ) && IsFolder( descendant ) && descendant.hasChildren && descendant.displayName is not "AddressableAssetsData" and not "Prototyping") {
        //                SetIsExpanded( window, descendant, true );
        //                window.Repaint();
        //                await Task.Delay( 500 );

        //                foreach (var descendant2 in GetDescendants( GetItem( window, descendant.id ) )) {
        //                    SetSelectedItems( window, item, descendant, descendant2 );
        //                    window.Repaint();
        //                    await Task.Delay( 150 );
        //                }

        //                if (IsExpanded( window, descendant )) {
        //                    await Task.Delay( 500 );
        //                    SetSelectedItems( window, item, descendant );
        //                    window.Repaint();
        //                    await Task.Delay( 500 );
        //                    SetIsExpanded( window, descendant, false );
        //                    window.Repaint();
        //                    await Task.Delay( 500 );
        //                }
        //            }
        //        }
        //        SetSelectedItems( window, item );
        //    }
        //}

        //[MenuItem( "Project/F3 _F3", priority = 603 )]
        //public static void F3() {
        //    var window = GetProjectWindow();
        //    ScrollProjectWindow( window, Vector2.up * 500f );
        //    window.Repaint();
        //}

        // Helpers
        private static IEnumerable<string> GetProjectAssets_CSharp() {
            var paths = AssetDatabase.GetAllAssetPaths().Where( i => i.EndsWith( ".cs" ) ).Select( i => new {
                path = i,
                dir = Path.GetDirectoryName( i ).Replace( '\\', '/' ),
                name = Path.GetFileName( i )
            } ).ToList();

            var paths1 = paths.Where( i => {
                return i.dir is
                    "Assets/Project" or
                    "Assets/Project/Editor";
            } )
            .OrderByDescending( i => i.name == "Launcher.cs" )
            .ThenByDescending( i => i.name == "Program.cs" )
            .ThenByDescending( i => i.name == "DebugScreen.cs" )
            .ThenByDescending( i => i.name == "ProjectMenuBar.cs" )
            .ThenByDescending( i => i.name == "ProjectWindow.cs" )
            .Select( i => i.path );

            var paths2 = paths.Where( i => {
                return i.dir is
                    "Assets/Project.00.UI" or
                    "Assets/Project.00.UI/RootWidget" or
                    "Assets/Project.00.UI.00.Main" or
                    "Assets/Project.00.UI.00.Main/MainWidget" or
                    "Assets/Project.00.UI.01.Game" or
                    "Assets/Project.00.UI.01.Game/GameWidget" or
                    "Assets/Project.00.UI.02.Common/DialogWidget" or
                    "Assets/Project.00.UI.02.Common/LoadingWidget" or
                    "Assets/Project.00.UI.02.Common/SettingsWidget";
            } )
            .OrderByDescending( i => i.name == "Theme.cs" )
            .ThenByDescending( i => i.name == "Screen.cs" )
            .ThenByDescending( i => i.name == "Router.cs" )

            .ThenByDescending( i => i.name == "MainPlayList.cs" )
            .ThenByDescending( i => i.name == "GamePlayList.cs" )

            .ThenByDescending( i => i.name == "RootWidget.cs" )
            .ThenByDescending( i => i.name == "RootWidgetView.cs" )

            .ThenByDescending( i => i.name == "MainWidget.cs" )
            .ThenByDescending( i => i.name == "MainWidgetView.cs" )

            .ThenByDescending( i => i.name == "MainMenuWidget.cs" )
            .ThenByDescending( i => i.name == "MainMenuWidgetView.cs" )

            .ThenByDescending( i => i.name == "GameWidget.cs" )
            .ThenByDescending( i => i.name == "GameWidgetView.cs" )

            .ThenByDescending( i => i.name == "PlayerWidget.cs" )
            .ThenByDescending( i => i.name == "PlayerWidgetView.cs" )

            .ThenByDescending( i => i.name == "GameTotalsWidget.cs" )
            .ThenByDescending( i => i.name == "GameTotalsWidgetView.cs" )

            .ThenByDescending( i => i.name == "GameMenuWidget.cs" )
            .ThenByDescending( i => i.name == "GameMenuWidgetView.cs" )

            .ThenByDescending( i => i.name == "DialogWidget.cs" )
            .ThenByDescending( i => i.name == "DialogWidgetView.cs" )

            .ThenByDescending( i => i.name == "LoadingWidget.cs" )
            .ThenByDescending( i => i.name == "LoadingWidgetView.cs" )

            .ThenByDescending( i => i.name == "UnloadingWidget.cs" )
            .ThenByDescending( i => i.name == "UnloadingWidgetView.cs" )

            .ThenByDescending( i => i.name == "SettingsWidget.cs" )
            .ThenByDescending( i => i.name == "SettingsWidgetView.cs" )

            .ThenByDescending( i => i.name == "ProfileSettingsWidget.cs" )
            .ThenByDescending( i => i.name == "ProfileSettingsWidgetView.cs" )

            .ThenByDescending( i => i.name == "VideoSettingsWidget.cs" )
            .ThenByDescending( i => i.name == "VideoSettingsWidgetView.cs" )

            .ThenByDescending( i => i.name == "AudioSettingsWidget.cs" )
            .ThenByDescending( i => i.name == "AudioSettingsWidgetView.cs" )

            .Select( i => i.path );

            var paths3 = paths.Where( i => {
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

            var paths4 = paths.Where( i => {
                return i.dir is
                    "Assets/Project.10.Game" or
                    "Assets/Project.10.Game/Characters/Playable" or
                    "Assets/Project.10.Game/Characters/NonPlayable" or
                    "Assets/Project.10.Game/Camera/Playable" or
                    "Assets/Project.10.Game.Entities" or
                    "Assets/Project.10.Game.Entities/Characters" or
                    "Assets/Project.10.Game.Entities/Characters/Playable" or
                    "Assets/Project.10.Game.Entities/Characters/NonPlayable" or
                    "Assets/Project.10.Game.Entities/Weapons" or
                    "Assets/Project.10.Game.Entities/Camera/Playable" or
                    "Assets/Project.10.Game.Worlds";
            } )
            .Select( i => i.path );

            return paths1.Concat( paths2 ).Concat( paths3 ).Concat( paths4 );
        }
        // Helpers
        //private static EditorWindow GetProjectWindow() {
        //    //const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    return (EditorWindow) typeof( EditorWindow ).Assembly.GetType( "UnityEditor.ProjectBrowser" ).GetField( "s_LastInteractedProjectBrowser", StaticFlags ).GetValue( null ) ?? throw new NullReferenceException( "Field 's_LastInteractedProjectBrowser' is null" );
        //}
        //private static void ScrollProjectWindow(EditorWindow window, Vector2 delta) {
        //    var treeViewState = GetTreeViewState( window );
        //    treeViewState.scrollPos += delta;
        //}
        //private static TreeViewItem GetRootItem(EditorWindow window) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    var treeViewDataSource = GetTreeViewDataSource( window );
        //    return (TreeViewItem) treeViewDataSource.GetType().GetField( "m_RootItem", InstanceFlags ).GetValue( treeViewDataSource );
        //}
        //private static TreeViewItem GetItem(EditorWindow window, int id) {
        //    return GetDescendantsAndSelf( GetRootItem( window ) ).First( i => i.id == id );
        //}
        //private static TreeViewItem[] GetSelectedItems(EditorWindow window) {
        //    var items = GetDescendantsAndSelf( GetRootItem( window ) );
        //    return items.Where( i => i.id == Selection.activeInstanceID ).ToArray();
        //}
        //private static void SetSelectedItems(EditorWindow window, params TreeViewItem[] items) {
        //    Selection.instanceIDs = items.Select( i => i.id ).ToArray();
        //}
        //private static bool IsExpanded(EditorWindow window, TreeViewItem item) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    var treeViewDataSource = GetTreeViewDataSource( window );
        //    return (bool) treeViewDataSource.GetType().GetMethod( "IsExpanded", 0, InstanceFlags, null, new[] { typeof( int ) }, null ).Invoke( treeViewDataSource, new object?[] { item.id } );
        //}
        //private static void SetIsExpanded(EditorWindow window, TreeViewItem item, bool isExpanded) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    var treeViewDataSource = GetTreeViewDataSource( window );
        //    treeViewDataSource.GetType().GetMethod( "SetExpanded", 0, InstanceFlags, null, new[] { typeof( int ), typeof( bool ) }, null ).Invoke( treeViewDataSource, new object?[] { item.id, isExpanded } );
        //}
        //private static void SetIsExpandedWithChildren(EditorWindow window, TreeViewItem item, bool isExpanded) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    var treeViewDataSource = GetTreeViewDataSource( window );
        //    treeViewDataSource.GetType().GetMethod( "SetExpandedWithChildren", 0, InstanceFlags, null, new[] { typeof( int ), typeof( bool ) }, null ).Invoke( treeViewDataSource, new object?[] { item.id, isExpanded } );
        //}
        ////private static void SetExpandedItems(EditorWindow window, params TreeViewItem[] items) {
        ////    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        ////    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        ////    var treeViewDataSource = GetTreeViewDataSource( window );
        ////    treeViewDataSource.GetType().GetMethod( "SetExpandedIDs", InstanceFlags ).Invoke( treeViewDataSource, new object?[] { items.Select( i => i.id ).ToArray() } );
        ////}
        //private static object GetTreeView(EditorWindow window) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    return window.GetType().GetField( "m_AssetTree", InstanceFlags ).GetValue( window ) ?? throw new NullReferenceException( "Field 'm_AssetTree' is null" );
        //}
        //private static object GetTreeViewDataSource(EditorWindow window) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    var treeView = GetTreeView( window );
        //    return treeView.GetType().GetProperty( "data", InstanceFlags ).GetValue( treeView ) ?? throw new NullReferenceException( "Property 'data' is null" );
        //}
        //private static TreeViewState GetTreeViewState(EditorWindow window) {
        //    const BindingFlags InstanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //    //const BindingFlags StaticFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        //    return (TreeViewState) window.GetType().GetField( "m_AssetTreeState", InstanceFlags ).GetValue( window );
        //}
        //private static IEnumerable<TreeViewItem> GetDescendants(TreeViewItem item) {
        //    if (item.hasChildren) {
        //        foreach (var child in item.children.OfType<TreeViewItem>()) {
        //            yield return child;
        //            foreach (var i in GetDescendants( child )) yield return i;
        //        }
        //    }
        //}
        //private static IEnumerable<TreeViewItem> GetDescendantsAndSelf(TreeViewItem item) {
        //    return GetDescendants( item ).Prepend( item );
        //}
        //private static bool IsFolder(TreeViewItem item) {
        //    return item.icon.name == "d_Folder Icon";
        //}

    }
}
#endif
