using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Camera
{
    public class CameraSynchroniser : MonoBehaviour
    {
        public Transform Camera;
        public Transform OtherCamera;
        
        public Transform OtherCameraCenter;
        private void LateUpdate()
        {
                OtherCameraCenter.rotation = transform.rotation;
                OtherCamera.localPosition = Camera.localPosition;
        }
    }
}
