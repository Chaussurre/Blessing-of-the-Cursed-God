using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace map.HexTilemap
{
    public class TileCircleSpawner : MonoBehaviour
    {
        public TilemapManager TilemapManager;

        public int radius;

        [SerializeField] private bool SpawnOnStart;

        [HideInInspector] public readonly List<Vector3Int> Coordinates = new();

        private void Start()
        {
            if(SpawnOnStart)
                CreateCircle();
        }

        public void CreateCircle()
        {
            for (int x = -radius; x <= radius; x++)
            {
                var radiusX = radius - Mathf.Abs(x);
                for (int y = -radiusX; y <= radiusX; y++)
                {
                    var radiusXY = radiusX - Mathf.Abs(y);
                    for (int z = -radiusXY; z <= radiusXY; z++)
                        Coordinates.Add(new Vector3Int(x, y, z));
                }
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TileCircleSpawner))]
    public class TileCircleSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (EditorApplication.isPlaying && GUILayout.Button("SpawnCircle"))
            {
                (target as TileCircleSpawner)?.CreateCircle();
            }
        }
    }

#endif
}
