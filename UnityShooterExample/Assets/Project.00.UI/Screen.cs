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

        public Screen(IDependencyProvider provider) : base( provider, provider.RequireDependency<UIDocument>(), provider.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
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
            this.Machine.SetRoot( new RootWidget( provider ).Node, null, (root, arg) => root.Widget().Dispose() );
        }
        public override void Dispose() {
            this.Machine.SetRoot( null, null, (root, arg) => root.Widget().Dispose() );
            base.Dispose();
        }

        public void OnFixedUpdate() {
            this.Machine.Root!.Widget<RootWidget>().OnFixedUpdate();
        }
        public void OnUpdate() {
            this.Machine.Root!.Widget<RootWidget>().OnUpdate();
        }

        public void ShowMainScreen() {
            this.Machine.Root!.Widget<RootWidget>().ShowMainWidget( new MainWidget( this.Provider ) );
        }
        public void ShowGameScreen() {
            this.Machine.Root!.Widget<RootWidget>().ShowGameWidget( new GameWidget( this.Provider ) );
        }
        public void ShowLoadingScreen() {
            this.Machine.Root!.Widget<RootWidget>().ShowLoadingWidget( new LoadingWidget( this.Provider ) );
        }
        public void ShowUnloadingScreen() {
            this.Machine.Root!.Widget<RootWidget>().ShowUnloadingWidget( new UnloadingWidget( this.Provider ) );
        }
        public void Clear() {
            this.Machine.Root!.Widget<RootWidget>().Clear();
        }

    }
}
