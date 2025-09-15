#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Game2 : GameBase2 {

        private GameState state;
        private bool isPaused;

        public GameInfo Info { get; }

        public bool IsPaused {
            get => isPaused;
            set {
                if (value != this.IsPaused) {
                    isPaused = value;
                    //Time.timeScale = IsPaused ? 0f : 1f;
                    OnPauseChangeEvent?.Invoke( this.IsPaused );
                }
            }
        }
        public event Action<bool>? OnPauseChangeEvent;

        public GameState State {
            get => state;
            private set {
                if (this.State is GameState.None && value is GameState.Playing) {
                    state = value;
                    OnStateChangeEvent?.Invoke( this.State );
                    return;
                }
                if (this.State is GameState.Playing && value is GameState.Completed) {
                    state = value;
                    OnStateChangeEvent?.Invoke( this.State );
                    return;
                }
                throw Exceptions.Operation.InvalidOperationException( $"Transition from {this.State} to {value} is invalid" );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;

        public Player2 Player { get; }
        public World World { get; }

        private bool IsDirty { get; set; }

        public Game2(IDependencyContainer container, GameInfo info, PlayerInfo playerInfo) : base( container ) {
            this.Info = info;
            this.IsPaused = false;
            this.State = GameState.Playing;
            this.Player = new Player2( container, playerInfo );
            this.World = container.RequireDependency<World>();
            {
                var point = this.World.PlayerPoints.Random();
                this.Player.Character = this.SpawnPlayerCharacter( point, playerInfo.CharacterType );
                this.Player.Camera = this.SpawnPlayerCamera();
            }
            foreach (var point in this.World.EnemyPoints) {
                this.SpawnEnemyCharacter( point );
            }
            foreach (var point in this.World.ThingPoints) {
                this.SpawnThing( point );
            }
            this.IsDirty = false;
        }
        public override void Dispose() {
            //Time.timeScale = 1f;
            this.Player.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (this.State is GameState.Playing or GameState.Completed && !this.IsPaused && Cursor.lockState == CursorLockMode.Locked) {
                this.Player.CharacterInputProvider.IsEnabled =
                   this.Player.State is PlayerState.Playing or PlayerState.Winner or PlayerState.Loser &&
                   this.Player.Character != null && this.Player.Character.IsAlive &&
                   this.Player.Camera != null;
                this.Player.CameraInputProvider.IsEnabled =
                    this.Player.State is PlayerState.Playing or PlayerState.Winner or PlayerState.Loser &&
                    this.Player.Character != null &&
                    this.Player.Camera != null;
            } else {
                this.Player.CharacterInputProvider.IsEnabled = false;
                this.Player.CameraInputProvider.IsEnabled = false;
            }
        }
        public void OnLateUpdate() {
            if (this.State is GameState.Playing) {
                if (this.IsDirty) {
                    if (this.Player.State is PlayerState.Playing) {
                        if (this.IsLoser( this.Player )) {
                            this.Player.State = PlayerState.Loser;
                            this.OnLoser( this.Player );
                        } else if (this.IsWinner( this.Player )) {
                            this.Player.State = PlayerState.Winner;
                            this.OnWinner( this.Player );
                        }
                    }
                    if (this.Player.State is PlayerState.Winner or PlayerState.Loser) {
                        this.State = GameState.Completed;
                        this.OnCompleted();
                    }
                    this.IsDirty = false;
                }
            }
        }

        protected virtual PlayerCharacter SpawnPlayerCharacter(PlayerPoint point, PlayerInfo.CharacterType_ type) {
            var character = PlayerCharacter.Factory.Create( point.transform.position, point.transform.rotation, (PlayerCharacter.Factory.CharacterType) type );
            character.OnDeathEvent += info => {
                this.IsDirty = true;
            };
            return character;
        }
        protected virtual PlayerCamera SpawnPlayerCamera() {
            var camera = PlayerCamera.Factory.Create();
            return camera;
        }
        protected virtual void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacter.Factory.Create( point.transform.position, point.transform.rotation );
            character.OnDeathEvent += info => {
                this.IsDirty = true;
            };
        }
        protected virtual void SpawnThing(ThingPoint point) {
            _ = Gun.Factory.Create( point.transform.position, point.transform.rotation );
        }

        protected virtual bool IsWinner(Player2 player) {
            var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            if (enemies.All( i => !i.IsAlive )) {
                return true;
            }
            return false;
        }
        protected virtual bool IsLoser(Player2 player) {
            if (!player.Character!.IsAlive) {
                return true;
            }
            return false;
        }

        protected virtual void OnWinner(Player2 player) {
        }
        protected virtual void OnLoser(Player2 player) {
        }
        protected virtual void OnCompleted() {
        }

    }
    public record GameInfo(string Name, GameInfo.Mode_ Mode, GameInfo.Level_ Level) {
        public enum Mode_ {
            None
        }
        public enum Level_ {
            Level1,
            Level2,
            Level3
        }
    }
    public static class GameInfoExtensions {
        public static bool IsLast(this GameInfo.Level_ level) {
            return level == GameInfo.Level_.Level3;
        }
        public static GameInfo.Level_ GetNext(this GameInfo.Level_ level) {
            Assert.Operation.Message( $"Level {level} must be non-last" ).Valid( level != GameInfo.Level_.Level3 );
            return level + 1;
        }
    }
    public enum GameState {
        None,
        Playing,
        Completed
    }
}
