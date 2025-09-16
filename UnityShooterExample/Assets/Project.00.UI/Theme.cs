#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class Theme : ThemeBase2 {

        public Theme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            this.Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
        }

        public void PlayMainTheme() {
            this.Machine.SetRoot( new MainPlayList( this.Container ).State, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayGameTheme() {
            this.Machine.SetRoot( new GamePlayList( this.Container ).State, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayGameCompletedTheme(bool isPlayerWinner) {
            this.Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayLoadingTheme() {
            if (this.Machine.Root?.PlayList() is MainPlayList mainPlayList) {
                mainPlayList.Fade();
            } else {
                this.Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
            }
        }
        public void PlayUnloadingTheme() {
            this.Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void StopTheme() {
            this.Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }

        public void Pause() {
            var playList = this.Machine.Root!.PlayList<GamePlayList>();
            playList.Pause();
        }
        public void UnPause() {
            var playList = this.Machine.Root!.PlayList<GamePlayList>();
            playList.UnPause();
        }

    }
}
