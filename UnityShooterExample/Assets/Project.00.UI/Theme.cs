#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Theme : ThemeBase2 {

        private new PlayListBase3? PlayList => (PlayListBase3?) base.PlayList;

        public Theme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            SetPlayList( null, null );
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
            SetPlayList( new MainPlayList( Container ), null );
        }
        public void PlayGameTheme() {
            SetPlayList( new GamePlayList( Container ), null );
        }
        public void PlayGameCompletedTheme(bool isPlayerWinner) {
            SetPlayList( null, null );
        }
        public void PlayLoadingTheme() {
            if (PlayList is MainPlayList mainStrategy) {
                mainStrategy.IsFading = true;
            } else {
                SetPlayList( null, null );
            }
        }
        public void PlayUnloadingTheme() {
            SetPlayList( null, null );
        }
        public void StopTheme() {
            SetPlayList( null, null );
        }

        public void Pause() {
            IsPaused = true;
        }
        public void UnPause() {
            IsPaused = false;
        }

    }
}
