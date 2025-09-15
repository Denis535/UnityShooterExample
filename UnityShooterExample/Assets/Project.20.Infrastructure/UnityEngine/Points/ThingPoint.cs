#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class ThingPoint : Point {

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            this.gameObject.isStatic = true;
            this.transform.localPosition = Snapping.Snap( this.transform.localPosition, Vector3.one * 0.5f );
            this.transform.localEulerAngles = Snapping.Snap( this.transform.localEulerAngles, Vector3.one * 45f );
            this.transform.localEulerAngles = new Vector3( 0, this.transform.localEulerAngles.y, 0 );
            if (this.transform.parent == null) this.transform.parent = GameObject.Find( "World" )?.transform;
        }
#endif

#if UNITY_EDITOR
        protected override void OnDrawGizmos() {
            var size = HandleUtility.GetHandleSize( this.transform.position ).Chain( i => Math.Clamp( i, 1f, 20f ) );
            Gizmos.color = Color.yellow;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawSphere( Vector3.zero, size * 0.1f );
            Gizmos.DrawFrustum( Vector3.zero, 30f, size * 0.5f, 0f, 2f );
        }
#endif

    }
}
