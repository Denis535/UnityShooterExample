#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidget : WidgetBase2<ProfileSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;

        public ProfileSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            if (argument is DeactivateReason.Submit) {
                ProfileSettings.Name = View.Name.value;
                ProfileSettings.Save();
            } else {
                ProfileSettings.Load();
            }
            HideSelf();
        }

        // Helpers
        private static ProfileSettingsWidgetView CreateView(ProfileSettingsWidget widget) {
            var view = new ProfileSettingsWidgetView( widget.ProfileSettings.IsNameValid );
            view.Name.SetValue( widget.ProfileSettings.Name );
            return view;
        }

    }
}
