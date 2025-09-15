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
        private Storage.ProfileSettings ProfileSettings => this.Application.ProfileSettings;

        public ProfileSettingsWidget(IDependencyContainer container) : base( container ) {
            this.Application = container.RequireDependency<Application2>();
            this.View = CreateView( this );
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            this.ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            if (argument is DeactivateReason.Submit) {
                this.ProfileSettings.Name = this.View.Name.value;
                this.ProfileSettings.Save();
            } else {
                this.ProfileSettings.Load();
            }
            this.HideSelf();
        }

        // Helpers
        private static ProfileSettingsWidgetView CreateView(ProfileSettingsWidget widget) {
            var view = new ProfileSettingsWidgetView( widget.ProfileSettings.IsNameValid );
            view.Name.SetValue( widget.ProfileSettings.Name );
            return view;
        }

    }
}
