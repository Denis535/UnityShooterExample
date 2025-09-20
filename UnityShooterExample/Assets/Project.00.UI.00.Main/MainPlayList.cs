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

    public class MainPlayList : PlayListBase2 {

        private bool IsFading { get; set; }

        private AssetHandle<AudioClip>[] Clips { get; } = new[] {
            new AssetHandle<AudioClip>( R.Project.UI.Main.Music.Value_Theme )
        }.Chain( Shuffle );

        public MainPlayList(IDependencyProvider provider) : base( provider ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            var cancellationToken = this.StateMutable.GetCancellationToken_OnDeactivateCallback();
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

        public void Fade() {
            Assert.Operation.Message( $"IsFading must be false" ).Valid( !this.IsFading );
            this.IsFading = true;
        }

        private async new Task PlayAndWaitForCompletionAsync(AudioClip clip, CancellationToken cancellationToken) {
            Assert.Operation.Message( $"PlayList {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"PlayList {this} must be non-running" ).Valid( !this.IsRunning );
            try {
                this.Play( clip );
                while (this.AudioSource.clip == clip && this.AudioSource.time < this.AudioSource.clip.length) {
                    await Awaitable.NextFrameAsync( cancellationToken );
                    if (this.IsFading) {
                        this.Volume = Mathf.MoveTowards( this.Volume, 0, this.Volume * 1.0f * Time.deltaTime );
                        this.Pitch = Mathf.MoveTowards( this.Pitch, 0, this.Pitch * 0.5f * Time.deltaTime );
                    }
                }
            } finally {
                this.Stop();
            }
        }

    }
}
