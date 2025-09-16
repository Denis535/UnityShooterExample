#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public class GamePlayList : PlayListBase2 {

        private AssetHandle<AudioClip>[] Clips { get; } = new[] {
            new AssetHandle<AudioClip>( R.Project.UI.Game.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.Game.Music.Value_Theme_2 ),
        }.Chain( Shuffle );

        public GamePlayList(IDependencyContainer container) : base( container ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            var cancellationToken = this.GetCancellationToken_OnDeactivateCallback();
            try {
                for (var i = 0; true; i = (i + 1) % this.Clips.Length) {
                    var clip = this.Clips[ i ];
                    try {
                        var clip_ = await clip.Load().GetValueAsync( cancellationToken );
                        this.Mute = false;
                        this.Volume = 1;
                        this.Pitch = 1;
                        await this.PlayAndWaitForCompletionAsync( clip_, cancellationToken );
                    } finally {
                        clip.Release();
                    }
                }
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
        }

        public void Pause() {
            //Assert.Operation.Message( $"IsPaused must be false" ).Valid( !this.IsPaused );
            this.IsPaused = true;
        }
        public void UnPause() {
            //Assert.Operation.Message( $"IsPaused must be true" ).Valid( this.IsPaused );
            this.IsPaused = false;
        }

    }
}
