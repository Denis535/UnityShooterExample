#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Project.App;
    using Project.Game;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;
    using Screen = Project.UI.Screen;

    public class Program : ProgramBase2<Theme, Screen, Router, Application2, Game2> {

        private Theme theme = default!;
        private Screen screen = default!;
        private Router router = default!;
        private Application2 application = default!;

        protected override Theme Theme => this.theme;
        protected override Screen Screen => this.screen;
        protected override Router Router => this.router;
        protected override Application2 Application => this.application;
        protected override Game2? Game => this.application.Game;

        //[RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        //private static void OnLoad() {
        //}

        //[RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.AfterAssembliesLoaded )]
        //private static void OnLoad2() {
        //}

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnLoad_Editor() {
            var message = new StringBuilder()
                .AppendLine( "https://u3d.as/3pWS" )
                .Append( "You can check the latest version: https://github.com/Denis535/UnityShooterExample" );
            Debug.Log( message );
            if (!EditorApplication.isPlaying) {
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Assets.Project.00/Main.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

        protected override void Awake() {
            base.Awake();
            VisualElementFactory.StringSelector = GetDisplayString;
            this.application = new Application2( this );
            this.router = new Router( this );
            this.screen = new Screen( this );
            this.theme = new Theme( this );
        }
        protected override void OnDestroy() {
            this.Theme.Dispose();
            this.Screen.Dispose();
            this.Router.Dispose();
            this.Application.Dispose();
            base.OnDestroy();
        }

        protected override void Start() {
            this.Router.LoadMainScene();
        }
        protected override void FixedUpdate() {
            this.Application?.OnFixedUpdate();
            this.Screen.OnFixedUpdate();
            this.Theme.OnFixedUpdate();
        }
        protected override void Update() {
            this.Application?.OnUpdate();
            this.Screen.OnUpdate();
            this.Theme.OnUpdate();
        }
        protected override void LateUpdate() {
            this.Application?.OnLateUpdate();
        }

        protected override bool OnQuit() {
            if (this.Router.IsMainSceneLoaded || this.Router.IsGameSceneLoaded) {
                this.Router.Quit();
                return false;
            }
            return true;
        }

        protected override Option<object?> GetValue(Type type, object? argument) {
            var value = base.GetValue( type, argument );
            if (value.HasValue) return value;

            // Game
            if (type == typeof( World )) {
                var result = FindAnyObjectByType<World>( FindObjectsInactive.Exclude );
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }

            // Misc
            if (type == typeof( AudioSource ) && (string?) argument == "MusicAudioSource") {
                var result = this.transform.Find( "MusicAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }
            if (type == typeof( UIDocument )) {
                var result = this.gameObject.GetComponentInChildren<UIDocument>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }
            if (type == typeof( AudioSource ) && (string?) argument == "SfxAudioSource") {
                var result = this.transform.Find( "SfxAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }
            return default;
        }

        // Helpers
        private static string GetDisplayString<T>(T value) {
            if (value is Resolution resolution) return GetDisplayString( resolution );
            return value?.ToString() ?? "Null";
        }
        private static string GetDisplayString(Resolution value) {
            return $"{value.width} x {value.height}";
        }

    }
}
