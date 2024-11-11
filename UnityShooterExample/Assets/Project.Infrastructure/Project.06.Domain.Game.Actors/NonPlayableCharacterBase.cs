#nullable enable
namespace Project.Domain.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class NonPlayableCharacterBase : CharacterBase {

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

    }
}
