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

    public class Theme : ThemeBase2 {

        private new PlayList? PlayList => (PlayList?) base.PlayList;

        public Theme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            SetPlayList( null );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (PlayList != null && PlayList.IsFading) {
                Volume = Mathf.MoveTowards( Volume, 0, Volume * 1.0f * Time.deltaTime );
                Pitch = Mathf.MoveTowards( Pitch, 0, Pitch * 0.5f * Time.deltaTime );
            }
        }

        public void PlayMainTheme() {
            SetPlayList( new MainPlayList( Container ) );
        }
        public void PlayGameTheme() {
            SetPlayList( new GamePlayList( Container ) );
        }
        public void PlayGameCompletedTheme(bool isPlayerWinner) {
            SetPlayList( null );
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

        public void Pause() {
            IsPaused = true;
        }
        public void UnPause() {
            IsPaused = false;
        }

    }
    public abstract class PlayList : PlayListBase2 {

        private AssetHandle<AudioClip>[] Clips { get; }
        internal bool IsFading { get; set; }

        public PlayList(IDependencyContainer container, AssetHandle<AudioClip>[] clips) : base( container ) {
            Clips = clips;
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            var cancellationToken = this.GetEventCancellationToken_OnAfterDeactivate();
            try {
                for (var i = 0; true; i = (i + 1) % Clips.Length) {
                    await PlayAsync( Clips[ i ], cancellationToken );
                }
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
        }

        private async Task PlayAsync(AssetHandle<AudioClip> clip, CancellationToken cancellationToken) {
            try {
                var clip_ = await clip.Load().GetValueAsync( cancellationToken );
                IsFading = false;
                Volume = 1;
                Pitch = 1;
                await PlayAsync( clip_, cancellationToken );
            } finally {
                clip.Release();
            }
        }

    }
    public class MainPlayList : PlayList {

        private static readonly AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );

        public MainPlayList(IDependencyContainer container) : base( container, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GamePlayList : PlayList {

        private static readonly AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
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
