#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class GamePlayList : PlayListBase3 {

        private static readonly AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.Game.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.Game.Music.Value_Theme_2 ),
        } );

        public GamePlayList(IDependencyContainer container) : base( container, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
