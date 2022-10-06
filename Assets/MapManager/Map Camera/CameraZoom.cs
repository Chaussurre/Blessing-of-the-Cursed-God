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

            public float Length()
            {
                return Mathf.Abs(End - Start);
            }
        }
        
        public ZoomRange RoomCameraZoomRange;
        public ZoomRange MapCameraZoomRange;

        public UnityEngine.Camera RoomCamera;
        public UnityEngine.Camera MapCamera;
        public SetUpRenderTexture Texture;
        
        public float ZoomStep;
        public float ZoomSmoothSpeed;

        private float zoomSensitivityRoom;
        private float zoomSensitivityMap;
            
        private float zoom;
        private float zoomTarget;

        private void Start()
        {
            zoomSensitivityMap = ZoomStep / MapCameraZoomRange.Length();
            zoomSensitivityRoom = ZoomStep / RoomCameraZoomRange.Length();
        }

        private void Update()
        {
            var mouse = Mouse.current;

            if (mouse != null)
                Zoom(mouse.scroll.ReadValue().y);
            
        }

        private void Zoom(float zoomDelta)
        {
            float zoomSensitivity = zoom < 1 ? zoomSensitivityRoom : zoomSensitivityMap;
            
            zoomTarget = Mathf.Clamp(zoomTarget + zoomDelta * zoomSensitivity, 0, 2);
            zoom = Mathf.Lerp(zoom, zoomTarget, ZoomSmoothSpeed * Time.deltaTime);
            
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
                
                var color = MapCamera.backgroundColor; 
                MapCamera.backgroundColor = new Color(color.r, color.g, color.b, zoom);
            }
        }
    }
}
