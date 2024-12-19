#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.Game;
    using Unity.Services.Authentication;
    using Unity.Services.Core;
    using UnityEngine;
    using UnityEngine.Framework;
    using PlayerInfo = Game.PlayerInfo;

    public class Application2 : ApplicationBase2 {

        public Storage Storage { get; }
        public Storage.ProfileSettings ProfileSettings { get; }
        public Storage.VideoSettings VideoSettings { get; }
        public Storage.AudioSettings AudioSettings { get; }
        public Storage.Preferences Preferences { get; }
        public Task InitializeTask { get; }
        public IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        public Game2? Game { get; private set; }

        public Application2(IDependencyContainer container) : base( container ) {
            Storage = new Storage();
            ProfileSettings = new Storage.ProfileSettings();
            VideoSettings = new Storage.VideoSettings();
            AudioSettings = new Storage.AudioSettings();
            Preferences = new Storage.Preferences();
            InitializeTask = InitializeAsync();
        }
        public override void Dispose() {
            Storage.Dispose();
            ProfileSettings.Dispose();
            VideoSettings.Dispose();
            AudioSettings.Dispose();
            Preferences.Dispose();
            base.Dispose();
        }

        private async Task InitializeAsync() {
            await Task.Delay( 1000 );
            //if (UnityServices.State != ServicesInitializationState.Initialized) {
            //    var options = new InitializationOptions();
            //    if (Storage.Profile != null) options.SetProfile( Storage.Profile );
            //    await UnityServices.InitializeAsync( options ).WaitAsync( DisposeCancellationToken );
            //}
            //if (!AuthenticationService.IsSignedIn) {
            //    var options = new SignInOptions() {
            //        CreateAccount = true,
            //    };
            //    await AuthenticationService.SignInAnonymouslyAsync( options ).WaitAsync( DisposeCancellationToken );
            //}
        }

        public Game2 RunGame(GameInfo gameInfo, PlayerInfo playerInfo) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Run Game" );
#endif
            {
                PlayerCamera.Factory.Load();
                PlayerCharacter.Factory.Load();
                EnemyCharacter.Factory.Load();
                Gun.Factory.Load();
                Bullet.Factory.Load();
            }
            Game = new Game2( Container, gameInfo, playerInfo );
            return Game;
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
#if !UNITY_EDITOR
            Debug.LogFormat( "Stop Game" );
#endif
            Game.Dispose();
            Game = null;
            {
                PlayerCamera.Factory.Unload();
                PlayerCharacter.Factory.Unload();
                EnemyCharacter.Factory.Unload();
                Gun.Factory.Unload();
                Bullet.Factory.Unload();
                Array.Clear( Utils.RaycastHitBuffer, 0, Utils.RaycastHitBuffer.Length );
                Array.Clear( Utils.ColliderBuffer, 0, Utils.ColliderBuffer.Length );
            }
        }

        public void OnFixedUpdate() {
            Game?.OnFixedUpdate();
        }
        public void OnUpdate() {
            Game?.OnUpdate();
        }
        public void OnLateUpdate() {
            Game?.OnLateUpdate();
        }

    }
}
