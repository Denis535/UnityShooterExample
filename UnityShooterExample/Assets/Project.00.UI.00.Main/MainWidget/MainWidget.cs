#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.TreeMachine.Pro;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.UIElements;

    public class MainWidget : ViewableWidgetBase2<MainWidgetView> {

        private Router Router { get; }
        private Application2 Application { get; }

        public MainWidget(IDependencyContainer container) : base( container ) {
            this.Router = container.RequireDependency<Router>();
            this.Application = container.RequireDependency<Application2>();
            this.View = CreateView( this );
            this.Node.AddChild( new MainMenuWidget( this.Container ).Node, null );
        }
        public override void Dispose() {
            foreach (var child in this.Node.Children) {
                child.Widget().Dispose();
            }
            this.View.Dispose();
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            this.ShowSelf();
            this.Node.Children.Select( i => i.Widget() ).OfType<MainMenuWidget>().First().View.style.display = DisplayStyle.None;
            try {
                await this.Application.InitializationTask.WaitAsync( this.DisposeCancellationToken );
                this.Node.Children.Select( i => i.Widget() ).OfType<MainMenuWidget>().First().View.style.display = StyleKeyword.Null;
            } catch (OperationCanceledException) {
            } catch (Exception ex) {
                ((RootWidget) this.Node.Root.Widget()).AddChild( new ErrorDialogWidget( this.Container, "Error", ex.Message ).OnSubmit( "Ok", () => this.Router.Quit() ) );
            }
        }
        protected override void OnDeactivate(object? argument) {
            this.HideSelf();
        }

        protected override void Sort(List<NodeBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a.Widget() ), GetOrderOf( b.Widget() ) ) );
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
