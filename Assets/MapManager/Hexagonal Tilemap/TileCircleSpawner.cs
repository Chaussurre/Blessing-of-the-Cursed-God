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

        public readonly List<Vector3Int> Coordinates = new();

        private void Start()
        {
            if(SpawnOnStart)
                CreateCircle();
        }

        public void CreateCircle()
        {
            Coordinates.Clear();
            
            for (int x = 1; x <= radius; x++)
            {
                var radiusX = radius - x;
                for (int y = 1; y <= radiusX; y++)
                {
                    Coordinates.Add(new Vector3Int(x, y, 0));
                    Coordinates.Add(new Vector3Int(-x, -y, 0));
                    Coordinates.Add(new Vector3Int(x, 0, y));
                    Coordinates.Add(new Vector3Int(-x, 0, -y));
                    Coordinates.Add(new Vector3Int(0, x, -y));
                    Coordinates.Add(new Vector3Int(0, -x, y));
                }

                Coordinates.Add(new Vector3Int(x, 0, 0));
                Coordinates.Add(new Vector3Int(-x, 0, 0));
                Coordinates.Add(new Vector3Int(0, x, 0));
                Coordinates.Add(new Vector3Int(0, -x, 0));
                Coordinates.Add(new Vector3Int(0, 0, x));
                Coordinates.Add(new Vector3Int(0, 0, -x));
            }

            Coordinates.Add(new Vector3Int(0, 0, 0));
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
