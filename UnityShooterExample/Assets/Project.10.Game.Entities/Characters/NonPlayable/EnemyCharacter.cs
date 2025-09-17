#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class EnemyCharacter {
        public static class Factory {

            private static readonly PrefabListHandle<EnemyCharacter> Prefabs = new PrefabListHandle<EnemyCharacter>( new[] {
                R.Project.Game.Entities.Characters.NonPlayable.Value_EnemyCharacter_Gray,
                R.Project.Game.Entities.Characters.NonPlayable.Value_EnemyCharacter_Red,
                R.Project.Game.Entities.Characters.NonPlayable.Value_EnemyCharacter_Green,
                R.Project.Game.Entities.Characters.NonPlayable.Value_EnemyCharacter_Blue
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static EnemyCharacter Create(Vector3 position, Quaternion rotation) {
                var result = GameObject.Instantiate<EnemyCharacter>( Prefabs.GetValues().Random(), position, rotation );
                result.Weapon = Gun.Factory.Create();
                return result;
            }

        }
    }
    public partial class EnemyCharacter : NonPlayableCharacterBase {
        private readonly struct Environment_ {
            public PlayerCharacter? Player { get; init; }
        }

        private Environment_ Environment { get; set; }

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        protected override void Start() {
            base.Start();
        }
        protected override void FixedUpdate() {
            base.FixedUpdate();
            this.Environment = GetEnvironment( this.transform );
        }
        protected override void Update() {
            base.Update();
            if (this.IsAlive) {
                this.Move( Vector3.zero, false, false, false );
                this.BodyAt( GetBodyTarget( this.Environment ) );
                _ = this.HeadAt( GetHeadTarget( this.Environment ) );
                _ = this.AimAt( GetWeaponTarget( this.Environment ) );
                if (this.Environment.Player != null && this.Environment.Player.IsAlive) {
                    _ = this.Weapon?.TryFire( null, this );
                }
            }
        }
        protected override void LateUpdate() {
            base.LateUpdate();
        }

        // Helpers
        private static Environment_ GetEnvironment(Transform transform) {
            return new Environment_() {
                Player = Utils.OverlapSphere( transform.position, 8, Masks.Entity_Approximate, QueryTriggerInteraction.Ignore )
                    .Select( i => i.transform.root.GetComponent<PlayerCharacter>() )
                    .FirstOrDefault( i => i != null )
            };
        }
        // Helpers
        private static Vector3? GetBodyTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return null;
            }
            return null;
        }
        private static Vector3? GetHeadTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return environment.Player.transform.position;
            }
            return null;
        }
        private static Vector3? GetWeaponTarget(Environment_ environment) {
            if (environment.Player != null) {
                if (environment.Player.IsAlive) {
                    return environment.Player.transform.position + Vector3.up * 1.5f;
                }
                return null;
            }
            return null;
        }

    }
}
