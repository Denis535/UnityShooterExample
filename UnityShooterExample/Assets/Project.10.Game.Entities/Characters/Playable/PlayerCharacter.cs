#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class PlayerCharacter {
        public static class Factory {
            public enum CharacterType {
                Gray,
                Red,
                Green,
                Blue
            }

            private static readonly PrefabListHandle<PlayerCharacter> Prefabs = new PrefabListHandle<PlayerCharacter>( new[] {
                R.Project.Game.Entities.Characters.Playable.Value_PlayerCharacter_Gray,
                R.Project.Game.Entities.Characters.Playable.Value_PlayerCharacter_Red,
                R.Project.Game.Entities.Characters.Playable.Value_PlayerCharacter_Green,
                R.Project.Game.Entities.Characters.Playable.Value_PlayerCharacter_Blue
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static PlayerCharacter Create(Vector3 position, Quaternion rotation, CharacterType type) {
                var result = GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
                return result;
            }

        }
    }
    public partial class PlayerCharacter : PlayableCharacterBase {

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
        }
        protected override void Update() {
            base.Update();
            if (this.InputProvider != null) {
                if (this.IsAlive) {
                    this.Move( this.InputProvider.GetMoveVector(), this.InputProvider.IsJumpPressed(), this.InputProvider.IsCrouchPressed(), this.InputProvider.IsAcceleratePressed() );
                    this.BodyAt( this.InputProvider.GetBodyTarget() );
                    _ = this.HeadAt( this.InputProvider.GetHeadTarget() );
                    _ = this.AimAt( this.InputProvider.GetWeaponTarget() );
                    if (this.InputProvider.IsAimPressed()) {
                    }
                    if (this.InputProvider.IsFirePressed( out var player )) {
                        _ = this.Weapon?.TryFire( player, this );
                    }
                    if (this.InputProvider.IsInteractPressed( out var interactable )) {
                        if (interactable is WeaponBase weapon) {
                            this.Weapon = weapon;
                        } else {
                            this.Weapon = null;
                        }
                    }
                }
            }
        }
        protected override void LateUpdate() {
            base.LateUpdate();
        }

    }
}
