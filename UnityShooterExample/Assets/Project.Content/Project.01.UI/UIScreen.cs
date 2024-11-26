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
            VisualElementFactory.OnPlayOpenDialog += evt => { };
            VisualElementFactory.OnPlayOpenInfoDialog += evt => { };
            VisualElementFactory.OnPlayOpenWarningDialog += evt => { };
            VisualElementFactory.OnPlayOpenErrorDialog += evt => { };
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

        protected override void Sort(List<UIWidgetBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }
        private static int GetOrderOf(UIWidgetBase widget) {
            return widget switch {
                // MainScreen
                MainWidget => 0,
                //MainMenuWidget => 1,
                // GameScreen
                GameWidget => 10,
                //PlayerWidget => 11,
                //GameTotalsWidget => 12,
                //GameMenuWidget => 13,
                // Common
                LoadingWidget => 20,
                UnloadingWidget => 21,
                //SettingsWidget => 22,
                DialogWidget => 23,
                InfoDialogWidget => 24,
                WarningDialogWidget => 25,
                ErrorDialogWidget => 26,
                _ => 100_000
            };
        }

    }
    public class RootWidgetView : UIRootWidgetViewBase {

        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override bool TryAddView(UIViewBase view) {
            return base.TryAddView( view );
        }
        protected override bool TryRemoveView(UIViewBase view) {
            return base.TryRemoveView( view );
        }

        protected override void Sort() {
            base.Sort();
        }
        protected override int GetOrderOf(UIViewBase view) {
            return view switch {
                // MainScreen
                MainWidgetView => 0,
                MainMenuWidgetView => 1,
                // GameScreen
                GameWidgetView => 10,
                PlayerWidgetView => 11,
                GameTotalsWidgetView => 12,
                GameMenuWidgetView => 13,
                // Common
                LoadingWidgetView => 20,
                UnloadingWidgetView => 21,
                SettingsWidgetView => 22,
                DialogWidgetView => 23,
                InfoDialogWidgetView => 24,
                WarningDialogWidgetView => 25,
                ErrorDialogWidgetView => 26,
                _ => 100_000
            };
        }

        protected override void SetVisibility(IReadOnlyList<VisualElement> views) {
            base.SetVisibility( views );
        }
        protected override void SetVisibility(UIViewBase view, UIViewBase? next) {
            if (view is not MainWidgetView and not GameWidgetView) {
                base.SetVisibility( view, next );
            }
        }
        protected override int GetLayerOf(UIViewBase view) {
            return view switch {
                // MainScreen
                MainWidgetView => 0,
                MainMenuWidgetView => 100,
                // GameScreen
                GameWidgetView => 0,
                PlayerWidgetView => 100,
                GameTotalsWidgetView => 100,
                GameMenuWidgetView => 100,
                // Common
                LoadingWidgetView => 0,
                UnloadingWidgetView => 0,
                SettingsWidgetView => 100,
                DialogWidgetView => 500,
                InfoDialogWidgetView => 501,
                WarningDialogWidgetView => 502,
                ErrorDialogWidgetView => 503,
                _ => 100_000
            };
        }

    }
}
