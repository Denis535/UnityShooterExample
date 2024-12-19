#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    public interface ICharacterInputProvider {
        Vector3? GetBodyTarget();
        Vector3? GetHeadTarget();
        Vector3? GetWeaponTarget();
        Vector3 GetMoveVector();
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed(out PlayerBase player);
        bool IsAimPressed();
        bool IsInteractPressed(out EntityBase? interactable);
    }
}
