#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    internal class CameraInputProvider : ICameraInputProvider, IDisposable {

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
        public Player2 Player { get; }
        private InputActions_Camera InputProvider_ { get; }
        private InputActions_Camera.CameraActions InputProvider => InputProvider_.Camera;

        public CameraInputProvider(Player2 player) {
            Player = player;
            InputProvider_ = new InputActions_Camera();
        }
        public void Dispose() {
            InputProvider_.Dispose();
        }

        public PlayableCharacterBase GetTarget() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return Player.Character;
        }
        public Vector2 GetLookDelta() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Look.ReadValue<Vector2>();
        }
        public float GetZoomDelta() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Zoom.ReadValue<Vector2>().y;
        }

    }
}
