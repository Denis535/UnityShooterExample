#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public abstract class WeaponBase : ThingBase {

        protected Rigidbody Rigidbody { get; private set; } = default!;
        public override bool IsRigidbody {
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

        public abstract bool TryFire(ActorBase actor, PlayerBase? player);

    }
    public class FireDelay {

        private float Interval { get; }
        private float? FireTime { get; set; }
        public bool CanFire => FireTime.HasValue ? (FireTime.Value + Interval - Time.time) <= 0 : true;

        public FireDelay(float interval) {
            Interval = interval;
        }

        public void Fire() {
            FireTime = Time.time;
        }

    }
}
