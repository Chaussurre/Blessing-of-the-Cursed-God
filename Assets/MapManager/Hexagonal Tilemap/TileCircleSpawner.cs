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
        public float Height;
        public Tile.TileInfos infos;

        [SerializeField] private bool SpawnOnStart;

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
                        CreateTile(new Vector3Int(x, y, z));
                }
            }
        }

        void CreateTile(Vector3Int coordinates)
        {
            var tileInfos = infos;
            tileInfos.Height = Random.Range(0f, Height);
            
            TilemapManager.SetTile(coordinates, tileInfos);
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
