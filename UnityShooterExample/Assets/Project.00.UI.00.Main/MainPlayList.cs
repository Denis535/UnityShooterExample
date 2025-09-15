#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class MainPlayList : PlayListBase3 {

        private static readonly AssetHandle<AudioClip>[] Clips = new[] {
            new AssetHandle<AudioClip>( R.Project.UI.Main.Music.Value_Theme )
        }.Chain( Shuffle );

        public MainPlayList(IDependencyContainer container) : base( container, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
