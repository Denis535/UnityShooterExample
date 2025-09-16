#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IPlayableCameraInputProvider {

        PlayableCharacterBase GetTarget();
        Vector2 GetRotateDelta();
        float GetZoomDelta();

    }
}
