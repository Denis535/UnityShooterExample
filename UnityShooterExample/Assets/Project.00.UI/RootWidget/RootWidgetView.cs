#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

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
