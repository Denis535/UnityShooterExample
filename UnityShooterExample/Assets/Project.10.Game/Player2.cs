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
        private PlayerCamera? camera;

        public PlayerInfo Info { get; }

        public PlayerState State {
            get => state;
            internal set {
                if (state is PlayerState.None && value is PlayerState.Playing) {
                    state = value;
                    OnStateChangeEvent?.Invoke( this.State );
                    return;
                }
                if (state is PlayerState.Playing && value is PlayerState.Winner or PlayerState.Loser) {
                    state = value;
                    OnStateChangeEvent?.Invoke( this.State );
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
                this.CharacterInputProvider.IsEnabled = false;
                this.CameraInputProvider.IsEnabled = false;
                if (this.Character != null) {
                    this.Character.InputProvider = null;
                }
                character = value;
                if (this.Character != null) {
                    this.Character.InputProvider = this.CharacterInputProvider;
                }
            }
        }
        public PlayerCamera? Camera {
            get => camera;
            internal set {
                this.CharacterInputProvider.IsEnabled = false;
                this.CameraInputProvider.IsEnabled = false;
                if (this.Camera != null) {
                    this.Camera.InputProvider = null;
                }
                camera = value;
                if (this.Camera != null) {
                    this.Camera.InputProvider = this.CameraInputProvider;
                }
            }
        }

        public Player2(IDependencyContainer container, PlayerInfo info) : base( container ) {
            this.Info = info;
            this.State = PlayerState.Playing;
            this.CharacterInputProvider = new CharacterInputProvider( this );
            this.CameraInputProvider = new CameraInputProvider( this );
        }
        public override void Dispose() {
            this.CameraInputProvider.Dispose();
            this.CharacterInputProvider.Dispose();
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
        Winner,
        Loser
    }
}
