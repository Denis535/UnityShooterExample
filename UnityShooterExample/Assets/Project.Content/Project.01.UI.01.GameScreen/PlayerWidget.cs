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
            View2 = CreateView( this );
        }
        public override void Dispose() {
            View2.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        public void OnUpdate() {
            if (Game.Player.Camera != null) {
                View2.Target.style.color = GetTargetColor( Game.Player.Camera );
            } else {
                View2.Target.style.color = default;
            }
        }

        // Helpers
        private static PlayerWidgetView CreateView(PlayerWidget widget) {
            var view = new PlayerWidgetView();
            return view;
        }
        // Helpers
        private static Color GetTargetColor(Camera2 camera) {
            if (camera.Hit?.Thing != null) return Color.yellow;
            if (camera.Hit?.Enemy != null) return Color.red;
            return Color.white;
        }

    }
}
