#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase2 {

        private new RootWidget Widget => (RootWidget?) base.Widget!;

        public UIScreen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
            VisualElementFactory.OnPlayClick += evt => { };
            VisualElementFactory.OnPlaySelect += evt => { };
            VisualElementFactory.OnPlaySubmit += evt => { };
            VisualElementFactory.OnPlayCancel += evt => { };
            VisualElementFactory.OnPlayChange += evt => { };
            VisualElementFactory.OnPlayFocus += evt => { };
            VisualElementFactory.OnPlayDialog += evt => { };
            VisualElementFactory.OnPlayInfoDialog += evt => { };
            VisualElementFactory.OnPlayWarningDialog += evt => { };
            VisualElementFactory.OnPlayErrorDialog += evt => { };
            SetWidget( new RootWidget( container ) );
        }
        public override void Dispose() {
            SetWidget( null );
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
    public class RootWidget : UIRootWidgetBase<RootWidgetView> {

        public RootWidget(IDependencyContainer container) : base( container ) {
            View = new RootWidgetView();
            View.OnSubmitEvent += OnSubmit;
            View.OnCancelEvent += OnCancel;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        internal void AddChild(MainWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(GameWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(LoadingWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(UnloadingWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(WarningDialogWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(ErrorDialogWidget widget) {
            base.AddChild( widget );
        }
        internal void Clear() {
            RemoveChildren( i => i is not (DialogWidget or InfoDialogWidget or WarningDialogWidget or ErrorDialogWidget) );
        }

        protected override void Sort(List<NodeBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( (UIWidgetBase) a ), GetOrderOf( (UIWidgetBase) b ) ) );
        }
        private static int GetOrderOf(UIWidgetBase widget) {
            return widget switch {
                MainWidget or GameWidget or LoadingWidget or UnloadingWidget => 0,
                _ => 1,
            };
        }

    }
    public class RootWidgetView : UIRootWidgetViewBase {

        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override int GetOrderOf(UIViewBase view) {
            return GetLayerOf( view );
        }

        protected override int GetLayerOf(UIViewBase view) {
            return view switch {
                // MainScreen
                MainWidgetView => 0,
                MainMenuWidgetView => 100,
                // GameScreen
                GameWidgetView => 0,
                GameTotalsWidgetView => 0,
                GameMenuWidgetView => 100,
                // Common
                LoadingWidgetView => 100,
                UnloadingWidgetView => 100,
                // Common
                SettingsWidgetView => 100,
                ProfileSettingsWidgetView => 100,
                VideoSettingsWidgetView => 100,
                AudioSettingsWidgetView => 100,
                // Common
                DialogWidgetView => 1000,
                InfoDialogWidgetView => 1001,
                WarningDialogWidgetView => 1002,
                ErrorDialogWidgetView => 1003,
                _ => 0
            };
        }

    }
}
