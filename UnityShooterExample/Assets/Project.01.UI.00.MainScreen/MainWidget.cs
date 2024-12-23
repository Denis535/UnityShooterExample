#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainWidget : WidgetBase2<MainWidgetView> {

        private Router Router { get; }
        private Application2 Application { get; }

        public MainWidget(IDependencyContainer container) : base( container ) {
            Router = container.RequireDependency<Router>();
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
            AddChild( new MainMenuWidget( Container ), null );
        }
        public override void Dispose() {
            foreach (var child in Children) {
                child.Dispose();
            }
            View.Dispose();
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            ShowSelf();
            Children.OfType<MainMenuWidget>().First().__GetView__().style.display = DisplayStyle.None;
            try {
                await Application.InitializationTask.WaitAsync( DisposeCancellationToken );
                Children.OfType<MainMenuWidget>().First().__GetView__().style.display = StyleKeyword.Null;
            } catch (OperationCanceledException) {
            } catch (Exception ex) {
                ((RootWidget) Root).AddChild( new ErrorDialogWidget( Container, "Error", ex.Message ).OnSubmit( "Ok", () => Router.Quit() ) );
            }
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        protected override void Sort(List<WidgetBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }
        private static int GetOrderOf(WidgetBase widget) {
            return widget switch {
                MainMenuWidget => 0,
                _ => int.MaxValue,
            };
        }

        public void OnUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

    }
}
