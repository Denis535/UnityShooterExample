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
        protected internal AudioSource AudioSource { get; }

        // IsRunning
        protected internal bool IsRunning {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.AudioSource.clip != null;
            }
        }
        protected internal bool IsPlaying {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.AudioSource.clip != null && this.AudioSource.time < this.AudioSource.clip.length;
            }
        }
        protected internal bool IsPaused {
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                if (value) {
                    this.AudioSource.Pause();
                } else {
                    this.AudioSource.UnPause();
                }
            }
        }
        protected internal bool Mute {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.AudioSource.mute;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.AudioSource.mute = value;
            }
        }
        protected internal float Volume {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.AudioSource.volume;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.AudioSource.volume = value;
            }
        }
        protected internal float Pitch {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return this.AudioSource.pitch;
            }
            set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                this.AudioSource.pitch = value;
            }
        }

        // Constructor
        public ThemeBase(AudioSource audioSource) {
            this.Machine = new StateMachine<State<PlayListBase>, ThemeBase>( this );
            this.AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Theme {this} must have no {this.Machine.Root} root" ).Valid( this.Machine.Root == null );
            Assert.Operation.Message( $"Theme {this} must be released" ).Valid( !this.AudioSource || this.AudioSource.clip == null );
            base.Dispose();
        }

        // Play
        protected internal void Play(AudioClip clip) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !this.IsRunning );
            this.AudioSource.clip = clip;
            this.AudioSource.Play();
        }
        protected internal async Task PlayAndWaitForCompletionAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !this.IsRunning );
            try {
                this.Play( clip );
                while (this.AudioSource.clip == clip && this.AudioSource.time < this.AudioSource.clip.length) {
                    await Awaitable.NextFrameAsync( cancellationToken );
                }
            } finally {
                this.Stop();
            }
        }
        protected internal void Stop() {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be running" ).Valid( this.IsRunning );
            this.AudioSource.Stop();
            this.AudioSource.clip = null;
        }

    }
}
