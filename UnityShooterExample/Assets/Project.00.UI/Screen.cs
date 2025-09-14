#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class Screen : ScreenBase2 {

        private new RootWidget Widget => (RootWidget?) base.Widget ?? throw Exceptions.Internal.NullReference( $"Reference 'Widget' is null" );

        public Screen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
            VisualElementFactory.OnPlayClick += evt => { };
            VisualElementFactory.OnPlaySelect += evt => { };
            VisualElementFactory.OnPlaySubmit += evt => { };
            VisualElementFactory.OnPlayCancel += evt => { };
            VisualElementFactory.OnPlayChange += evt => { };
            VisualElementFactory.OnPlayFocus += evt => { };
            VisualElementFactory.OnPlayOpenDialog += evt => { };
            VisualElementFactory.OnPlayOpenInfoDialog += evt => { };
            VisualElementFactory.OnPlayOpenWarningDialog += evt => { };
            VisualElementFactory.OnPlayOpenErrorDialog += evt => { };
            SetWidget( new RootWidget( container ), null );
        }
        public override void Dispose() {
            SetWidget( null, null );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.OnUpdate();
                (child as GameWidget)?.OnUpdate();
            }
        }

        public void ShowMainScreen() {
            HideScreen();
            Widget.AddChild( new MainWidget( Container ) );
        }
        public void ShowGameScreen() {
            HideScreen();
            Widget.AddChild( new GameWidget( Container ) );
        }
        public void ShowLoadingScreen() {
            HideScreen();
            Widget.AddChild( new LoadingWidget( Container ) );
        }
        public void ShowUnloadingScreen() {
            HideScreen();
            Widget.AddChild( new UnloadingWidget( Container ) );
        }
        public void HideScreen() {
            Widget.Clear();
        }

    }
}
