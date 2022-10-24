#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MathExtra.NoiseMaps.Tests
{
    public class PerlinNoiseTexture : MonoBehaviour
    {
        public Vector2Int TextureSize;

        public MeshRenderer renderer;
        private Texture2D texture = null;

        public PerlinNoise perlin;

        public bool UpdateTextureEveryFrame;
        
        private void Start()
        {
            texture = new Texture2D(TextureSize.x, TextureSize.y);
            texture.filterMode = FilterMode.Point;
            renderer.material.mainTexture = texture;
            UpdateTexture();
        }

        private void Update()
        {
            if(UpdateTextureEveryFrame)
                UpdateTexture();
        }

        public void UpdateTexture()
        {
            for (int x = 0; x < TextureSize.x; x++)
            for (int y = 0; y < TextureSize.y; y++)
            {
                var pos = new Vector2((float)x / TextureSize.x, (float)y / TextureSize.y);
                var value = perlin.NoiseValue(pos);
                texture.SetPixel(x, y, new Color(value, value, value));
            }
            texture.Apply();   
        }
    }
}
#endif