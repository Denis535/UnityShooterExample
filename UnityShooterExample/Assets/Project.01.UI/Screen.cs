#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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
    public class RootWidget : RootWidgetBase<RootWidgetView> {

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
            base.AddChild( widget, null );
        }
        internal void AddChild(GameWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(LoadingWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(UnloadingWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(WarningDialogWidget widget) {
            base.AddChild( widget, null );
        }
        internal void AddChild(ErrorDialogWidget widget) {
            base.AddChild( widget, null );
        }
        internal void Clear() {
            RemoveChildren( i => i is not (DialogWidget or InfoDialogWidget or WarningDialogWidget or ErrorDialogWidget), null );
        }

        protected override void Sort(List<WidgetBase> children) {
            base.Sort( children );
        }
        protected override int GetOrderOf(WidgetBase widget) {
            return widget switch {
                // MainScreen
                MainWidget => 0,
                // GameScreen
                GameWidget => 100,
                // Common
                LoadingWidget => 200,
                UnloadingWidget => 201,
                _ => int.MaxValue
            };
        }

    }
    public class RootWidgetView : RootWidgetViewBase {

        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override bool TryAddView(ViewBase view) {
            return base.TryAddView( view );
        }
        protected override bool TryRemoveView(ViewBase view) {
            return base.TryRemoveView( view );
        }

        protected override void Sort() {
            base.Sort();
        }
        protected override int GetOrderOf(ViewBase view) {
            return view switch {
                // MainScreen
                MainWidgetView => 0,
                MainMenuWidgetView => 1,
                // GameScreen
                GameWidgetView => 100,
                PlayerWidgetView => 101,
                GameTotalsWidgetView => 102,
                GameMenuWidgetView => 103,
                // Common
                LoadingWidgetView => 200,
                UnloadingWidgetView => 201,
                SettingsWidgetView => 202,
                _ => int.MaxValue,
            };
        }

        protected override void SetVisibility(IReadOnlyList<VisualElement> views) {
            SaveFocus( views );
            for (var i = 0; i < views.Count; i++) {
                var view = (ViewBase) views[ i ];
                var next = views.Skip( i + 1 ).Cast<ViewBase>();
                SetVisibility( view, next );
            }
            LoadFocus( views );
        }
        private void SetVisibility(ViewBase view, IEnumerable<ViewBase> next) {
            if (next.Any()) {
                if (view is not MainWidgetView and not GameWidgetView) {
                    view.SetEnabled( false );
                } else {
                    view.SetEnabled( true );
                }
                if (GetPriorityOf( view ) < next.Max( GetPriorityOf )) {
                    view.SetDisplayed( false );
                } else {
                    view.SetDisplayed( true );
                }
            } else {
                view.SetEnabled( true );
                view.SetDisplayed( true );
            }
        }
        protected int GetPriorityOf(ViewBase view) {
            return view switch {
                // MainScreen
                MainWidgetView => int.MaxValue,
                MainMenuWidgetView => 100,
                // GameScreen
                GameWidgetView => int.MaxValue,
                PlayerWidgetView => 0,
                GameTotalsWidgetView => 100,
                GameMenuWidgetView => 100,
                // Common
                LoadingWidgetView => int.MaxValue,
                UnloadingWidgetView => int.MaxValue,
                SettingsWidgetView => 200,
                DialogWidgetView => int.MinValue,
                InfoDialogWidgetView => int.MinValue,
                WarningDialogWidgetView => int.MinValue,
                ErrorDialogWidgetView => int.MinValue,
                _ => int.MaxValue
            };
        }

    }
}
