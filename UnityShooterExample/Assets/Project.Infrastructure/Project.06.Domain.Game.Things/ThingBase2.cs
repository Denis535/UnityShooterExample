#nullable enable
namespace Project.Domain.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public abstract class ThingBase2 : ThingBase {

        protected Rigidbody Rigidbody { get; private set; } = default!;
        public bool IsRigidbody {
            get => !Rigidbody.isKinematic;
            set {
                Rigidbody.isKinematic = !value;
            }
        }

        protected override void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        protected override void OnDestroy() {
        }

    }
}
