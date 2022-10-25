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
        [SerializeField] private float ChangeMax;
        [SerializeField] private UnityEngine.Camera MapCamera;
        [SerializeField] private Transform Map;
        
        private void Update()
        {
            var mouse = Mouse.current;
            if (!Zoom.MapCameraActive || mouse == null || !mouse.leftButton.isPressed)
                return; //check that the map is active and the user is left clicking
            
            var plane = new Plane(Vector3.up, 0); //The horizontal plane
            
            //both mouse positions before and after movements
            var mouseEnd = mouse.position.ReadValue();
            var mouseStart = mouseEnd - mouse.delta.ReadValue(); 

            //The rays we are casting
            var startRay = MapCamera.ScreenPointToRay(mouseStart);
            var endRay = MapCamera.ScreenPointToRay(mouseEnd);

            //if both rays hit the horizontal plane
            if (plane.Raycast(startRay, out var StartEnter) &&
                plane.Raycast(endRay, out var EndEnter))
            {
                //the collision positions of the rays
                var startPos = startRay.origin + startRay.direction * StartEnter;
                var endPos = endRay.origin + endRay.direction * EndEnter;
                
                if (endPos.magnitude > 3 * RadiusMax || startPos.magnitude > 3* RadiusMax)
                    return;

                //we move the camera
                transform.position -= endPos - startPos;

                var horizontalPos = Vector3.ProjectOnPlane(transform.position, Vector3.up);
                var verticalPos = transform.position.y;
                if (horizontalPos.magnitude > RadiusMax)
                    transform.position = horizontalPos.normalized * RadiusMax + verticalPos * Vector3.up;
            }
        }
    }
}
