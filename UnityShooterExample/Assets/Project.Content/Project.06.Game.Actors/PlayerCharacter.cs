#nullable enable
namespace Project.Game {
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

            public static PlayerCharacter Create(Vector3 position, Quaternion rotation, CharacterType type) {
                var result = GameObject.Instantiate<PlayerCharacter>( Prefabs.GetValues()[ (int) type ], position, rotation );
                return result;
            }

        }
    }
    public partial class PlayerCharacter : PlayableCharacterBase {

        public ICharacterInputProvider? InputProvider { get; set; }

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
            if (InputProvider != null) {
                if (IsAlive) {
                    Move( InputProvider.GetMoveVector(), InputProvider.IsJumpPressed(), InputProvider.IsCrouchPressed(), InputProvider.IsAcceleratePressed() );
                    BodyAt( InputProvider.GetBodyTarget() );
                    HeadAt( InputProvider.GetHeadTarget() );
                    AimAt( InputProvider.GetWeaponTarget() );
                    if (InputProvider.IsAimPressed()) {

                    }
                    if (InputProvider.IsFirePressed( out var player )) {
                        Weapon?.Fire( this, player );
                    }
                    if (InputProvider.IsInteractPressed( out var interactable )) {
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
    public interface ICharacterInputProvider {
        Vector3 GetMoveVector();
        Vector3? GetBodyTarget();
        Vector3? GetHeadTarget();
        Vector3? GetWeaponTarget();
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed(out PlayerBase player);
        bool IsAimPressed();
        bool IsInteractPressed(out MonoBehaviour? interactable);
    }
}
