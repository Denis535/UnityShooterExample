#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class Screen : ScreenBase2 {

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
            Machine.SetRoot( new RootWidget( container ).Node, null, (root, arg) => root.Widget().Dispose() );
        }
        public override void Dispose() {
            Machine.SetRoot( null, null, (root, arg) => root.Widget().Dispose() );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            foreach (var child in Machine.Root!.Children) {
                (child.Widget() as MainWidget)?.OnUpdate();
                (child.Widget() as GameWidget)?.OnUpdate();
            }
        }

        public void ShowMainScreen() {
            HideScreen();
            Machine.Root!.AddChild( new MainWidget( Container ).Node, null );
        }
        public void ShowGameScreen() {
            HideScreen();
            Machine.Root!.AddChild( new GameWidget( Container ).Node, null );
        }
        public void ShowLoadingScreen() {
            HideScreen();
            Machine.Root!.AddChild( new LoadingWidget( Container ).Node, null );
        }
        public void ShowUnloadingScreen() {
            HideScreen();
            Machine.Root!.AddChild( new UnloadingWidget( Container ).Node, null );
        }
        public void HideScreen() {
            Machine.Root!.RemoveChildren( null, (child, arg) => child.Widget().Dispose() );
        }

    }
}
