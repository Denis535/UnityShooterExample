#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public abstract class WeaponBase : ThingBase {

        protected Rigidbody Rigidbody { get; private set; } = default!;
        public bool IsRigidbody {
            get => !this.Rigidbody.isKinematic;
            set {
                this.Rigidbody.isKinematic = !value;
            }
        }

        protected override void Awake() {
            this.Rigidbody = this.gameObject.RequireComponent<Rigidbody>();
        }
        protected override void OnDestroy() {
        }

        public abstract bool TryFire(ActorBase actor, PlayerBase? player);

    }
    public class FireDelay {

        private float Interval { get; }
        private float? FireTime { get; set; }
        public bool CanFire => !this.FireTime.HasValue || (this.FireTime.Value + this.Interval - Time.time) <= 0;

        public FireDelay(float interval) {
            this.Interval = interval;
        }

        public void Fire() {
            this.FireTime = Time.time;
        }

    }
}
