using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Map.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        public AnimationCurve ZoomCurve;

        [Serializable]
        public struct ZoomRange
        {
            public float Start;
            public float End;
        }
        
        public ZoomRange RoomCameraZoomRange;
        public ZoomRange MapCameraZoomRange;

        public UnityEngine.Camera RoomCamera;
        public UnityEngine.Camera MapCamera;
        public SetUpRenderTexture Texture;
        
        public float ZoomSensitivity;

        private float zoom;
        
        private void Update()
        {
            var mouse = Mouse.current;

            if (mouse != null)
                Zoom(mouse.scroll.ReadValue().y);
            
        }

        private void Zoom(float zoomDelta)
        {
            zoom = Mathf.Clamp(zoom + zoomDelta * ZoomSensitivity, 0, 2);
            
            var zoomRoom = Mathf.Clamp01(zoom);
            var zoomMap = Mathf.Clamp01(zoom - 1);

            float RoomCameraPos = Mathf.Lerp(RoomCameraZoomRange.Start, RoomCameraZoomRange.End,
                ZoomCurve.Evaluate(zoomRoom));
            float MapCameraPos = Mathf.Lerp(MapCameraZoomRange.Start, MapCameraZoomRange.End,
                ZoomCurve.Evaluate(zoomMap));

            RoomCamera.transform.localPosition = Vector3.back * RoomCameraPos; 
            MapCamera.transform.localPosition = Vector3.back * (MapCameraPos + RoomCameraPos);

            if (zoom >= 1)
            {
                RoomCamera.enabled = false;
                MapCamera.targetTexture = null;
            }
            else
            {
                RoomCamera.enabled = true;
                MapCamera.targetTexture = Texture.Texture;
            }
        }
    }
}
