#nullable enable
namespace Project.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class World : MonoBehaviour {

        private PlayerPoint[] playerPoints = default!;
        private EnemyPoint[] enemyPoints = default!;
        private ThingPoint[] thingPoints = default!;

        public PlayerPoint[] PlayerPoints => this.Chain( i => i.ThrowIfInvalid() ).playerPoints;
        public EnemyPoint[] EnemyPoints => this.Chain( i => i.ThrowIfInvalid() ).enemyPoints;
        public ThingPoint[] ThingPoints => this.Chain( i => i.ThrowIfInvalid() ).thingPoints;

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

        protected virtual void Awake() {
            playerPoints = gameObject.GetComponentsInChildren<PlayerPoint>();
            enemyPoints = gameObject.GetComponentsInChildren<EnemyPoint>();
            thingPoints = gameObject.GetComponentsInChildren<ThingPoint>();
        }
        protected virtual void OnDestroy() {
        }

    }
}
