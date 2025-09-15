#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Assertions.Must;
    using UnityEngine.Framework;

    public class Theme : ThemeBase2 {

        public Theme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            if (Machine.Root != null && ((PlayListBase3) Machine.Root.PlayList()).IsFading) {
                Volume = Mathf.MoveTowards( Volume, 0, Volume * 1.0f * Time.deltaTime );
                Pitch = Mathf.MoveTowards( Pitch, 0, Pitch * 0.5f * Time.deltaTime );
            }
        }

        public void PlayMainTheme() {
            Machine.SetRoot( new MainPlayList( Container ).State, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayGameTheme() {
            Machine.SetRoot( new GamePlayList( Container ).State, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayGameCompletedTheme(bool isPlayerWinner) {
            Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void PlayLoadingTheme() {
            if (Machine.Root?.PlayList() is MainPlayList mainStrategy) {
                mainStrategy.IsFading = true;
            } else {
                Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
            }
        }
        public void PlayUnloadingTheme() {
            Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }
        public void StopTheme() {
            Machine.SetRoot( null, null, (state, arg) => state.PlayList().Dispose() );
        }

        public void Pause() {
            IsPaused = true;
        }
        public void UnPause() {
            IsPaused = false;
        }

    }
}
