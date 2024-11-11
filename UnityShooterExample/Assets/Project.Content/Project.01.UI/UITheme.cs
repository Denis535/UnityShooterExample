#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public class UITheme : UIThemeBase2 {

        private new PlayList? PlayList => (PlayList?) base.PlayList;
        public new bool IsPaused {
            set => base.IsPaused = value;
        }

        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            SetPlayList( null );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
        }

        public void PlayMainTheme() {
            SetPlayList( new MainPlayList( Container ) );
        }
        public void PlayGameTheme() {
            SetPlayList( new GamePlayList( Container ) );
        }
        public void PlayLoadingTheme() {
            if (PlayList is MainPlayList mainStrategy) {
                mainStrategy.IsFading = true;
            } else {
                SetPlayList( null );
            }
        }
        public void PlayUnloadingTheme() {
            SetPlayList( null );
        }
        public void StopTheme() {
            SetPlayList( null );
        }

    }
    public abstract class PlayList : UIPlayListBase2 {

        private readonly CancellationTokenSource stopCancellationTokenSource = new CancellationTokenSource();

        private CancellationToken StopCancellationToken => stopCancellationTokenSource.Token;
        protected AssetHandle<AudioClip>[] Clips { get; }
        public bool IsFading { get; internal set; }

        public PlayList(IDependencyContainer container, AssetHandle<AudioClip>[] clips) : base( container ) {
            Clips = clips;
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            try {
                for (var i = 0; true; i++) {
                    await PlayAsync( Clips[ i % Clips.Length ] );
                }
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
            stopCancellationTokenSource.Cancel();
        }

        private async Task PlayAsync(AssetHandle<AudioClip> clip) {
            try {
                await PlayAsync( await clip.Load().GetValueAsync( StopCancellationToken ) );
            } finally {
                clip.Release();
            }
        }
        private async Task PlayAsync(AudioClip clip) {
            try {
                Play( clip );
                IsFading = false;
                Volume = 1;
                Pitch = 1;
                while (IsPlaying) {
                    if (IsFading) {
                        Volume = Mathf.MoveTowards( Volume, 0, Volume * 1.0f * Time.deltaTime );
                        Pitch = Mathf.MoveTowards( Pitch, 0, Pitch * 0.5f * Time.deltaTime );
                    }
                    await Awaitable.NextFrameAsync( StopCancellationToken );
                }
            } finally {
                Stop();
            }
        }

    }
    public class MainPlayList : PlayList {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );

        public MainPlayList(IDependencyContainer container) : base( container, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GamePlayList : PlayList {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        public GamePlayList(IDependencyContainer container) : base( container, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
