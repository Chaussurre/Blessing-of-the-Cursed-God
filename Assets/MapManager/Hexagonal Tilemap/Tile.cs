using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace map.HexTilemap
{
    public class Tile : MonoBehaviour
    {

        public TileType Type;
        public float Height;
        public MeshRenderer meshRenderer;
        public float minimumHeight;

        public static readonly Vector3 up = Vector3.forward * 0.5f;
        public static readonly Vector3 leftUp = Quaternion.Euler(0, 60, 0) * Vector3.forward * 0.5f;
        public static readonly Vector3 leftDown = Quaternion.Euler(0, 120, 0) * Vector3.forward * 0.5f;

        public virtual void SetInfos(TileType Type, float Height)
        {
            if (Type.Material)
                meshRenderer.material = Type.Material;
            
            meshRenderer.material.color = Type.color;
            meshRenderer.transform.localPosition = Vector3.up * 0.5f;
            transform.localScale = new Vector3(1, Height + minimumHeight, 1);
            this.Type = Type;
            this.Height = Height;
        }
    }
}
