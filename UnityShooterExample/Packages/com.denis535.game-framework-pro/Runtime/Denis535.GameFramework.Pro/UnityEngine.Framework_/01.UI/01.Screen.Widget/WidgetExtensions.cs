#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using UnityEngine;

    public static class WidgetExtensions {

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.Node.OnDetachCallback += OnEvent;
            void OnEvent(object? argument) {
                cts.Cancel();
                widget.Node.OnDetachCallback -= OnEvent;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.Node.OnDeactivateCallback += OnEvent;
            void OnEvent(object? argument) {
                cts.Cancel();
                widget.Node.OnDeactivateCallback -= OnEvent;
            }
            return cts.Token;
        }

    }
}
