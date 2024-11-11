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
                state = value;
                OnStateChangeEvent?.Invoke( State );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        internal InputActions_Character CharacterInput { get; }
        internal InputActions_Camera CameraInput { get; }

        public PlayerCharacter? Character {
            get => character;
            internal set {
                CharacterInput.Disable();
                CameraInput.Disable();
                if (Character != null) {
                    Character.InputProvider = null;
                }
                if (Camera != null) {
                    Camera.InputProvider = null;
                    Camera.Target = null;
                }
                character = value;
                if (Character != null && Camera != null) {
                    Character.InputProvider = new CharacterInput( CharacterInput, Character, Camera );
                    Camera.InputProvider = new CameraInput( CameraInput );
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
                    Character.InputProvider = null;
                }
                if (Camera != null) {
                    Camera.InputProvider = null;
                    Camera.Target = null;
                }
                camera = value;
                if (Character != null && Camera != null) {
                    Character.InputProvider = new CharacterInput( CharacterInput, Character, Camera );
                    Camera.InputProvider = new CameraInput( CameraInput );
                    Camera.Target = Character;
                }
            }
        }

        public Player2(IDependencyContainer container, PlayerInfo info) : base( container ) {
            Info = info;
            State = PlayerState.Playing;
            CharacterInput = new InputActions_Character();
            CameraInput = new InputActions_Camera();
        }
        public override void Dispose() {
            CameraInput.Dispose();
            CharacterInput.Dispose();
            base.Dispose();
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
