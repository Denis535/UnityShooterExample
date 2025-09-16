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

        protected override Theme Theme { get; set; } = default!;
        protected override Screen Screen { get; set; } = default!;
        protected override Router Router { get; set; } = default!;
        protected override Application2 Application { get; set; } = default!;
        protected override Game2? Game => this.Application.Game;

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
                .Append( "You can check the latest version: https://denis535.github.io/#unity-shooter-example-unity" );
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
            this.Application = new Application2( this );
            this.Router = new Router( this );
            this.Screen = new Screen( this );
            this.Theme = new Theme( this );
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
            this.ThrowIfInvalid();
            // UI
            if (type.IsAssignableTo( typeof( ThemeBase ) )) {
                if (this.Theme != null) return Option.Create( (object?) this.Theme );
                return default;
            }
            if (type.IsAssignableTo( typeof( ScreenBase ) )) {
                if (this.Screen != null) return Option.Create( (object?) this.Screen );
                return default;
            }
            if (type.IsAssignableTo( typeof( RouterBase ) )) {
                if (this.Router != null) return Option.Create( (object?) this.Router );
                return default;
            }
            // App
            if (type.IsAssignableTo( typeof( ApplicationBase ) )) {
                if (this.Application != null) return Option.Create( (object?) this.Application );
                return default;
            }
            // Game
            if (type.IsAssignableTo( typeof( GameBase ) )) {
                if (this.Game != null) return Option.Create( (object?) this.Game );
                return default;
            }
            if (type.IsAssignableTo( typeof( World ) )) {
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
            if (type == typeof( AudioSource ) && (string?) argument == "SfxAudioSource") {
                var result = this.transform.Find( "SfxAudioSource" )?.gameObject.GetComponent<AudioSource?>();
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
