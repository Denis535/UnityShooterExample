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
                if (value != IsPaused) {
                    isPaused = value;
                    //Time.timeScale = IsPaused ? 0f : 1f;
                    OnPauseChangeEvent?.Invoke( IsPaused );
                }
            }
        }
        public event Action<bool>? OnPauseChangeEvent;

        public GameState State {
            get => state;
            private set {
                if (State is GameState.None && value is GameState.Playing) {
                    state = value;
                    OnStateChangeEvent?.Invoke( State );
                    return;
                }
                if (State is GameState.Playing && value is GameState.Completed) {
                    state = value;
                    OnStateChangeEvent?.Invoke( State );
                    return;
                }
                throw Exceptions.Operation.InvalidOperationException( $"Transition from {State} to {value} is invalid" );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;

        private bool IsDirty { get; set; }

        public Player2 Player { get; }
        public World World { get; }

        public Game2(IDependencyContainer container, GameInfo info, PlayerInfo playerInfo) : base( container ) {
            Info = info;
            IsPaused = false;
            State = GameState.Playing;
            IsDirty = false;
            Player = new Player2( container, playerInfo );
            World = container.RequireDependency<World>();
            {
                var point = World.PlayerPoints.First();
                Player.Character = SpawnPlayerCharacter( point, playerInfo.CharacterType );
                Player.Camera = Camera2.Factory.Create();
            }
            foreach (var point in World.EnemyPoints) {
                SpawnEnemyCharacter( point );
            }
            foreach (var point in World.ThingPoints) {
                SpawnThing( point );
            }
        }
        public override void Dispose() {
            //Time.timeScale = 1f;
            Player.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (State is GameState.Playing or GameState.Completed && !IsPaused && Cursor.lockState == CursorLockMode.Locked) {
                Player.CharacterInputProvider.IsEnabled =
                    Player.State is PlayerState.Playing or PlayerState.Winner or PlayerState.Loser &&
                    Player.Character != null && Player.Character.IsAlive &&
                    Player.Camera != null;
                Player.CameraInputProvider.IsEnabled =
                    Player.State is PlayerState.Playing or PlayerState.Winner or PlayerState.Loser &&
                    Player.Character != null &&
                    Player.Camera != null;
            } else {
                Player.CharacterInputProvider.IsEnabled = false;
                Player.CameraInputProvider.IsEnabled = false;
            }
        }
        public void OnLateUpdate() {
            if (State is GameState.Playing) {
                if (Player.State is PlayerState.Playing) {
                    if (IsDirty && IsLoser( Player )) {
                        OnLoser( Player );
                    }
                    if (IsDirty && IsWinner( Player )) {
                        OnWinner( Player );
                    }
                } else if (Player.State is PlayerState.Winner or PlayerState.Loser) {
                    OnCompleted();
                }
            }
            IsDirty = false;
        }

        protected virtual PlayerCharacter SpawnPlayerCharacter(PlayerPoint point, PlayerInfo.CharacterType_ type) {
            var character = PlayerCharacter.Factory.Create( point.transform.position, point.transform.rotation, (PlayerCharacter.Factory.CharacterType) type );
            character.OnDeathEvent += info => {
                IsDirty = true;
            };
            return character;
        }
        protected virtual void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacter.Factory.Create( point.transform.position, point.transform.rotation );
            character.OnDeathEvent += info => {
                IsDirty = true;
            };
        }
        protected virtual void SpawnThing(ThingPoint point) {
            var thing = Gun.Factory.Create( point.transform.position, point.transform.rotation );
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
            player.State = PlayerState.Winner;
        }
        protected virtual void OnLoser(Player2 player) {
            player.State = PlayerState.Loser;
        }
        protected virtual void OnCompleted() {
            State = GameState.Completed;
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
