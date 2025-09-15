#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public abstract class PlayListBase : DisposableBase {

        // State
        public State<PlayListBase> State { get; }
        // Theme
        protected ThemeBase Theme {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                Assert.Operation.Message( $"PlayList {this} must be active or activating or deactivating" ).Valid( State.Activity is Activity.Active or Activity.Activating or Activity.Deactivating );
                return ((StateMachine<State<PlayListBase>, ThemeBase>?) State.Machine)!.UserData;
            }
        }

        // IsRunning
        protected bool IsRunning {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return Theme!.IsRunning;
            }
        }
        protected bool IsPlaying {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return Theme!.IsPlaying;
            }
        }
        protected bool IsPaused {
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                Theme!.IsPaused = value;
            }
        }
        protected bool Mute {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return Theme!.Mute;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                Theme!.Mute = value;
            }
        }
        protected float Volume {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return Theme!.Volume;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                Theme!.Volume = value;
            }
        }
        protected float Pitch {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return Theme!.Pitch;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                Theme!.Pitch = value;
            }
        }

        // Constructor
        public PlayListBase() {
            State = new State<PlayListBase>( this );
            State.OnActivateCallback += OnActivate;
            State.OnDeactivateCallback += OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"PlayList {this} must be inactive" ).Valid( State.Activity is Activity.Inactive );
            base.Dispose();
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        // Play
        protected void Play(AudioClip clip) {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be non-running" ).Valid( !IsRunning );
            Theme!.Play( clip );
        }
        protected Task PlayAndWaitForCompletionAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be non-running" ).Valid( !IsRunning );
            return Theme!.PlayAndWaitForCompletionAsync( clip, cancellationToken );
        }
        protected void Stop() {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be running" ).Valid( IsRunning );
            Theme!.Stop();
        }

        // Helpers
        protected static void Shuffle<T>(T[] array) {
            for (int i = 0, j = array.Length; i < array.Length; i++, j--) {
                var rnd = i + UnityEngine.Random.Range( 0, j );
                (array[ i ], array[ rnd ]) = (array[ rnd ], array[ i ]);
            }
        }
        protected static T GetNext<T>(T[] array, T? value) {
            var index = Array.IndexOf( array, value );
            if (index != -1) {
                index = (index + 1) % array.Length;
                return array[ index ];
            }
            return array[ 0 ];
        }
        protected static T GetNextRandom<T>(T[] array, T? value) where T : class {
            var index = UnityEngine.Random.Range( 0, array.Length );
            if (index != -1) {
                if (array.Length >= 2 && array[ index ] == value) {
                    return GetNextRandom( array, value );
                }
                return array[ index ];
            }
            return array[ 0 ];
        }

    }
    public static class StateExtensions {
        public static PlayListBase PlayList(this StateBase state) {
            return ((State<PlayListBase>) state).UserData;
        }
        public static T PlayList<T>(this StateBase state) where T : PlayListBase {
            return (T) ((State<PlayListBase>) state).UserData;
        }
    }
}
