#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayableCharacterBase : CharacterBase {

        public ICharacterInputProvider? InputProvider { get; set; }

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        protected override void Start() {
            base.Start();
        }
        protected override void FixedUpdate() {
            base.FixedUpdate();
        }
        protected override void Update() {
            base.Update();
        }
        protected override void LateUpdate() {
            base.LateUpdate();
        }

    }
}
