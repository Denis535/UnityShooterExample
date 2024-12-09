#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using CharacterInputProvider2 = UnityEngine.InputSystem.CharacterInputProvider;

    internal class CharacterInputProvider : ICharacterInputProvider, IDisposable {

        public bool IsEnabled {
            get => InputProvider.enabled;
            set {
                if (value) {
                    Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
                    Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
                    InputProvider.Enable();
                } else {
                    InputProvider.Disable();
                }
            }
        }
        private Player2 Player { get; }
        private CharacterInputProvider2 InputProvider_ { get; }
        private CharacterInputProvider2.CharacterActions InputProvider => InputProvider_.Character;

        public CharacterInputProvider(Player2 player) {
            Player = player;
            InputProvider_ = new CharacterInputProvider2();
        }
        public void Dispose() {
            InputProvider_.Dispose();
        }

        public Vector3? GetBodyTarget() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            if (InputProvider.Aim.IsPressed() || InputProvider.Fire.IsPressed()) {
                return GetLookTarget( Player.Camera );
            }
            if (InputProvider.Move.IsPressed()) {
                var vector = InputProvider.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return Player.Character.transform.position + vector;
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            if (InputProvider.Aim.IsPressed() || InputProvider.Fire.IsPressed()) {
                return GetLookTarget( Player.Camera );
            }
            if (InputProvider.Move.IsPressed()) {
                return GetLookTarget( Player.Camera );
            }
            return GetLookTarget( Player.Camera );
        }
        public Vector3? GetWeaponTarget() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            if (InputProvider.Aim.IsPressed() || InputProvider.Fire.IsPressed()) {
                return GetLookTarget( Player.Camera );
            }
            if (InputProvider.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public Vector3 GetMoveVector() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            if (InputProvider.Move.IsPressed()) {
                var vector = InputProvider.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public bool IsJumpPressed() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Accelerate.IsPressed();
        }
        public bool IsFirePressed(out PlayerBase player) {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            player = Player;
            return InputProvider.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Aim.IsPressed();
        }
        public bool IsInteractPressed(out EntityBase? interactable) {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            interactable = Player.Camera.Hit?.Entity;
            return InputProvider.Interact.WasPressedThisFrame();
        }

        // Helpers
        private static Vector3 GetLookTarget(Camera2 camera) {
            return camera.Hit?.Point ?? camera.transform.TransformPoint( Vector3.forward * 128f );
        }

    }
}
