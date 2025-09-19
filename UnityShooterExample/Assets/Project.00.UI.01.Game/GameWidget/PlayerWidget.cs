#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Game;
    using UnityEngine;
    using UnityEngine.Framework;

    public class PlayerWidget : ViewableWidgetBase2<PlayerWidgetView> {

        private Game2 Game { get; }
        private Player2 Player => this.Game.Player;

        public PlayerWidget(IDependencyProvider provider) : base( provider ) {
            this.Game = provider.RequireDependency<Game2>();
            this.View = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        public void OnUpdate() {
            if (this.Player.Camera != null) {
                this.View.Target.style.color = GetTargetColor( this.Player.Camera.Hit );
            } else {
                this.View.Target.style.color = default;
            }
        }

        // Helpers
        private static PlayerWidgetView CreateView(PlayerWidget widget) {
            var view = new PlayerWidgetView();
            return view;
        }
        // Helpers
        private static Color GetTargetColor(PlayerCamera.RaycastHit? hit) {
            if (hit?.Entity is WeaponBase) return Color.yellow;
            if (hit?.Entity is EnemyCharacter) return Color.red;
            return Color.white;
        }

    }
}
