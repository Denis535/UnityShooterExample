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
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                Assert.Operation.Message( $"PlayList {this} must be active or activating or deactivating" ).Valid( this.State.Activity is Activity.Active or Activity.Activating or Activity.Deactivating );
                return ((StateMachine<State<PlayListBase>, ThemeBase>?) this.State.Machine)!.UserData;
            }
        }

        // AudioSource
        protected AudioSource AudioSource => this.Theme.AudioSource;

        // IsRunning
        protected bool IsRunning {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.Theme!.IsRunning;
            }
        }
        protected bool IsPlaying {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.Theme!.IsPlaying;
            }
        }
        protected bool IsPaused {
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.Theme!.IsPaused = value;
            }
        }
        protected bool Mute {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.Theme!.Mute;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.Theme!.Mute = value;
            }
        }
        protected float Volume {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.Theme!.Volume;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.Theme!.Volume = value;
            }
        }
        protected float Pitch {
            get {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.Theme!.Pitch;
            }
            set {
                Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.Theme!.Pitch = value;
            }
        }

        // Constructor
        public PlayListBase() {
            this.State = new State<PlayListBase>( this );
            this.State.OnActivateCallback += this.OnActivate;
            this.State.OnDeactivateCallback += this.OnDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"PlayList {this} must be inactive" ).Valid( this.State.Activity is Activity.Inactive );
            base.Dispose();
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        // Play
        protected void Play(AudioClip clip) {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be non-running" ).Valid( !this.IsRunning );
            this.Theme!.Play( clip );
        }
        protected Task PlayAndWaitForCompletionAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be non-running" ).Valid( !this.IsRunning );
            return this.Theme!.PlayAndWaitForCompletionAsync( clip, cancellationToken );
        }
        protected void Stop() {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be running" ).Valid( this.IsRunning );
            this.Theme!.Stop();
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
}
