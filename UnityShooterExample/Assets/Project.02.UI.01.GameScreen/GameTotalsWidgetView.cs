#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class GameTotalsWidgetView : SmallWidgetView {

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
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Completed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!" + Environment.NewLine +
                                "You have completed the level!" + Environment.NewLine +
                                "Do you want to continue or back to the menu?"
                                ).Class( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Continue = VisualElementFactory.Submit( "Continue" ),
                        Back = VisualElementFactory.Cancel( "Back To Menu" )
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
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Level Failed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "We're sorry." + Environment.NewLine +
                                "You have failed the level." + Environment.NewLine +
                                "Do you want to retry or back to the menu?"
                                ).Class( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Retry = VisualElementFactory.Submit( "Retry" ),
                        Back = VisualElementFactory.Cancel( "Back To Menu" )
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
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Game Completed" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        VisualElementFactory.ColumnGroup().Class( "gray" ).Class( "medium" ).Class( "grow-1" ).Class( "justify-content-center" ).Class( "align-items-center" ).Children(
                            Message = VisualElementFactory.Label(
                                "Congratulations!" + Environment.NewLine +
                                "You have completed the game!"
                                ).Class( "text-align-middle-center" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Okey = VisualElementFactory.Submit( "Ok" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
