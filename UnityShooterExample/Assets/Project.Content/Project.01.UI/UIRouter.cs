#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Domain.Game_;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;
    using UnityEngine.SceneManagement;

    public class UIRouter : UIRouterBase2 {

        private static readonly Lock @lock = new Lock();

        private UITheme Theme => Container.RequireDependency<UITheme>();
        private UIScreen Screen => Container.RequireDependency<UIScreen>();
        private Application2 Application { get; }
        private static SceneHandle Main { get; } = new SceneHandle( R.Project.Scenes.Value_Main );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.Value_GameScene );
        private SceneHandle? WorldScene { get; set; }
        public bool IsMainSceneLoaded => MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => GameScene.IsSucceeded;
        public bool IsWorldSceneLoaded => WorldScene != null;

        public UIRouter(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            using (@lock.Enter()) {
                base.Dispose();
            }
        }

        public static async void LoadMain() {
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: Main" );
#endif
            using (@lock.Enter()) {
                await LoadAsync_Main();
            }
        }

        public async void LoadMainScene() {
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: MainScene" );
#endif
            using (@lock.Enter()) {
                Theme.PlayMainTheme();
                Screen.ShowMainScreen();
                {
                    await LoadAsync_MainScene();
                }
            }
        }

        public async void LoadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    await UnloadAsync_MainScene();
                }
                {
                    await LoadAsync_GameScene();
                    await LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    Application.RunGame( gameInfo, playerInfo );
                    Application.Game!.OnPauseChangeEvent += i => Theme.IsPaused = i;
                }
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void ReloadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
#if !UNITY_EDITOR
            Debug.LogFormat( "Reload: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    Application.StopGame();
                    await UnloadAsync_WorldScene();
                    await UnloadAsync_GameScene();
                }
                {
                    await LoadAsync_GameScene();
                    await LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    Application.RunGame( gameInfo, playerInfo );
                    Application.Game!.OnPauseChangeEvent += i => Theme.IsPaused = i;
                }
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void UnloadGameScene() {
#if !UNITY_EDITOR
            Debug.LogFormat( "Unload: GameScene" );
#endif
            using (@lock.Enter()) {
                Theme.PlayUnloadingTheme();
                Screen.ShowUnloadingScreen();
                {
                    Application.StopGame();
                    await UnloadAsync_WorldScene();
                    await UnloadAsync_GameScene();
                }
                {
                    await LoadAsync_MainScene();
                }
                Theme.PlayMainTheme();
                Screen.ShowMainScreen();
            }
        }

        public async void Quit() {
#if !UNITY_EDITOR
            Debug.Log( "Quit" );
#endif
            using (@lock.Enter()) {
                Theme.StopTheme();
                Screen.HideScreen();
                {
                    if (Application.Game != null) Application.StopGame();
                    if (WorldScene != null) await UnloadAsync_WorldScene();
                    if (GameScene.IsValid) await UnloadAsync_GameScene();
                    if (MainScene.IsValid) await UnloadAsync_MainScene();
                }
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                UnityEngine.Application.Quit();
#endif
            }
        }

        // Helpers
        private static async Task LoadAsync_Main() {
            await Main.Load( LoadSceneMode.Single, false ).WaitAsync();
            await Main.ActivateAsync();
            SceneManager.SetActiveScene( await Main.GetValueAsync() );
        }
        private async Task LoadAsync_MainScene() {
            await Task.Delay( 1_000 );
            await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await MainScene.ActivateAsync();
            SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
        }
        private async Task LoadAsync_GameScene() {
            await Task.Delay( 3_000 );
            await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await GameScene.ActivateAsync();
            SceneManager.SetActiveScene( await GameScene.GetValueAsync() );
        }
        private async Task LoadAsync_WorldScene(string key) {
            WorldScene = new SceneHandle( key );
            await WorldScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await WorldScene.ActivateAsync();
            SceneManager.SetActiveScene( await WorldScene.GetValueAsync() );
        }
        // Helpers
        private async Task UnloadAsync_MainScene() {
            await MainScene.UnloadAsync();
        }
        private async Task UnloadAsync_GameScene() {
            await GameScene.UnloadAsync();
        }
        private async Task UnloadAsync_WorldScene() {
            await WorldScene!.UnloadAsync();
            WorldScene = null;
        }
        // Helpers
        private static string GetWorldSceneAddress(GameInfo.Level_ level) {
            switch (level) {
                case GameInfo.Level_.Level1: return R.Project.Game.Worlds.Value_World_01;
                case GameInfo.Level_.Level2: return R.Project.Game.Worlds.Value_World_02;
                case GameInfo.Level_.Level3: return R.Project.Game.Worlds.Value_World_03;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }

    }
}
