#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    internal class CameraInputProvider : ICameraInputProvider, IDisposable {

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
        private InputActions_Camera Actions_ { get; }
        private InputActions_Camera.CameraActions Actions => Actions_.Camera;
        public Player2 Player { get; }
        private PlayerCharacter Character => Player.Character!;
        private Camera2 Camera => Player.Camera!;

        public CameraInputProvider(Player2 player) {
            Actions_ = new InputActions_Camera();
            Player = player;
        }
        public void Dispose() {
            Actions_.Dispose();
        }

        public PlayableCharacterBase GetTarget(out bool isChanged) {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            isChanged = false;
            return Character;
        }

        public Vector2 GetLookDelta() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Look.ReadValue<Vector2>();
        }
        public float GetZoomDelta() {
            Assert.Operation.Message( $"Player {this} must have character" ).Valid( Player.Character != null );
            Assert.Operation.Message( $"Player {this} must have camera" ).Valid( Player.Camera != null );
            return Actions.Zoom.ReadValue<Vector2>().y;
        }

    }
}
