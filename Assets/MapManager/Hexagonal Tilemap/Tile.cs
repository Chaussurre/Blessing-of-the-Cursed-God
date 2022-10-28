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

        public float Scale;
        
        [SerializeField] private MeshRenderer meshRenderer;
        public float minimumHeight;

        public static readonly Vector3 up = Vector3.forward * 0.5f;
        public static readonly Vector3 leftUp = Quaternion.Euler(0, 60, 0) * Vector3.forward * 0.5f;
        public static readonly Vector3 leftDown = Quaternion.Euler(0, 120, 0) * Vector3.forward * 0.5f;

        public Tile BottomTile;
        
        public virtual void SetInfos(TileType Type, float Height, Tile BottomTile = null)
        {
            meshRenderer.material.color = Type.color;
            this.Type = Type;
            this.Height = Height;
            this.BottomTile = BottomTile;
            
            var bottomHeight = BottomTile != null ? BottomTile.Height : 0;
            bottomHeight = 0;
            
            if (Height < minimumHeight)
                Height = minimumHeight;

            var range = Height - bottomHeight;
            transform.localScale = new Vector3(1, range * Scale, 1);

            meshRenderer.transform.localPosition =
                //new Vector3(0, bottomHeight / range + 0.5f , 0);
                new Vector3(0, 0.5f , 0);
        }

        public virtual void SetMaterial(Material Mat)
        {
            meshRenderer.material = Mat;
            meshRenderer.material.color = Type.color;
        }

        public void SetScale(float Scale)
        {
            this.Scale = Scale;
        }
    }
}
