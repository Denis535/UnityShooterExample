#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public abstract class ThemeBase : DisposableBase, IStateful<PlayListBase> {

        private PlayListBase? playList;

        // AudioSource
        protected AudioSource AudioSource { get; }
        // PlayList
        PlayListBase? IStateful<PlayListBase>.State { get => PlayList; set => PlayList = value; }
        protected internal PlayListBase? PlayList {
            get {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                return playList;
            }
            private set {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                playList = value;
            }
        }
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
            AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must have no play list" ).Valid( PlayList == null );
            Assert.Operation.Message( $"Theme {this} must be released" ).Valid( !AudioSource || AudioSource.clip == null );
            base.Dispose();
        }

        // SetPlayList
        void IStateful<PlayListBase>.SetState(PlayListBase? state, object? argument) {
            SetPlayList( state, argument );
        }
        protected virtual void SetPlayList(PlayListBase? playList, object? argument = null) {
            if (playList != null) {
                Assert.Argument.Message( $"Argument 'playList' ({playList}) must be non-disposed" ).Valid( !playList.IsDisposed );
                Assert.Argument.Message( $"Argument 'playList' ({playList}) must be inactive" ).Valid( playList.Activity is PlayListBase.Activity_.Inactive );
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                IStateful<PlayListBase>.SetState( this, playList, argument );
            } else {
                Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
                IStateful<PlayListBase>.SetState( this, playList, argument );
            }
        }
        void IStateful<PlayListBase>.AddState(PlayListBase playList, object? argument) {
            IStateful<PlayListBase>.AddState( this, playList, argument );
        }
        void IStateful<PlayListBase>.RemoveState(PlayListBase playList, object? argument) {
            IStateful<PlayListBase>.RemoveState( this, playList, argument );
            playList.Dispose();
        }

        // Play
        protected internal async Task PlayAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !IsRunning );
            try {
                Play( clip );
                while (IsPlaying) {
                    await Awaitable.NextFrameAsync( cancellationToken );
                }
            } finally {
                Stop();
            }
        }
        protected internal void Play(AudioClip clip) {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be non-running" ).Valid( !IsRunning );
            AudioSource.clip = clip;
            AudioSource.Play();
        }
        protected internal void Stop() {
            Assert.Operation.Message( $"Theme {this} must be non-disposed" ).NotDisposed( !IsDisposed );
            Assert.Operation.Message( $"Theme {this} must be running" ).Valid( IsRunning );
            AudioSource.Stop();
            AudioSource.clip = null;
        }

    }
}