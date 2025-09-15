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
            if (this.Machine.Root != null && ((PlayListBase3) this.Machine.Root.PlayList()).IsFading) {
                this.Volume = Mathf.MoveTowards( this.Volume, 0, this.Volume * 1.0f * Time.deltaTime );
                this.Pitch = Mathf.MoveTowards( this.Pitch, 0, this.Pitch * 0.5f * Time.deltaTime );
            }
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
            if (this.Machine.Root?.PlayList() is MainPlayList mainStrategy) {
                mainStrategy.IsFading = true;
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
            this.IsPaused = true;
        }
        public void UnPause() {
            this.IsPaused = false;
        }

    }
}
