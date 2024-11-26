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

    public class Program : ProgramBase2<UITheme, UIScreen, UIRouter, Application2, Game2> {

        protected override UITheme Theme { get; set; } = default!;
        protected override UIScreen Screen { get; set; } = default!;
        protected override UIRouter Router { get; set; } = default!;
        protected override Application2 Application { get; set; } = default!;
        protected override Game2? Game => Application.Game;

        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSplashScreen )]
        private static void OnLoad() {
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnLoad_Editor() {
            var message = new StringBuilder().AppendLine( "You can check the latest version:" ).Append( "https://denis535.github.io/#unity-shooter-example" );
            Debug.Log( message );
            if (!EditorApplication.isPlaying) {
                UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>( "Assets/Project.Content/Assets.Project.00/Scenes/Main.unity" );
                //EditorSceneManager.playModeStartScene = null;
            }
        }
#endif

        protected override void Awake() {
            base.Awake();
            VisualElementFactory.StringSelector = GetDisplayString;
            Application = new Application2( this );
            Router = new UIRouter( this );
            Screen = new UIScreen( this );
            Theme = new UITheme( this );
        }
        protected override void OnDestroy() {
            Theme.Dispose();
            Screen.Dispose();
            Router.Dispose();
            Application.Dispose();
            base.OnDestroy();
        }

        protected override void Start() {
            Router.LoadMainScene();
        }
        protected override void FixedUpdate() {
            Application?.OnFixedUpdate();
            Screen.OnFixedUpdate();
            Theme.OnFixedUpdate();
        }
        protected override void Update() {
            Application?.OnUpdate();
            Screen.OnUpdate();
            Theme.OnUpdate();
        }
        protected override void LateUpdate() {
            Application?.OnLateUpdate();
        }

        protected override bool OnQuit() {
            if (Router.IsMainSceneLoaded || Router.IsGameSceneLoaded) {
                Router.Quit();
                return false;
            }
            return true;
        }

        protected override Option<object?> GetValue(Type type, object? argument) {
            this.ThrowIfInvalid();
            // UI
            if (type.IsAssignableTo( typeof( UIThemeBase ) )) {
                if (Theme != null) return Option.Create( (object?) Theme );
                return default;
            }
            if (type.IsAssignableTo( typeof( UIScreenBase ) )) {
                if (Screen != null) return Option.Create( (object?) Screen );
                return default;
            }
            if (type.IsAssignableTo( typeof( UIRouterBase ) )) {
                if (Router != null) return Option.Create( (object?) Router );
                return default;
            }
            // App
            if (type.IsAssignableTo( typeof( ApplicationBase ) )) {
                if (Application != null) return Option.Create( (object?) Application );
                return default;
            }
            // Game
            if (type.IsAssignableTo( typeof( GameBase ) )) {
                if (Game != null) return Option.Create( (object?) Game );
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
                var result = transform.Find( "MusicAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }
            if (type == typeof( AudioSource ) && (string?) argument == "SfxAudioSource") {
                var result = transform.Find( "SfxAudioSource" )?.gameObject.GetComponent<AudioSource?>();
                if (result is not null) {
                    result.ThrowIfInvalid();
                    return Option.Create( (object?) result );
                }
                return default;
            }
            if (type == typeof( UIDocument )) {
                var result = gameObject.GetComponentInChildren<UIDocument>();
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
