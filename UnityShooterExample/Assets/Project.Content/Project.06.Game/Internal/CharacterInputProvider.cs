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
        private Player2 Player { get; }
        private InputActions_Character Actions_ { get; }
        private InputActions_Character.CharacterActions Actions => Actions_.Character;

        public CharacterInputProvider(Player2 player) {
            Player = player;
            Actions_ = new InputActions_Character();
        }
        public void Dispose() {
            Actions_.Dispose();
        }

        public Vector3 GetMoveVector() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Move.IsPressed()) {
                var vector = Actions.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public Vector3? GetBodyTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return GetTarget( Player.Camera );
            }
            if (Actions.Move.IsPressed()) {
                var vector = Actions.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return Player.Character.transform.position + vector;
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return GetTarget( Player.Camera );
            }
            if (Actions.Move.IsPressed()) {
                return GetTarget( Player.Camera );
            }
            return GetTarget( Player.Camera );
        }
        public Vector3? GetWeaponTarget() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            if (Actions.Aim.IsPressed() || Actions.Fire.IsPressed()) {
                return GetTarget( Player.Camera );
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
        public bool IsInteractPressed(out EntityBase? interactable) {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            interactable = GetInteractable( Player.Camera );
            return Actions.Interact.WasPressedThisFrame();
        }

        // Helpers
        private static Vector3 GetTarget(Camera2 camera) {
            return camera.Hit?.Point ?? camera.transform.TransformPoint( Vector3.forward * 128f );
        }
        private static EntityBase? GetInteractable(Camera2 camera) {
            return (EntityBase?) camera.Hit?.Enemy ?? camera.Hit?.Thing;
        }

    }
}
