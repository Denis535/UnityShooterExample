#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Player2 : PlayerBase2 {

        private PlayerState state;
        private PlayerCharacter? character;
        private Camera2? camera;

        public PlayerInfo Info { get; }

        public PlayerState State {
            get => state;
            internal set {
                if (state is PlayerState.None && value is PlayerState.Playing) {
                    state = value;
                    OnStateChangeEvent?.Invoke( State );
                    return;
                }
                if (state is PlayerState.Playing && value is PlayerState.Won or PlayerState.Lost) {
                    state = value;
                    OnStateChangeEvent?.Invoke( State );
                    return;
                }
                throw Exceptions.Operation.InvalidOperationException( $"Transition from {state} to {value} is invalid" );
            }
        }
        public event Action<PlayerState>? OnStateChangeEvent;

        internal CharacterInputProvider CharacterInputProvider { get; }
        internal CameraInputProvider CameraInputProvider { get; }

        public PlayerCharacter? Character {
            get => character;
            internal set {
                CharacterInputProvider.IsEnabled = false;
                CameraInputProvider.IsEnabled = false;
                if (Character != null) {
                    Character.InputProvider = null;
                }
                character = value;
                if (Character != null) {
                    Character.InputProvider = CharacterInputProvider;
                }
            }
        }
        public Camera2? Camera {
            get => camera;
            internal set {
                CharacterInputProvider.IsEnabled = false;
                CameraInputProvider.IsEnabled = false;
                if (Camera != null) {
                    Camera.InputProvider = null;
                }
                camera = value;
                if (Camera != null) {
                    Camera.InputProvider = CameraInputProvider;
                }
            }
        }

        public Player2(IDependencyContainer container, PlayerInfo info) : base( container ) {
            Info = info;
            State = PlayerState.Playing;
            CharacterInputProvider = new CharacterInputProvider( this );
            CameraInputProvider = new CameraInputProvider( this );
        }
        public override void Dispose() {
            CameraInputProvider.Dispose();
            CharacterInputProvider.Dispose();
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
        None,
        Playing,
        Won,
        Lost
    }
}
