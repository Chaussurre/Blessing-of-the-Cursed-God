using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Map.Camera
{
    public class CameraController : MonoBehaviour
    {
        public float sensitivity;
        public float AngleMax;
        public float AngleMin;

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null || !mouse.middleButton.isPressed)
                return;
            var direction = mouse.delta.ReadValue();
            
            transform.Rotate(Vector3.up, direction.x * sensitivity);
        }
    }
}