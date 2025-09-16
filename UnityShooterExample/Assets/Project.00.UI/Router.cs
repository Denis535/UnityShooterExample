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

    public class Router : RouterBase2<Theme, Screen, Application2> {

        private static readonly Lock @lock = new Lock();

        private static SceneHandle Main { get; } = new SceneHandle( R.Project.Value_Main );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Value_GameScene );
        private SceneHandle? WorldScene { get; set; }
        public bool IsMainSceneLoaded => this.MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => this.GameScene.IsSucceeded;
        public bool IsWorldSceneLoaded => this.WorldScene != null;

        public Router(IDependencyContainer container) : base( container ) {
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
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !this.MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !this.GameScene.IsDone );
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: MainScene" );
#endif
            using (@lock.Enter()) {
                this.Theme.PlayMainTheme();
                this.Screen.ShowMainScreen();
                {
                    await this.LoadAsync_MainScene();
                }
            }
        }

        public async void LoadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
            Assert.Operation.Message( $"MainScene must be loaded" ).Valid( this.MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !this.GameScene.IsDone );
            Assert.Operation.Message( $"Game must be null" ).Valid( this.Application.Game == null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Load: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                this.Theme.PlayLoadingTheme();
                this.Screen.ShowLoadingScreen();
                {
                    await this.UnloadAsync_MainScene();
                    await this.LoadAsync_GameScene();
                    await this.LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    this.RunGame( gameInfo, playerInfo );
                }
                this.Theme.PlayGameTheme();
                this.Screen.ShowGameScreen();
            }
        }

        public async void ReloadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !this.MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( this.GameScene.IsDone );
            Assert.Operation.Message( $"Game must be non-null" ).Valid( this.Application.Game != null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Reload: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
            using (@lock.Enter()) {
                this.Theme.PlayLoadingTheme();
                this.Screen.ShowLoadingScreen();
                {
                    this.StopGame();
                    await this.UnloadAsync_WorldScene();
                    await this.UnloadAsync_GameScene();
                    await this.LoadAsync_GameScene();
                    await this.LoadAsync_WorldScene( GetWorldSceneAddress( gameInfo.Level ) );
                    this.RunGame( gameInfo, playerInfo );
                }
                this.Theme.PlayGameTheme();
                this.Screen.ShowGameScreen();
            }
        }

        public async void UnloadGameScene() {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !this.MainScene.IsDone );
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( this.GameScene.IsDone );
            Assert.Operation.Message( $"Game must be non-null" ).Valid( this.Application.Game != null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Unload: GameScene" );
#endif
            using (@lock.Enter()) {
                this.Theme.PlayUnloadingTheme();
                this.Screen.ShowUnloadingScreen();
                {
                    this.StopGame();
                    await this.UnloadAsync_WorldScene();
                    await this.UnloadAsync_GameScene();
                    await this.LoadAsync_MainScene();
                }
                this.Theme.PlayMainTheme();
                this.Screen.ShowMainScreen();
            }
        }

        public async void Quit() {
            Assert.Operation.Message( $"MainScene or GameScene must be loaded" ).Valid( this.MainScene.IsDone || this.GameScene.IsDone );
#if !UNITY_EDITOR
            Debug.Log( "Quit" );
#endif
            using (@lock.Enter()) {
                this.Theme.StopTheme();
                this.Screen.HideScreen();
                {
                    if (this.Application.Game != null) this.StopGame();
                    if (this.WorldScene != null) await this.UnloadAsync_WorldScene();
                    if (this.GameScene.IsValid) await this.UnloadAsync_GameScene();
                    if (this.MainScene.IsValid) await this.UnloadAsync_MainScene();
                }
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                UnityEngine.Application.Quit();
#endif
            }
        }

        private void RunGame(GameInfo gameInfo, PlayerInfo playerInfo) {
            var game = this.Application.RunGame( gameInfo, playerInfo );
            game.OnPauseChangeEvent += i => {
                if (i) this.Theme.Pause(); else this.Theme.UnPause();
            };
            game.OnStateChangeEvent += i => {
                if (i is GameState.Completed) this.Theme.PlayGameCompletedTheme( game.Player.State is PlayerState.Winner );
            };
            //game.Player.OnStateChangeEvent += i => {
            //};
        }
        private void StopGame() {
            this.Application.StopGame();
        }

        private static async Task LoadAsync_Main() {
            Assert.Operation.Message( $"Main must be non-loaded" ).Valid( !Main.IsDone );
            await Main.Load( LoadSceneMode.Single, false ).WaitAsync();
            _ = await Main.ActivateAsync();
            _ = SceneManager.SetActiveScene( await Main.GetValueAsync() );
        }
        private async Task LoadAsync_MainScene() {
            Assert.Operation.Message( $"MainScene must be non-loaded" ).Valid( !this.MainScene.IsDone );
            await this.MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            _ = await this.MainScene.ActivateAsync();
            _ = SceneManager.SetActiveScene( await this.MainScene.GetValueAsync() );
        }
        private async Task LoadAsync_GameScene() {
            Assert.Operation.Message( $"GameScene must be non-loaded" ).Valid( !this.GameScene.IsDone );
            await this.GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            _ = await this.GameScene.ActivateAsync();
            _ = SceneManager.SetActiveScene( await this.GameScene.GetValueAsync() );
        }
        private async Task LoadAsync_WorldScene(string key) {
            Assert.Operation.Message( $"WorldScene must be non-loaded" ).Valid( this.WorldScene == null );
            await Awaitable.WaitForSecondsAsync( 3, this.DisposeCancellationToken );
            this.WorldScene = new SceneHandle( key );
            await this.WorldScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            _ = await this.WorldScene.ActivateAsync();
            _ = SceneManager.SetActiveScene( await this.WorldScene.GetValueAsync() );
        }

        private async Task UnloadAsync_MainScene() {
            Assert.Operation.Message( $"MainScene must be loaded" ).Valid( this.MainScene.IsDone );
            await this.MainScene.UnloadAsync();
        }
        private async Task UnloadAsync_GameScene() {
            Assert.Operation.Message( $"GameScene must be loaded" ).Valid( this.GameScene.IsDone );
            await this.GameScene.UnloadAsync();
        }
        private async Task UnloadAsync_WorldScene() {
            Assert.Operation.Message( $"WorldScene must be loaded" ).Valid( this.WorldScene != null );
            await Awaitable.WaitForSecondsAsync( 1, this.DisposeCancellationToken );
            await this.WorldScene.UnloadAsync();
            this.WorldScene = null;
        }

        // Helpers
        private static string GetWorldSceneAddress(GameInfo.Level_ level) {
            return level switch {
                GameInfo.Level_.Level1 => R.Project.Game.Worlds.Value_World_01,
                GameInfo.Level_.Level2 => R.Project.Game.Worlds.Value_World_02,
                GameInfo.Level_.Level3 => R.Project.Game.Worlds.Value_World_03,
                _ => throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" ),
            };
        }

    }
}
