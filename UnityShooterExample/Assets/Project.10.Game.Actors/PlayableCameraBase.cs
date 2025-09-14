#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    [DefaultExecutionOrder( ExecutionOrder - 1 )]
    public abstract class PlayableCameraBase : EntityBase {

        public ICameraInputProvider? InputProvider { get; set; }

        protected override void Awake() {
        }
        protected override void OnDestroy() {
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }
        protected virtual void LateUpdate() {
        }

    }
}
