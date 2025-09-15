#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public abstract class ThemeBase : DisposableBase {

        // Machine
        protected internal StateMachine<State<PlayListBase>, ThemeBase> Machine { get; }

        // AudioSource
        protected AudioSource AudioSource { get; }
        // IsRunning
        protected internal bool IsRunning {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return AudioSource.clip != null;
            }
        }
        protected internal bool IsPlaying {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return AudioSource.clip != null && AudioSource.time < AudioSource.clip.length;
            }
        }
        protected internal bool IsPaused {
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                if (value) {
                    AudioSource.Pause();
                } else {
                    AudioSource.UnPause();
                }
            }
        }
        protected internal bool Mute {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return AudioSource.mute;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                AudioSource.mute = value;
            }
        }
        protected internal float Volume {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return AudioSource.volume;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                AudioSource.volume = value;
            }
        }
        protected internal float Pitch {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return AudioSource.pitch;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                AudioSource.pitch = value;
            }
        }

        // Constructor
        public ThemeBase(AudioSource audioSource) {
            Machine = new StateMachine<State<PlayListBase>, ThemeBase>( this );
            AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Theme {this} must have no {Machine.Root} root" ).Valid( Machine.Root == null );
            Assert.Operation.Message( $"Theme {this} must be released" ).Valid( !AudioSource || AudioSource.clip == null );
            base.Dispose();
        }

        // Play
        protected internal void Play(AudioClip clip) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !IsRunning );
            AudioSource.clip = clip;
            AudioSource.Play();
        }
        protected internal async Task PlayAndWaitForCompletionAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !IsRunning );
            try {
                Play( clip );
                while (AudioSource.clip == clip && AudioSource.time < AudioSource.clip.length) {
                    await Awaitable.NextFrameAsync( cancellationToken );
                }
            } finally {
                Stop();
            }
        }
        protected internal void Stop() {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be running" ).Valid( IsRunning );
            AudioSource.Stop();
            AudioSource.clip = null;
        }

    }
}
