#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;

    internal class CharacterInputProvider : ICharacterInputProvider, IDisposable {

        public bool IsEnabled {
            get => Actions.enabled;
            set {
                if (value) {
                    Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
                    Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
                    Actions.Enable();
                } else {
                    Actions.Disable();
                }
            }
        }
        private InputActions_Character Actions_ { get; }
        private InputActions_Character.CharacterActions Actions => Actions_.Character;
        private Player2 Player { get; }
        private PlayerCharacter Character => Player.Character!;
        private Camera2 Camera => Player.Camera!;

        private Camera2.RaycastHit? Hit => Player.Camera!.Hit;
        private Vector3 Target => Player.Camera!.Hit?.Point ?? Player.Camera.transform.TransformPoint( Vector3.forward * 128f );

        public CharacterInputProvider(Player2 player) {
            Actions_ = new InputActions_Character();
            Player = player;
        }
        public void Dispose() {
            Actions_.Dispose();
        }

        public Vector3 GetMoveVector() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Move.IsPressed()) {
                var vector = Actions.Move.ReadValue<Vector2>().Pipe( i => new Vector3( i.x, 0, i.y ) );
                vector = Camera.transform.TransformDirection( vector );
                vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public Vector3? GetBodyTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return Target;
            }
            if (Actions.Move.IsPressed()) {
                var vector = Actions.Move.ReadValue<Vector2>().Pipe( i => new Vector3( i.x, 0, i.y ) );
                if (vector != Vector3.zero) {
                    vector = Camera.transform.TransformDirection( vector );
                    vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                    return Character.transform.position + vector;
                }
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return Target;
            }
            if (Actions.Move.IsPressed()) {
                return Target;
            }
            return Target;
        }
        public Vector3? GetWeaponTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return Target;
            }
            if (Actions.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public bool IsJumpPressed() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Accelerate.IsPressed();
        }
        public bool IsFirePressed(out PlayerBase player) {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            player = Player;
            return Actions.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Aim.IsPressed();
        }
        public bool IsInteractPressed(out MonoBehaviour? interactable) {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            interactable = (MonoBehaviour?) Hit?.Enemy ?? Hit?.Thing;
            return Actions.Interact.WasPressedThisFrame();
        }

    }
}
