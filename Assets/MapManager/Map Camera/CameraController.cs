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
        
        public bool inverseVerticalCamera;

        private int zoomSteps;
        private int MaxZoomSteps;

        private RenderTexture mapCameraRenderTexture;
        
        private float angleX;
        private float angleY;

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null)
                return;
            
            if (mouse.middleButton.isPressed)
                RotateCameras(mouse.delta.ReadValue());
        }

        void RotateCameras(Vector2 mouseDelta)
        {

            angleX += mouseDelta.x * sensitivity;
            angleX %= 360;

            if (inverseVerticalCamera)
                angleY += mouseDelta.y * sensitivity;
            else
                angleY -= mouseDelta.y * sensitivity;
            
            angleY = Mathf.Clamp(angleY, AngleMin, AngleMax);

            transform.eulerAngles = new Vector3(angleY, angleX);
        }
    }
}