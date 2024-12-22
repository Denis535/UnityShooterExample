#if DEBUG
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Game;
    using Project.UI;
    using UnityEngine;
    using Screen = Project.UI.Screen;

    [DefaultExecutionOrder( int.MaxValue )]
    public class DebugScreen : MonoBehaviour {

        private IDependencyContainer Contairner { get; set; } = default!;
        private Theme Theme { get; set; } = default!;
        private Screen Screen { get; set; } = default!;
        private Router Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        private Game2? Game => Application.Game;

        public void Awake() {
            Contairner = gameObject.RequireComponent<IDependencyContainer>();
            Theme = Contairner.RequireDependency<Theme>();
            Screen = Contairner.RequireDependency<Screen>();
            Router = Contairner.RequireDependency<Router>();
            Application = Contairner.RequireDependency<Application2>();
        }
        public void OnDestroy() {
        }

        public void OnGUI() {
            using (new GUILayout.VerticalScope( GUI.skin.box )) {
                GUILayout.Label( "Fps: " + (1f / Time.smoothDeltaTime).ToString( "000." ) );
                GUILayout.Label( "Main Scene: " + Router.IsMainSceneLoaded );
                GUILayout.Label( "Game Scene: " + Router.IsGameSceneLoaded );
                if (Game != null) {
                    GUILayout.Label( "Game State: " + Game.State );
                    GUILayout.Label( "Game Pause: " + Game.IsPaused );
                    GUILayout.Label( "Player State: " + Game.Player.State );
                }
            }
        }

    }
}
#endif
