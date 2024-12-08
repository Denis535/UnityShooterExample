#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainMenuWidgetView : LeftWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public VisualElement Content { get; }

        public MainMenuWidgetView() : base( "main-menu-widget-view" ) {
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Menu" )
                    ),
                    Content = VisualElementFactory.Content()
                )
            );
        }
        public override void Dispose() {
            foreach (var view in Content.Children().Cast<UIViewBase>()) {
                view.Dispose();
            }
            base.Dispose();
        }

        protected override bool AddView(UIViewBase view) {
            if (view is MainMenuWidgetView_Initial or MainMenuWidgetView_StartGame or MainMenuWidgetView_SelectLevel or MainMenuWidgetView_SelectCharacter) {
                Content.Add( view );
                Title.text = GetTitle( (UIViewBase) Content.Children().Last() );
                SetVisibility( Content.Children().Cast<UIViewBase>().ToArray() );
                return true;
            }
            return false;
        }
        protected override bool RemoveView(UIViewBase view) {
            if (view is MainMenuWidgetView_Initial or MainMenuWidgetView_StartGame or MainMenuWidgetView_SelectLevel or MainMenuWidgetView_SelectCharacter) {
                Content.Remove( view );
                Title.text = GetTitle( (UIViewBase) Content.Children().Last() );
                SetVisibility( Content.Children().Cast<UIViewBase>().ToArray() );
                return true;
            }
            return false;
        }

        // Helpers
        private static string GetTitle(UIViewBase view) {
            if (view is MainMenuWidgetView_Initial) {
                return "Menu";
            }
            if (view is MainMenuWidgetView_StartGame) {
                return "Start Game";
            }
            if (view is MainMenuWidgetView_SelectLevel) {
                return "Select Level";
            }
            if (view is MainMenuWidgetView_SelectCharacter) {
                return "Select Your Character";
            }
            throw Exceptions.Internal.NotSupported( $"View {view} is not supported" );
        }
        // Helpers
        private static void SetVisibility(UIViewBase[] views) {
            SaveFocus( views );
            for (var i = 0; i < views.Length; i++) {
                var view = views[ i ];
                var next = views.ElementAtOrDefault( i + 1 );
                view.SetDisplayed( next == null );
            }
            LoadFocus( views );
        }
        private static void SaveFocus(IReadOnlyList<UIViewBase> views) {
            foreach (var view in views) {
                if (view.HasFocusedElement()) {
                    view.SaveFocus();
                }
            }
        }
        private static void LoadFocus(IReadOnlyList<UIViewBase> views) {
            var view = views.LastOrDefault();
            if (view != null) {
                if (!view.HasFocusedElement()) {
                    if (!view.LoadFocus()) {
                        view.Focus();
                    }
                }
            }
        }

    }
    public class MainMenuWidgetView_Initial : View {

        public Button StartGame { get; }
        public Button Settings { get; }
        public Button Quit { get; }

        public MainMenuWidgetView_Initial() : base( "initial-view" ) {
            this.Add(
                StartGame = VisualElementFactory.Select( "Start Game" ),
                Settings = VisualElementFactory.Select( "Settings" ),
                Quit = VisualElementFactory.Quit( "Quit" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_StartGame : View {

        public Button NewGame { get; }
        public Button Continue { get; }
        public Button Back { get; }

        public MainMenuWidgetView_StartGame() : base( "start-game-view" ) {
            this.Add(
                NewGame = VisualElementFactory.Select( "New Game" ),
                Continue = VisualElementFactory.Select( "Continue" ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_SelectLevel : View {

        public Button Level1 { get; }
        public Button Level2 { get; }
        public Button Level3 { get; }
        public Button Back { get; }

        public MainMenuWidgetView_SelectLevel() : base( "select-level-view" ) {
            this.Add(
                VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).Children(
                    Level1 = VisualElementFactory.Select( "Level 1" ),
                    Level2 = VisualElementFactory.Select( "Level 2" ),
                    Level3 = VisualElementFactory.Select( "Level 3" )
                ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_SelectCharacter : View {

        public Button Gray { get; }
        public Button Red { get; }
        public Button Green { get; }
        public Button Blue { get; }
        public Button Back { get; }

        public MainMenuWidgetView_SelectCharacter() : base( "select-character-view" ) {
            this.Add(
                VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).Children(
                    Gray = VisualElementFactory.Select( "Gray" ),
                    Red = VisualElementFactory.Select( "Red" ),
                    Green = VisualElementFactory.Select( "Green" ),
                    Blue = VisualElementFactory.Select( "Blue" )
                ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
