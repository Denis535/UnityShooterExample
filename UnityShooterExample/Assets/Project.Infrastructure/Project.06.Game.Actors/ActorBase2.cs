#nullable enable
namespace Project.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public abstract class ActorBase2 : ActorBase, IDamageable {

        public bool IsAlive { get; private set; } = true;
        public event Action<DamageInfo>? OnDamageEvent;
        public event Action<DamageInfo>? OnDeathEvent;

        protected override void Awake() {
        }
        protected override void OnDestroy() {
        }

        public void Damage(DamageInfo info) {
            if (IsAlive) {
                IsAlive = false;
                OnDamage( info );
                OnDamageEvent?.Invoke( info );
                OnDeath( info );
                OnDeathEvent?.Invoke( info );
            }
        }
        protected virtual void OnDamage(DamageInfo info) {
        }
        protected virtual void OnDeath(DamageInfo info) {
        }

    }
}
