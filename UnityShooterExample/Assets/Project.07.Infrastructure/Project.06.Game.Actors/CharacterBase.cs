#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract partial class CharacterBase : ActorBase, IDamageable {

        public bool IsAlive { get; private set; } = true;
        public event Action<DamageInfo>? OnDamageEvent;
        public event Action<DamageInfo>? OnDeathEvent;

        private MoveableBody MoveableBody { get; set; } = default!;
        private Rigidbody Rigidbody { get; set; } = default!;
        private GameObject Head { get; set; } = default!;
        private WeaponSocket WeaponSocket { get; set; } = default!;
        public WeaponBase? Weapon {
            get {
                if (WeaponSocket.transform.childCount > 0) {
                    return WeaponSocket.transform.GetChild( 0 ).gameObject.RequireComponent<WeaponBase>();
                } else {
                    return null;
                }
            }
            protected set {
                var prevWeapon = Weapon;
                if (prevWeapon != null) {
                    prevWeapon.gameObject.SetLayerRecursively( Layers.Entity );
                    prevWeapon.transform.SetParent( null, true );
                    prevWeapon.IsRigidbody = true;
                }
                if (value != null) {
                    value.gameObject.SetLayerRecursively( Layers.Entity_Exact );
                    value.transform.SetParent( WeaponSocket.transform, true );
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                    value.IsRigidbody = false;
                }
            }
        }

        protected override void Awake() {
            MoveableBody = gameObject.RequireComponent<MoveableBody>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Head = gameObject.transform.Require( "Head" ).gameObject;
            WeaponSocket = gameObject.RequireComponentInChildren<WeaponSocket>();
        }
        protected override void OnDestroy() {
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }
        protected virtual void LateUpdate() {
        }

        protected void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            MoveableBody.Move( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }

        protected void BodyAt(Vector3? target) {
            MoveableBody.LookAt( target );
        }

        protected bool HeadAt(Vector3? target) {
            return LookAt( Head.transform, target );
            static bool LookAt(Transform transform, Vector3? target) {
                var rotation = transform.localRotation;
                if (target != null) {
                    transform.LookAt( target.Value );
                    if (Check( transform.localRotation )) {
                        transform.localRotation = Quaternion.RotateTowards( rotation, transform.localRotation, 2 * 360 * Time.deltaTime );
                        return true;
                    }
                }
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
            static bool Check(Quaternion rotation) {
                var angles = rotation.eulerAngles;
                if (angles.x > 180) angles.x -= 360;
                if (angles.y > 180) angles.y -= 360;
                if (angles.x >= -100 && angles.x <= 100) {
                    if (angles.y >= -80 && angles.y <= 80) {
                        return true;
                    }
                }
                return false;
            }
        }

        protected bool AimAt(Vector3? target) {
            return LookAt( WeaponSocket.transform, target );
            static bool LookAt(Transform transform, Vector3? target) {
                var rotation = transform.localRotation;
                if (target != null) {
                    transform.LookAt( target.Value );
                    if (Check( transform.localRotation )) {
                        transform.localRotation = Quaternion.RotateTowards( rotation, transform.localRotation, 2 * 360 * Time.deltaTime );
                        return true;
                    }
                }
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
            static bool Check(Quaternion rotation) {
                var angles = rotation.eulerAngles;
                if (angles.x > 180) angles.x -= 360;
                if (angles.y > 180) angles.y -= 360;
                if (angles.x >= -100 && angles.x <= 100) {
                    if (angles.y >= -80 && angles.y <= 80) {
                        return true;
                    }
                }
                return false;
            }
        }

        void IDamageable.Damage(DamageInfo info) {
            {
                OnDamage( info );
                OnDamageEvent?.Invoke( info );
            }
            if (IsAlive) {
                IsAlive = false;
                OnDeath( info );
                OnDeathEvent?.Invoke( info );
            }
        }
        protected virtual void OnDamage(DamageInfo info) {
        }
        protected virtual void OnDeath(DamageInfo info) {
            Weapon = null;
            gameObject.SetLayerRecursively( Layers.Entity );
            MoveableBody.enabled = false;
            Rigidbody.isKinematic = false;
            if (info is HitDamageInfo bulletDamageInfo) {
                Rigidbody.AddForceAtPosition( bulletDamageInfo.Direction * 5, bulletDamageInfo.Point, ForceMode.Impulse );
            }
        }

    }
}
