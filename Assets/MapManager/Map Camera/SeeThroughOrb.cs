using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Camera
{
    public class SeeThroughOrb : MonoBehaviour
    {
        public MeshFilter InsideOrb;
        public UnityEngine.Camera RoomCamera;

        private List<Vector2> UVs = new List<Vector2>(1000);

        private void Start()
        {
            foreach (var uv in InsideOrb.mesh.uv)
                UVs.Add(uv);
        }

        private void LateUpdate()
        {
            var mesh = InsideOrb.mesh;
            var orbPos = InsideOrb.transform.position;
            var orbScale = InsideOrb.transform.lossyScale;
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                var pos = RoomCamera.WorldToScreenPoint(GetPointInWorld(mesh.vertices[i], orbPos, orbScale));
                var UVPos = new Vector2(pos.x / RoomCamera.pixelWidth, pos.y / RoomCamera.pixelHeight);
                UVs[i] = UVPos;
            }

            mesh.uv = UVs.ToArray();
        }

        Vector3 GetPointInWorld(Vector3 point, Vector3 pos, Vector3 scale)
        {
            return new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z) + pos;
        }
    }
}
