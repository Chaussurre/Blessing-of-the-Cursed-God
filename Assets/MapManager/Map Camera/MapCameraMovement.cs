using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Map.Camera
{
    public class MapCameraMovement : MonoBehaviour
    {
        [SerializeField] private CameraZoom Zoom;
        [SerializeField] private float RadiusMax;
        [SerializeField] private UnityEngine.Camera MapCamera;
        
        private void Update()
        {
            var mouse = Mouse.current;
            if (!Zoom.MapCameraActive || mouse == null || !mouse.leftButton.isPressed)
                return;
            
            var plane = new Plane(Vector3.up, 0); //The horizontal plane
            var mouseEnd = mouse.position.ReadValue();
            var mouseStart = mouseEnd - mouse.delta.ReadValue();

            var startRay = MapCamera.ScreenPointToRay(mouseStart);
            var endRay = MapCamera.ScreenPointToRay(mouseEnd);

            if (plane.Raycast(startRay, out var StartEnter) &&
                plane.Raycast(endRay, out var EndEnter))
            {
                var startPos = startRay.origin + startRay.direction * StartEnter;
                var endPos = endRay.origin + endRay.direction * EndEnter;

                transform.position -= endPos - startPos;
            }
        }
    }
}
