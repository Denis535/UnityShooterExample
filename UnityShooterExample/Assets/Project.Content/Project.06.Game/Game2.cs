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

        public GameState State {
            get => state;
            private set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;

        public bool IsPaused {
            get => isPaused;
            set {
                if (value != isPaused) {
                    isPaused = value;
                    //Time.timeScale = isPaused ? 0f : 1f;
                    OnPauseChangeEvent?.Invoke( isPaused );
                }
            }
        }
        public event Action<bool>? OnPauseChangeEvent;
        
        private bool IsDirty { get; set; }

        public Player2 Player { get; }
        public World World { get; }

        public Game2(IDependencyContainer container, GameInfo info, PlayerInfo playerInfo) : base( container ) {
            Info = info;
            Player = new Player2( container, playerInfo );
            World = container.RequireDependency<World>();
            {
                var point = World.PlayerPoints.First();
                Player.Character = SpawnPlayerCharacter( point, Player );
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
            Player.OnFixedUpdate();
        }
        public void OnUpdate() {
            Player.OnUpdate();
        }
        public void OnLateUpdate() {
            Player.OnLateUpdate();
            if (IsDirty) {
                if (IsLoser()) {
                    OnLoser();
                }
                if (IsWinner()) {
                    OnWinner();
                }
                IsDirty = false;
            }
        }

        protected PlayerCharacter SpawnPlayerCharacter(PlayerPoint point, Player2 player) {
            var character = PlayerCharacter.Factory.Create( point.transform.position, point.transform.rotation, player, (PlayerCharacter.Factory.CharacterType) player.Info.CharacterType );
            character.OnDeathEvent += info => {
                IsDirty = true;
            };
            return character;
        }
        protected void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacter.Factory.Create( point.transform.position, point.transform.rotation );
            character.OnDeathEvent += info => {
                IsDirty = true;
            };
        }
        protected void SpawnThing(ThingPoint point) {
            var thing = Gun.Factory.Create( point.transform.position, point.transform.rotation );
        }

        protected bool IsWinner() {
            if (State is GameState.Playing) {
                var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                if (enemies.All( i => !i.IsAlive )) {
                    return true;
                }
            }
            return false;
        }
        protected bool IsLoser() {
            if (State is GameState.Playing) {
                if (!Player.Character!.IsAlive) {
                    return true;
                }
            }
            return false;
        }

        protected void OnWinner() {
            Player.State = PlayerState.Winner;
            State = GameState.Completed;
        }
        protected void OnLoser() {
            Player.State = PlayerState.Loser;
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
        Playing,
        Completed
    }
}
