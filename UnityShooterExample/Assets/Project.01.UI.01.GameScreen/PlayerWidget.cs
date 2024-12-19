#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;

    public class PlayerWidget : UIWidgetBase2<PlayerWidgetView> {

        private Game2 Game { get; }
        private Player2 Player => Game.Player;

        public PlayerWidget(IDependencyContainer container) : base( container ) {
            Game = container.RequireDependency<Game2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        public void OnUpdate() {
            if (Player.Camera != null) {
                View.Target.style.color = GetTargetColor( Player.Camera.Hit );
            } else {
                View.Target.style.color = default;
            }
        }

        // Helpers
        private static PlayerWidgetView CreateView(PlayerWidget widget) {
            var view = new PlayerWidgetView();
            return view;
        }
        // Helpers
        private static Color GetTargetColor(PlayerCamera.RaycastHit? hit) {
            if (hit?.Entity is ThingBase) return Color.yellow;
            if (hit?.Entity is EnemyCharacter) return Color.red;
            return Color.white;
        }

    }
}
