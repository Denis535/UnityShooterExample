#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public partial class Bullet {
        public static class Factory {

            private static readonly PrefabHandle<Bullet> Prefab = new PrefabHandle<Bullet>( R.Project.Game.Entities.Things.Value_Bullet );

            public static void Load() {
                Prefab.Load().Wait();
            }
            public static void Unload() {
                Prefab.Release();
            }

            public static Bullet Create(PlayerBase? player, CharacterBase character, WeaponBase weapon, Vector3 position, Quaternion rotation, float force) {
                var result = GameObject.Instantiate<Bullet>( Prefab.GetValue(), position, rotation, null );
                result.Rigidbody.AddForce( result.transform.forward * force, ForceMode.Impulse );
                result.Player = player;
                result.Character = character;
                result.Weapon = weapon;
                result.Force = force;
                GameObject.Destroy( result.gameObject, 10 );
                return result;
            }

        }
    }
    public partial class Bullet : EntityBase {

        private Rigidbody Rigidbody { get; set; } = default!;
        public PlayerBase? Player { get; private set; } = default!;
        public CharacterBase Character { get; private set; } = default!;
        public WeaponBase Weapon { get; private set; } = default!;
        public float Force { get; private set; } = default!;

        protected override void Awake() {
            this.Rigidbody = this.gameObject.RequireComponent<Rigidbody>();
        }
        protected override void OnDestroy() {
        }

        public void OnCollisionEnter(Collision collision) {
            if (this.enabled) {
                var damageInfo = new HitDamageInfo( this.Player, this.Weapon, this.Force, this.Rigidbody.position, this.Rigidbody.linearVelocity.normalized, this.transform.position );
                if (collision.transform.root != this.Character.transform.root) {
                    _ = collision.gameObject.TryDamage( damageInfo, out _ );
                }
                this.enabled = false;
            }
        }

    }
}
