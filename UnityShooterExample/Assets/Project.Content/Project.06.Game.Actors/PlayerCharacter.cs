#nullable enable
namespace Project.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public partial class PlayerCharacter {
        public static class Factory {
            public enum CharacterType {
                Gray,
                Red,
                Green,
                Blue
            }

            private static readonly PrefabListHandle<PlayerCharacter> Prefabs = new PrefabListHandle<PlayerCharacter>( new[] {
                R.Project.Game.Actors.Value_PlayerCharacter_Gray,
                R.Project.Game.Actors.Value_PlayerCharacter_Red,
                R.Project.Game.Actors.Value_PlayerCharacter_Green,
                R.Project.Game.Actors.Value_PlayerCharacter_Blue
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static PlayerCharacter Create(Vector3 position, Quaternion rotation, PlayerBase player, CharacterType type) {
                var result = GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
                result.Player = player;
                return result;
            }

        }
    }
    public partial class PlayerCharacter : PlayableCharacterBase {

        public PlayerBase Player { get; private set; } = default!;

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
            if (IsAlive) {
                if (Input != null) {
                    Move( Input.GetMoveVector(), Input.IsJumpPressed(), Input.IsCrouchPressed(), Input.IsAcceleratePressed() );
                    BodyAt( Input.GetBodyTarget() );
                    HeadAt( Input.GetHeadTarget() );
                    AimAt( Input.GetWeaponTarget() );
                    if (Input.IsAimPressed()) {

                    }
                    if (Input.IsFirePressed()) {
                        Weapon?.Fire( this, Player );
                    }
                    if (Input.IsInteractPressed( out var interactable )) {
                        if (interactable is WeaponBase weapon) {
                            Weapon = weapon;
                        } else {
                            Weapon = null;
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
