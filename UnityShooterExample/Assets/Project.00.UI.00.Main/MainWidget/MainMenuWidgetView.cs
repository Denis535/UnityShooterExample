#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainMenuWidgetView : LeftWidgetViewBase {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public VisualElement Content { get; }

        public MainMenuWidgetView() : base( "main-menu-widget-view" ) {
            this.Add(
                 this.Card = VisualElementFactory.Card().Children(
                     this.Header = VisualElementFactory.Header().Children(
                         this.Title = VisualElementFactory.Label( "Menu" )
                      ),
                     this.Content = VisualElementFactory.Content()
                  )
              );
        }
        public override void Dispose() {
            foreach (var view in this.Content.Children().Cast<ViewBase>()) {
                view.Dispose();
            }
            base.Dispose();
        }

        protected override bool TryAddView(ViewBase view) {
            if (view is MainMenuWidgetView_Initial or MainMenuWidgetView_StartGame or MainMenuWidgetView_SelectLevel or MainMenuWidgetView_SelectCharacter) {
                this.Content.Add( view );
                this.Title.text = GetTitle( (ViewBase) this.Content.Children().Last() );
                SetVisibility( this.Content.Children().Cast<ViewBase>().ToArray() );
                return true;
            }
            return false;
        }
        protected override bool TryRemoveView(ViewBase view) {
            if (view is MainMenuWidgetView_Initial or MainMenuWidgetView_StartGame or MainMenuWidgetView_SelectLevel or MainMenuWidgetView_SelectCharacter) {
                this.Content.Remove( view );
                this.Title.text = GetTitle( (ViewBase) this.Content.Children().Last() );
                SetVisibility( this.Content.Children().Cast<ViewBase>().ToArray() );
                return true;
            }
            return false;
        }

        // Helpers
        private static string GetTitle(ViewBase view) {
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
        private static void SetVisibility(ViewBase[] views) {
            SaveFocus( views );
            for (var i = 0; i < views.Length; i++) {
                var view = views[ i ];
                var next = views.ElementAtOrDefault( i + 1 );
                view.SetDisplayed( next == null );
            }
            LoadFocus( views );
        }
        private static void SaveFocus(IReadOnlyList<ViewBase> views) {
            foreach (var view in views) {
                if (view.HasFocusedElement()) {
                    view.SaveFocus();
                }
            }
        }
        private static void LoadFocus(IReadOnlyList<ViewBase> views) {
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
    public class MainMenuWidgetView_Initial : IndieViewBase {

        public Button StartGame { get; }
        public Button Settings { get; }
        public Button Quit { get; }

        public MainMenuWidgetView_Initial() : base( "initial-view" ) {
            this.Add(
              this.StartGame = VisualElementFactory.Select( "Start Game" ),
               this.Settings = VisualElementFactory.Select( "Settings" ),
               this.Quit = VisualElementFactory.Quit( "Quit" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_StartGame : IndieViewBase {

        public Button NewGame { get; }
        public Button Continue { get; }
        public Button Back { get; }

        public MainMenuWidgetView_StartGame() : base( "start-game-view" ) {
            this.Add(
               this.NewGame = VisualElementFactory.Select( "New Game" ),
               this.Continue = VisualElementFactory.Select( "Continue" ),
               this.Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_SelectLevel : IndieViewBase {

        public Button Level1 { get; }
        public Button Level2 { get; }
        public Button Level3 { get; }
        public Button Back { get; }

        public MainMenuWidgetView_SelectLevel() : base( "select-level-view" ) {
            this.Add(
                VisualElementFactory.ColumnScope().Class( "margin-bottom-4px" ).Children(
                  this.Level1 = VisualElementFactory.Select( "Level 1" ),
                  this.Level2 = VisualElementFactory.Select( "Level 2" ),
                   this.Level3 = VisualElementFactory.Select( "Level 3" )
                ),
               this.Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MainMenuWidgetView_SelectCharacter : IndieViewBase {

        public Button Gray { get; }
        public Button Red { get; }
        public Button Green { get; }
        public Button Blue { get; }
        public Button Back { get; }

        public MainMenuWidgetView_SelectCharacter() : base( "select-character-view" ) {
            this.Add(
                VisualElementFactory.ColumnScope().Class( "margin-bottom-4px" ).Children(
                   this.Gray = VisualElementFactory.Select( "Gray" ),
                  this.Red = VisualElementFactory.Select( "Red" ),
                  this.Green = VisualElementFactory.Select( "Green" ),
                  this.Blue = VisualElementFactory.Select( "Blue" )
                ),
               this.Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
