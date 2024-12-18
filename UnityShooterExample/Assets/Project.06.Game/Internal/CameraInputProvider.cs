#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using CameraInputProvider2 = UnityEngine.InputSystem.CameraInputProvider;

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
        private CameraInputProvider2 InputProvider_ { get; }
        private CameraInputProvider2.CameraActions InputProvider => InputProvider_.Camera;

        public CameraInputProvider(Player2 player) {
            Player = player;
            InputProvider_ = new CameraInputProvider2();
        }
        public void Dispose() {
            InputProvider_.Dispose();
        }

        public PlayableCharacterBase GetTarget() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return Player.Character;
        }
        public Vector2 GetRotate() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Look.ReadValue<Vector2>();
        }
        public float GetZoom() {
            Assert.Operation.Message( $"Player {Player} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {Player} must have camera" ).Valid( Player.Camera != null );
            return InputProvider.Zoom.ReadValue<Vector2>().y;
        }

    }
}
