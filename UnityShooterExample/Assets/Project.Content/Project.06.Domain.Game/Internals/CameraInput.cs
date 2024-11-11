#nullable enable
namespace Project.Domain.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    internal class CameraInput : ICameraInput {

        private InputActions_Camera Input { get; }
        //public bool IsEnabled {
        //    get => Input.Camera.enabled;
        //    set {
        //        if (value) Input.Enable(); else Input.Disable();
        //    }
        //}

        public CameraInput(InputActions_Camera input) {
            Input = input;
        }

        public Vector2 GetLookDelta() {
            return Input.Camera.Look.ReadValue<Vector2>();
        }
        public float GetZoomDelta() {
            return Input.Camera.Zoom.ReadValue<Vector2>().y;
        }

    }
}
