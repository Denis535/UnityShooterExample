﻿#nullable enable
namespace Project.Game_ {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    internal class CharacterInput : ICharacterInput {

        private InputActions_Character Input { get; }
        //public bool IsEnabled {
        //    get => Input.Character.enabled;
        //    set {
        //        if (value) Input.Enable(); else Input.Disable();
        //    }
        //}
        private CharacterBase Character { get; }
        private Camera2 Camera { get; }
        private Camera2.RaycastHit? Hit => Camera.Hit;
        private Vector3 Target => Camera.Hit?.Point ?? Camera.transform.TransformPoint( Vector3.forward * 128f );

        public CharacterInput(InputActions_Character input, CharacterBase character, Camera2 camera) {
            Input = input;
            Character = character;
            Camera = camera;
        }

        public Vector3 GetMoveVector() {
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Pipe( i => new Vector3( i.x, 0, i.y ) );
                vector = Camera.transform.TransformDirection( vector );
                vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                return vector;
            } else {
                return Vector3.zero;
            }
        }
        public Vector3? GetBodyTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                var vector = Input.Character.Move.ReadValue<Vector2>().Pipe( i => new Vector3( i.x, 0, i.y ) );
                if (vector != Vector3.zero) {
                    vector = Camera.transform.TransformDirection( vector );
                    vector = new Vector3( vector.x, 0, vector.z ).normalized * vector.magnitude;
                    return Character.transform.position + vector;
                }
            }
            return null;
        }
        public Vector3? GetHeadTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                return Target;
            }
            return Target;
        }
        public Vector3? GetWeaponTarget() {
            if (Input.Character.Aim.IsPressed() || Input.Character.Fire.IsPressed()) {
                return Target;
            }
            if (Input.Character.Move.IsPressed()) {
                return null;
            }
            return null;
        }
        public bool IsJumpPressed() {
            return Input.Character.Jump.IsPressed();
        }
        public bool IsCrouchPressed() {
            return Input.Character.Crouch.IsPressed();
        }
        public bool IsAcceleratePressed() {
            return Input.Character.Accelerate.IsPressed();
        }
        public bool IsFirePressed() {
            return Input.Character.Fire.IsPressed();
        }
        public bool IsAimPressed() {
            return Input.Character.Aim.IsPressed();
        }
        public bool IsInteractPressed(out MonoBehaviour? interactable) {
            interactable = (MonoBehaviour?) Hit?.Enemy ?? Hit?.Thing;
            return Input.Character.Interact.WasPressedThisFrame();
        }

    }
}
