#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    internal class PlayableCameraInputProvider : IPlayableCameraInputProvider, IDisposable {

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
        public Player2 Player { get; }
        private CameraInputProvider InputProvider_ { get; }
        private CameraInputProvider.CameraActions InputProvider => this.InputProvider_.Camera;

        public PlayableCameraInputProvider(Player2 player) {
            this.Player = player;
            this.InputProvider_ = new CameraInputProvider();
        }
        public void Dispose() {
            this.InputProvider_.Dispose();
        }

        public PlayableCharacterBase GetTarget() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.Player.Character;
        }
        public Vector2 GetRotateDelta() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Rotate.ReadValue<Vector2>().Pipe( i => new Vector2( -i.y, i.x ) );
        }
        public float GetZoomDelta() {
            Assert.Operation.Message( $"Player {this.Player} must have character" ).Valid( this.Player.Character != null );
            Assert.Operation.Message( $"Player {this.Player} must have camera" ).Valid( this.Player.Camera != null );
            return this.InputProvider.Zoom.ReadValue<Vector2>().y;
        }

    }
}
