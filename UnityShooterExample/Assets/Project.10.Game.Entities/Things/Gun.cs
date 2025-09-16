#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public partial class Gun {
        public static class Factory {

            private static readonly PrefabListHandle<Gun> Prefabs = new PrefabListHandle<Gun>( new[] {
                R.Project.Game.Entities.Things.Value_Gun_Gray,
                R.Project.Game.Entities.Things.Value_Gun_Red,
                R.Project.Game.Entities.Things.Value_Gun_Green,
                R.Project.Game.Entities.Things.Value_Gun_Blue,
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static Gun Create() {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().Random(), null );
                return result;
            }
            public static Gun Create(Vector3 position, Quaternion rotation) {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().Random(), position, rotation, null );
                return result;
            }

        }
    }
    public partial class Gun : WeaponBase {

        private FireDelay FireDelay { get; } = new FireDelay( 0.25f );
        private FirePoint FirePoint { get; set; } = default!;

        protected override void Awake() {
            base.Awake();
            this.FirePoint = this.gameObject.RequireComponentInChildren<FirePoint>();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        public override bool TryFire(PlayerBase? player, CharacterBase character) {
            if (this.FireDelay.CanFire) {
                this.FireDelay.Fire();
                var bullet = Bullet.Factory.Create( player, character, this, this.FirePoint.transform.position, this.FirePoint.transform.rotation, 5 );
                Physics.IgnoreCollision( this.gameObject.RequireComponentInChildren<Collider>(), bullet.gameObject.RequireComponentInChildren<Collider>() );
                return true;
            }
            return false;
        }

    }
}
