#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class Utils {

        private static readonly RaycastHit[] RaycastHitBuffer = new RaycastHit[ 256 ];
        private static readonly Collider[] ColliderBuffer = new Collider[ 256 ];

        public static IEnumerable<RaycastHit> RaycastAll(Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction) {
            var count = Physics.RaycastNonAlloc( ray, RaycastHitBuffer, maxDistance, mask, QueryTriggerInteraction.Ignore );
            return RaycastHitBuffer.Take( count );
        }

        public static IEnumerable<Collider> OverlapSphere(Vector3 position, float radius, int mask, QueryTriggerInteraction queryTriggerInteraction) {
            var count = Physics.OverlapSphereNonAlloc( position, radius, ColliderBuffer, mask, QueryTriggerInteraction.Ignore );
            return ColliderBuffer.Take( count );
        }

        public static T Random<T>(this IEnumerable<T> source) {
            var index = UnityEngine.Random.Range( 0, source.Count() );
            return source.ElementAt( index );
        }

        public static void Clear() {
            Array.Clear( RaycastHitBuffer, 0, RaycastHitBuffer.Length );
            Array.Clear( ColliderBuffer, 0, ColliderBuffer.Length );
        }

    }
    public static class Tags {

        public static readonly string Entity = "Entity";

    }
    public static class Layers {

        public static readonly int Entity = GetLayer( "Entity" );
        public static readonly int Entity_Approximate = GetLayer( "Entity-Approximate" );
        public static readonly int Entity_Exact = GetLayer( "Entity-Exact" );
        public static readonly int Trivial = GetLayer( "Trivial" );

        public static int GetLayer(string name) {
            var layer = LayerMask.NameToLayer( name );
            Assert.Operation.Message( $"Can not find {name} layer" ).Valid( layer != -1 );
            return layer;
        }
        public static string GetName(int layer) {
            var name = LayerMask.LayerToName( layer );
            Assert.Operation.Message( $"Can not find {layer} layer name" ).Valid( name != null );
            return name;
        }

    }
    public static class Masks {

        public static readonly int Entity = GetMask( "Entity" );
        public static readonly int Entity_Approximate = GetMask( "Entity-Approximate" );
        public static readonly int Entity_Exact = GetMask( "Entity-Exact" );
        public static readonly int Trivial = GetMask( "Trivial" );

        public static int GetMask(string name) {
            var layer = Layers.GetLayer( name );
            Assert.Operation.Message( $"Can not find {name} layer" ).Valid( layer != -1 );
            return 1 << layer;
        }

    }
}
