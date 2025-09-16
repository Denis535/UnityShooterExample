#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class Screen : ScreenBase2<Router, Application2> {

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
            this.Machine.SetRoot( new RootWidget( container ).Node, null, (root, arg) => root.Widget().Dispose() );
        }
        public override void Dispose() {
            this.Machine.SetRoot( null, null, (root, arg) => root.Widget().Dispose() );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            foreach (var child in this.Machine.Root!.Children) {
                (child.Widget() as MainWidget)?.OnUpdate();
                (child.Widget() as GameWidget)?.OnUpdate();
            }
        }

        public void ShowMainScreen() {
            this.HideScreen();
            this.Machine.Root!.AddChild( new MainWidget( this.Container ).Node, null );
        }
        public void ShowGameScreen() {
            this.HideScreen();
            this.Machine.Root!.AddChild( new GameWidget( this.Container ).Node, null );
        }
        public void ShowLoadingScreen() {
            this.HideScreen();
            this.Machine.Root!.AddChild( new LoadingWidget( this.Container ).Node, null );
        }
        public void ShowUnloadingScreen() {
            this.HideScreen();
            this.Machine.Root!.AddChild( new UnloadingWidget( this.Container ).Node, null );
        }
        public void HideScreen() {
            _ = this.Machine.Root!.RemoveChildren( null, (child, arg) => child.Widget().Dispose() );
        }

    }
}
