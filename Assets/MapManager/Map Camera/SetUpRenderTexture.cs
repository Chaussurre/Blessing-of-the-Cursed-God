using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Camera
{
    public class SetUpRenderTexture : MonoBehaviour
    {
        public UnityEngine.Camera MapCamera;
        public MeshRenderer InsideOrb;

        [HideInInspector] public RenderTexture Texture;

        public int depth = 0;

        private void Start()
        {
            ResetTexture();
            MapCamera.enabled = true;
        }

        [ContextMenu(nameof(ResetTexture))]
        private void ResetTexture()
        {
            Texture = new RenderTexture(Screen.width, Screen.height, depth);
            Texture.antiAliasing = 1;
            
            InsideOrb.sharedMaterial.mainTexture = Texture;
            MapCamera.targetTexture = Texture;
        }
    }
}
