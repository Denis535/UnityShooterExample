#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public class World : EntityBase {

        private IReadOnlyList<PlayerPoint> playerPoints = default!;
        private IReadOnlyList<EnemyPoint> enemyPoints = default!;
        private IReadOnlyList<ThingPoint> thingPoints = default!;

        public IReadOnlyList<PlayerPoint> PlayerPoints {
            get {
                this.ThrowIfInvalid();
                return this.playerPoints;
            }
        }
        public IReadOnlyList<EnemyPoint> EnemyPoints {
            get {
                this.ThrowIfInvalid();
                return this.enemyPoints;
            }
        }
        public IReadOnlyList<ThingPoint> ThingPoints {
            get {
                this.ThrowIfInvalid();
                return this.thingPoints;
            }
        }

#if UNITY_EDITOR
        //public void OnValidate() {
        //    if (!Application.isPlaying) {
        //        foreach (var gameObject in gameObject.scene.GetRootGameObjects()) {
        //            if (gameObject != base.gameObject) {
        //                if (gameObject.isStatic) {
        //                    gameObject.transform.parent = base.transform;
        //                } else {
        //                    gameObject.transform.parent = null;
        //                }
        //            }
        //        }
        //    }
        //}
#endif

        protected override void Awake() {
            playerPoints = this.gameObject.GetComponentsInChildren<PlayerPoint>();
            enemyPoints = this.gameObject.GetComponentsInChildren<EnemyPoint>();
            thingPoints = this.gameObject.GetComponentsInChildren<ThingPoint>();
        }
        protected override void OnDestroy() {
        }

    }
}
