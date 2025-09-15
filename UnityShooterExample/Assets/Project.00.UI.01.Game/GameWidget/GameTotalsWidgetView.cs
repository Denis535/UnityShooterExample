#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class GameTotalsWidgetView : SmallWidgetViewBase {

        public GameTotalsWidgetView(string name) : base( name ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GameTotalsWidgetView_LevelCompleted : GameTotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }
        public Button Continue { get; }
        public Button Back { get; }

        public GameTotalsWidgetView_LevelCompleted() : base( "game-totals-widget-view (level-completed)" ) {
            this.Add(
                this.Card = VisualElementFactory.Card().Children(
                    this.Header = VisualElementFactory.Header().Children(
                         this.Title = VisualElementFactory.Label( "Level Completed" )
                      ),
                    this.Content = VisualElementFactory.Content().Children(
                          VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                             this.Message = VisualElementFactory.Label(
                                  "Congratulations!" + Environment.NewLine +
                                  "You have completed the level!" + Environment.NewLine +
                                  "Do you want to continue or back to the menu?"
                                  ).Class( "text-align-middle-center" )
                          )
                      ),
                      this.Footer = VisualElementFactory.Footer().Children(
                         this.Continue = VisualElementFactory.Submit( "Continue" ),
                          this.Back = VisualElementFactory.Cancel( "Back To Menu" )
                      )
                  )
              );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GameTotalsWidgetView_LevelFailed : GameTotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Label Message { get; }
        public Footer Footer { get; }
        public Button Retry { get; }
        public Button Back { get; }

        public GameTotalsWidgetView_LevelFailed() : base( "game-totals-widget-view (level-failed)" ) {
            this.Add(
                this.Card = VisualElementFactory.Card().Children(
                    this.Header = VisualElementFactory.Header().Children(
                         this.Title = VisualElementFactory.Label( "Level Failed" )
                      ),
                   this.Content = VisualElementFactory.Content().Children(
                          VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                             this.Message = VisualElementFactory.Label(
                                  "We're sorry." + Environment.NewLine +
                                  "You have failed the level." + Environment.NewLine +
                                  "Do you want to retry or back to the menu?"
                                  ).Class( "text-align-middle-center" )
                          )
                      ),
                     this.Footer = VisualElementFactory.Footer().Children(
                         this.Retry = VisualElementFactory.Submit( "Retry" ),
                          this.Back = VisualElementFactory.Cancel( "Back To Menu" )
                      )
                  )
              );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GameTotalsWidgetView_GameCompleted : GameTotalsWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public Footer Footer { get; }
        public Label Message { get; }
        public Button Okey { get; }

        public GameTotalsWidgetView_GameCompleted() : base( "game-totals-widget-view (game-completed)" ) {
            this.Add(
                 this.Card = VisualElementFactory.Card().Children(
                     this.Header = VisualElementFactory.Header().Children(
                        this.Title = VisualElementFactory.Label( "Game Completed" )
                      ),
                   this.Content = VisualElementFactory.Content().Children(
                          VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                            this.Message = VisualElementFactory.Label(
                                  "Congratulations!" + Environment.NewLine +
                                  "You have completed the game!"
                                  ).Class( "text-align-middle-center" )
                          )
                      ),
                    this.Footer = VisualElementFactory.Footer().Children(
                         this.Okey = VisualElementFactory.Submit( "Ok" )
                      )
                  )
              );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
