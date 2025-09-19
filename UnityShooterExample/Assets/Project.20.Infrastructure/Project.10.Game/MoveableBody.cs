#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Note: Character consists of approximate capsule-collider and exact colliders (body, head, hands, legs, weapon, etc).
    [RequireComponent( typeof( CharacterController ) )]
    public class MoveableBody : MonoBehaviour {

        private bool fixedUpdateWasInvoked;

        private static LayerMask ExcludeLayers_Default => ~(Masks.Entity_Approximate);
        private static LayerMask ExcludeLayers_WhenMoving => (Masks.Entity_Exact | Masks.Trivial);

        private CharacterController Collider { get; set; } = default!;
        public Vector3 MoveVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }
        public bool IsAcceleratePressed { get; private set; }
        public Quaternion? LookRotation { get; private set; }

        protected void Awake() {
            this.gameObject.SetLayerRecursively( Layers.Entity_Approximate, Layers.Entity_Exact );
            this.Collider = this.gameObject.RequireComponent<CharacterController>();
            this.Collider.excludeLayers = ExcludeLayers_Default;
        }
        protected void OnDestroy() {
        }

        protected void OnEnable() {
            this.Collider.enabled = true;
        }
        protected void OnDisable() {
            this.Collider.enabled = false;
        }

        protected void FixedUpdate() {
            fixedUpdateWasInvoked = true;
            var velocity = Vector3.zero;
            if (this.MoveVector != Vector3.zero) {
                if (this.IsAcceleratePressed) {
                    velocity += this.MoveVector * 13;
                } else {
                    velocity += this.MoveVector * 5;
                }
            }
            if (this.IsJumpPressed) {
                if (this.IsAcceleratePressed) {
                    velocity += Vector3.up * 13;
                } else {
                    velocity += Vector3.up * 5;
                }
            } else
            if (this.IsCrouchPressed) {
                if (this.IsAcceleratePressed) {
                    velocity -= Vector3.up * 13;
                } else {
                    velocity -= Vector3.up * 5;
                }
            }
            this.Collider.excludeLayers = ExcludeLayers_WhenMoving;
            _ = this.Collider.Move( velocity * Time.fixedDeltaTime );
            this.Collider.excludeLayers = ExcludeLayers_Default;
        }
        protected void Update() {
        }

        public void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Assert.Operation.Message( $"Method 'Move' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( this.didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( this.enabled );
            if (fixedUpdateWasInvoked) {
                fixedUpdateWasInvoked = false;
                this.MoveVector = moveVector;
                this.IsJumpPressed = isJumpPressed;
                this.IsCrouchPressed = isCrouchPressed;
                this.IsAcceleratePressed = isAcceleratePressed;
            } else {
                this.MoveVector = Vector3.Max( this.MoveVector, moveVector );
                this.IsJumpPressed |= isJumpPressed;
                this.IsCrouchPressed |= isCrouchPressed;
                this.IsAcceleratePressed |= isAcceleratePressed;
            }
        }

        public void LookAt(Quaternion? rotation) {
            Assert.Operation.Message( $"Method 'LookAt' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( this.didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( this.enabled );
            this.LookRotation = rotation;
            if (this.LookRotation != null) {
                this.transform.localRotation = Quaternion.RotateTowards( this.transform.localRotation, this.LookRotation.Value, 3 * 360 * Time.deltaTime );
            }
        }

        public void LookAt(Vector3? target) {
            Assert.Operation.Message( $"Method 'LookAt' must be invoked only within update" ).Valid( !Time.inFixedTimeStep );
            Assert.Operation.Message( $"MoveableBody {this} must be awakened" ).Ready( this.didAwake );
            Assert.Operation.Message( $"MoveableBody {this} must not be disposed" ).NotDisposed( this );
            Assert.Operation.Message( $"MoveableBody {this} must be enabled" ).Valid( this.enabled );
            if (target != null) {
                this.LookAt( GetRotation( this.transform.position, target.Value ) );
            } else {
                this.LookAt( (Quaternion?) null );
            }
        }

        protected void OnControllerColliderHit(ControllerColliderHit hit) {
            hit.rigidbody?.WakeUp();
        }

        // Helpers
        private static Vector3 GetDirection(Vector3 position, Vector3 target) {
            var direction = target - position;
            direction = new Vector3( direction.x, 0, direction.z );
            direction = direction.normalized;
            return direction;
        }
        private static Quaternion GetRotation(Vector3 position, Vector3 target) {
            var direction = GetDirection( position, target );
            return Quaternion.LookRotation( direction, Vector3.up );
        }

    }
}
