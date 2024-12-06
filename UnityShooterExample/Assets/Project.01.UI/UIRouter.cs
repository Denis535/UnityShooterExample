#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Game;
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
            Assert.Operation.Message( $"Main must be non-loaded" ).Valid( !Main.IsDone );
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: Main" );
#endif
            using (@lock.Enter()) {
                await LoadAsync_Main();
            }
        }

        public async void LoadMainScene() {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !GameScene.IsDone );
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
            Assert.Operation.Message( $"MainScene must be loaded" ).Valid( MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !GameScene.IsDone );
            Assert.Operation.Message( $"Game must be null" ).Valid( Application.Game == null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    await UnloadAsync_MainScene();
                    await LoadAsync_GameScene();
                    await LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    RunGame( gameInfo, playerInfo );
                }
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void ReloadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( GameScene.IsDone );
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Application.Game != null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Reload: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    StopGame();
                    await UnloadAsync_WorldScene();
                    await UnloadAsync_GameScene();
                    await LoadAsync_GameScene();
                    await LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    RunGame( gameInfo, playerInfo );
                }
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void UnloadGameScene() {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( GameScene.IsDone );
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Application.Game != null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Unload: GameScene" );
#endif
            using (@lock.Enter()) {
                Theme.PlayUnloadingTheme();
                Screen.ShowUnloadingScreen();
                {
                    StopGame();
                    await UnloadAsync_WorldScene();
                    await UnloadAsync_GameScene();
                    await LoadAsync_MainScene();
                }
                Theme.PlayMainTheme();
                Screen.ShowMainScreen();
            }
        }

        public async void Quit() {
            Assert.Operation.Message( $"MainScene or GameScene must be loaded" ).Valid( MainScene.IsDone || GameScene.IsDone );
#if !UNITY_EDITOR
            Debug.Log( "Quit" );
#endif
            using (@lock.Enter()) {
                Theme.StopTheme();
                Screen.HideScreen();
                {
                    if (Application.Game != null) StopGame();
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

        private void RunGame(GameInfo gameInfo, PlayerInfo playerInfo) {
            var game = Application.RunGame( gameInfo, playerInfo );
            game.OnPauseChangeEvent += i => {
                if (i) Theme.Pause(); else Theme.UnPause();
            };
            game.OnStateChangeEvent += i => {
                if (i is GameState.Completed) Theme.PlayGameCompletedTheme( game.Player.State is PlayerState.Winner );
            };
            //game.Player.OnStateChangeEvent += i => {
            //};
        }
        private void StopGame() {
            Application.StopGame();
        }

        private static async Task LoadAsync_Main() {
            Assert.Operation.Message( $"Main must be non-loaded" ).Valid( !Main.IsDone );
            await Main.Load( LoadSceneMode.Single, false ).WaitAsync();
            await Main.ActivateAsync();
            SceneManager.SetActiveScene( await Main.GetValueAsync() );
        }
        private async Task LoadAsync_MainScene() {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !MainScene.IsDone );
            await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await MainScene.ActivateAsync();
            SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
        }
        private async Task LoadAsync_GameScene() {
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !GameScene.IsDone );
            await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await GameScene.ActivateAsync();
            SceneManager.SetActiveScene( await GameScene.GetValueAsync() );
        }
        private async Task LoadAsync_WorldScene(string key) {
            Assert.Operation.Message( $"WorldScene must be non-loaded" ).Valid( WorldScene == null );
            await Task.Delay( 3_000 );
            WorldScene = new SceneHandle( key );
            await WorldScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await WorldScene.ActivateAsync();
            SceneManager.SetActiveScene( await WorldScene.GetValueAsync() );
        }

        private async Task UnloadAsync_MainScene() {
            Assert.Operation.Message( $"MainScene must be loaded" ).Valid( MainScene.IsDone );
            await MainScene.UnloadAsync();
        }
        private async Task UnloadAsync_GameScene() {
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( GameScene.IsDone );
            await GameScene.UnloadAsync();
        }
        private async Task UnloadAsync_WorldScene() {
            Assert.Operation.Message( $"WorldScene must be loaded" ).Valid( WorldScene != null );
            await WorldScene.UnloadAsync();
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
