#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.InputSystem;

    public class Player2 : PlayerBase2 {

        private PlayerState state;
        private PlayerCharacter? character;
        private Camera2? camera;

        public PlayerInfo Info { get; }

        public PlayerState State {
            get => state;
            internal set {
                Assert.Operation.Message( $"Transition from {State} to {value} is invalid" ).Valid( value != State );
                state = value;
                OnStateChangeEvent?.Invoke( State );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        private InputActions_Character CharacterInput { get; } = new InputActions_Character();
        private InputActions_Camera CameraInput { get; } = new InputActions_Camera();

        public PlayerCharacter? Character {
            get => character;
            internal set {
                CharacterInput.Disable();
                CameraInput.Disable();
                if (Character != null) {
                    Character.Input = null;
                }
                if (Camera != null) {
                    Camera.Input = null;
                    Camera.Target = null;
                }
                character = value;
                if (Character != null && Camera != null) {
                    Character.Input = new CharacterInput( CharacterInput, Character, Camera );
                    Camera.Input = new CameraInput( CameraInput );
                    Camera.Target = Character;
                }
            }
        }
        public Camera2? Camera {
            get => camera;
            internal set {
                CharacterInput.Disable();
                CameraInput.Disable();
                if (Character != null) {
                    Character.Input = null;
                }
                if (Camera != null) {
                    Camera.Input = null;
                    Camera.Target = null;
                }
                camera = value;
                if (Character != null && Camera != null) {
                    Character.Input = new CharacterInput( CharacterInput, Character, Camera );
                    Camera.Input = new CameraInput( CameraInput );
                    Camera.Target = Character;
                }
            }
        }

        public Player2(IDependencyContainer container, PlayerInfo info) : base( container ) {
            Info = info;
        }
        public override void Dispose() {
            CharacterInput.Dispose();
            CameraInput.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (Character != null && Character.IsAlive && Camera != null && Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f) {
                CharacterInput.Enable();
            } else {
                CharacterInput.Disable();
            }
            if (Character != null && Camera != null && Cursor.lockState == CursorLockMode.Locked && Time.timeScale != 0f) {
                CameraInput.Enable();
            } else {
                CameraInput.Disable();
            }
        }
        public void OnLateUpdate() {
        }

    }
    public record PlayerInfo(string Name, PlayerInfo.CharacterType_ CharacterType) {
        public enum CharacterType_ {
            Gray,
            Red,
            Green,
            Blue
        }
    }
    public enum PlayerState {
        Playing,
        Winner,
        Loser
    }
}
