using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Map.Camera
{
    public class SeeThroughMat : MonoBehaviour
    {
        public MeshFilter SeeThroughTarget;
        public UnityEngine.Camera RoomCamera;

        private List<Vector2> UVs = new List<Vector2>(1000);
        
        private void Awake()
        {
            foreach (var uv in SeeThroughTarget.mesh.uv)
                UVs.Add(uv);

            transform.position = SeeThroughTarget.transform.position;
            
            InverseTriangles();
        }
        
        void InverseTriangles()
        {
            var triangles = new List<int>(SeeThroughTarget.mesh.triangles);
            for (int i = 0; i < triangles.Count; i += 3)
                (triangles[i], triangles[i + 1]) = (triangles[i + 1], triangles[i]);
            SeeThroughTarget.mesh.triangles = triangles.ToArray();
        }

        private void LateUpdate()
        {
            var mesh = SeeThroughTarget.mesh;
            var orbPos = SeeThroughTarget.transform.position;
            var orbScale = SeeThroughTarget.transform.lossyScale;
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                var pos = RoomCamera.WorldToScreenPoint(
                    GetPointInWorld(mesh.vertices[i], orbPos, orbScale));
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
