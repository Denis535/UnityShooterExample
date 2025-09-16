#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;

    internal class PlayableCharacterInputProvider : IPlayableCharacterInputProvider, IDisposable {

        public bool IsEnabled {
            get => this.InputProvider.enabled;
            set {
                if (value) {
                    Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
                    Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
                    this.InputProvider.Enable();
                } else {
                    this.InputProvider.Disable();
                }
            }
        }
        private Player2 Player { get; }
        private CharacterInputProvider InputProvider_ { get; }
        private CharacterInputProvider.CharacterActions InputProvider => this.InputProvider_.Character;

        public PlayableCharacterInputProvider(Player2 player) {
            this.Player = player;
            this.InputProvider_ = new CharacterInputProvider();
        }
        public void Dispose() {
            this.InputProvider_.Dispose();
        }

        public Vector3? GetBodyTarget() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            if (this.InputProvider.Aim.IsPressed() || this.InputProvider.Fire.IsPressed()) {
                return GetLookTarget( this.Player.Camera );
            }
            if (this.InputProvider.Move.IsPressed()) {
                var vector = this.InputProvider.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( this.Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return this.Player.Character.transform.position + vector;
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            if (this.InputProvider.Aim.IsPressed() || this.InputProvider.Fire.IsPressed()) {
                return GetLookTarget( this.Player.Camera );
            }
            if (this.InputProvider.Move.IsPressed()) {
                return GetLookTarget( this.Player.Camera );
            }
            return GetLookTarget( this.Player.Camera );
        }
        public Vector3? GetWeaponTarget() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            if (this.InputProvider.Aim.IsPressed() || this.InputProvider.Fire.IsPressed()) {
                return GetLookTarget( this.Player.Camera );
            }
            if (this.InputProvider.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public Vector3 GetMoveVector() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            if (this.InputProvider.Move.IsPressed()) {
                var vector = this.InputProvider.Move.ReadValue<Vector2>()
                    .Pipe( i => new Vector3( i.x, 0, i.y ) )
                    .Pipe( this.Player.Camera.transform.TransformDirection )
                    .Pipe( i => new Vector3( i.x, 0, i.z ).normalized * i.magnitude );
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public bool IsJumpPressed() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Accelerate.IsPressed();
        }
        public bool IsFirePressed(out PlayerBase player) {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            player = this.Player;
            return this.InputProvider.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Aim.IsPressed();
        }
        public bool IsInteractPressed(out EntityBase? interactable) {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            interactable = this.Player.Camera.Hit?.Entity;
            return this.InputProvider.Interact.WasPressedThisFrame();
        }

        // Helpers
        private static Vector3 GetLookTarget(PlayerCamera camera) {
            return camera.Hit?.Point ?? camera.transform.TransformPoint( Vector3.forward * 128f );
        }

    }
}
