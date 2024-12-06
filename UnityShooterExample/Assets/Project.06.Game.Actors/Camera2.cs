#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;

    public partial class Camera2 {
        public static class Factory {

            private static readonly PrefabHandle<Camera2> Prefab = new PrefabHandle<Camera2>( R.Project.Game.Actors.Value_Camera );

            public static void Load() {
                Prefab.Load().Wait();
            }
            public static void Unload() {
                Prefab.Release();
            }

            public static Camera2 Create() {
                var result = GameObject.Instantiate( Prefab.GetValue() );
                return result;
            }

        }
    }
    [DefaultExecutionOrder( 99 )]
    public partial class Camera2 : EntityBase {
        public record RaycastHit(Vector3 Point, float Distance, GameObject GameObject, EnemyCharacter? Enemy, ThingBase? Thing);

        private static readonly Vector2 DefaultAngles = new Vector2( 30, 0 );
        private static readonly float DefaultDistance = 1.5f;
        private static readonly float MinAngleX = -88;
        private static readonly float MaxAngleX = +88;
        private static readonly float MinDistance = 1;
        private static readonly float MaxDistance = 3;
        private static readonly float AnglesInputSensitivity = 0.15f;
        private static readonly float DistanceInputSensitivity = 0.20f;

        public ICameraInputProvider? InputProvider { get; set; }

        public Vector2 Angles { get; private set; }
        public float Distance { get; private set; }

        public RaycastHit? Hit { get; private set; }

        protected override void Awake() {
        }
        protected override void OnDestroy() {
        }

        protected void Start() {
        }
        protected void FixedUpdate() {
        }
        protected void Update() {
            if (InputProvider != null) {
                var target = InputProvider.GetTarget( out var isNewTarget );
                if (isNewTarget) {
                    Angles = new Vector2( DefaultAngles.x, target.transform.eulerAngles.y );
                    Distance = DefaultDistance;
                } else {
                    var lookDelta = InputProvider.GetLookDelta() * AnglesInputSensitivity;
                    var zoomDelta = InputProvider.GetZoomDelta() * DistanceInputSensitivity;
                    var angles = Angles + new Vector2( -lookDelta.y, lookDelta.x );
                    var distance = Distance + zoomDelta;
                    angles.x = Math.Clamp( angles.x, MinAngleX, MaxAngleX );
                    distance = Math.Clamp( distance, MinDistance, MaxDistance );
                    Angles = angles;
                    Distance = distance;
                }
                if (target.IsAlive) {
                    var distance01 = Mathf.InverseLerp( MinDistance, MaxDistance, Distance );
                    transform.localPosition = target.transform.position;
                    transform.localEulerAngles = Angles;
                    transform.Translate( 0, 0, -Distance, Space.Self );
                    transform.Translate( Vector3.LerpUnclamped( Vector3.right * 0.2f, Vector3.right * 0.6f, distance01 ), Space.Self );
                    transform.Translate( Vector3.LerpUnclamped( target.transform.up * 1.8f, target.transform.up * 2.2f, distance01 ), Space.World );
                } else {
                    transform.localPosition = target.transform.position;
                    transform.localEulerAngles = Angles;
                    transform.Translate( 0, 0, -Distance, Space.Self );
                    transform.Translate( target.transform.up * 1.5f, Space.World );
                }
                Camera.main.transform.localPosition = transform.localPosition;
                Camera.main.transform.localRotation = transform.localRotation;
                Hit = Raycast( new Ray( transform.position, transform.forward ), target.transform );
            } else {
                Hit = null;
            }
        }
        protected void LateUpdate() {
        }

        // Helpers
        private static RaycastHit? Raycast(Ray ray, Transform character) {
            var mask = ~(Masks.Entity_Approximate | Masks.Trivial);
            var hit = Utils.RaycastAll( ray, 128, mask, QueryTriggerInteraction.Ignore ).Where( i => i.transform.root != character ).OrderBy( i => i.distance ).FirstOrDefault();
            if (hit.collider) {
                return new RaycastHit(
                    hit.point,
                    hit.distance,
                    hit.collider.gameObject,
                    GetEnemy( hit.collider.gameObject, hit.point, hit.distance, character ),
                    GetThing( hit.collider.gameObject, hit.point, hit.distance, character ) );
            }
            return null;
        }
        private static EnemyCharacter? GetEnemy(GameObject gameObject, Vector3 point, float distance, Transform character) {
            if (Vector3.Distance( point, character.position ) <= 16f) {
                var @object = gameObject.transform.root.gameObject;
                return @object.GetComponent<EnemyCharacter>();
            }
            return null;
        }
        private static ThingBase? GetThing(GameObject gameObject, Vector3 point, float distance, Transform character) {
            if (Vector3.Distance( point, character.position ) <= 2.5f) {
                var @object = gameObject.transform.root.gameObject;
                return @object.GetComponent<ThingBase>();
            }
            return null;
        }

    }
    public interface ICameraInputProvider {
        PlayableCharacterBase GetTarget(out bool isNewTarget);
        Vector2 GetLookDelta();
        float GetZoomDelta();
    }
}