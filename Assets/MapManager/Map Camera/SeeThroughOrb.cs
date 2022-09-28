using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
            var rot = Quaternion.LookRotation(RoomCamera.transform.position - InsideOrb.transform.position,
                Vector3.up);
            InsideOrb.transform.rotation = rot;
            var mesh = InsideOrb.mesh;
            var orbPos = InsideOrb.transform.position;
            var orbScale = InsideOrb.transform.lossyScale;
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                var pos = RoomCamera.WorldToScreenPoint(
                    GetPointInWorld(mesh.vertices[i], orbPos, orbScale, rot));
                var UVPos = new Vector2(pos.x / RoomCamera.pixelWidth, pos.y / RoomCamera.pixelHeight);
                UVs[i] = UVPos;
            }

            mesh.uv = UVs.ToArray();
        }

        Vector3 GetPointInWorld(Vector3 point, Vector3 pos, Vector3 scale, Quaternion rot)
        {
            return rot * new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z) + pos;
        }
    }
}
