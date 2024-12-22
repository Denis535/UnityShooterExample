#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ICameraInputProvider {
        PlayableCharacterBase GetTarget();
        Vector2 GetRotateDelta();
        float GetZoomDelta();
    }
}
