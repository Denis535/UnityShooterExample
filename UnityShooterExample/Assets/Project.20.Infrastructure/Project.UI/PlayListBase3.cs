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

    public abstract class PlayListBase3 : PlayListBase2 {

        private AssetHandle<AudioClip>[] Clips { get; }
        public bool IsFading { get; set; }

        public PlayListBase3(IDependencyContainer container, AssetHandle<AudioClip>[] clips) : base( container ) {
            Clips = clips;
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            var cancellationToken = this.GetCancellationToken_OnDeactivateCallback();
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
                await PlayAndWaitForCompletionAsync( clip_, cancellationToken );
            } finally {
                clip.Release();
            }
        }

    }
}
