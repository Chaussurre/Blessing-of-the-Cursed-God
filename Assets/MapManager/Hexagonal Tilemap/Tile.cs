using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace map.HexTilemap
{
    public class Tile : MonoBehaviour
    {
        [Serializable]
        public struct TileInfos
        {
            public Color color;
            public float Height;
        }

        public TileInfos infos;
        public MeshRenderer meshRenderer;

        public static readonly Vector3 up = Vector3.forward * 0.5f;
        public static readonly Vector3 leftUp = Quaternion.Euler(0, 60, 0) * Vector3.forward * 0.5f;
        public static readonly Vector3 leftDown = Quaternion.Euler(0, 120, 0) * Vector3.forward * 0.5f;

        public virtual void SetInfos(TileInfos newInfos)
        {
            meshRenderer.material.color = newInfos.color;
            meshRenderer.transform.localPosition = Vector3.up * newInfos.Height;
            infos = newInfos;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            Gizmos.DrawRay(position, up);
            Gizmos.DrawRay(position, leftUp);
            Gizmos.DrawRay(position, leftDown);
        }
#endif
    }
}
