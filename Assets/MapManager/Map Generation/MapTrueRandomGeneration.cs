using System;
using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Map.Generation
{
    public class MapTrueRandomGeneration : MonoBehaviour
    {
        public TileCircleSpawner TileCoordinates;
        public TilemapManager MapManager;
        public TileTypeByHeight TileTypeManager;

        public float MaxHeight;
        
        public bool SetSeed;
        public int Seed;

        private void Start()
        {
            GenerateMap();
        }

        public void InitRandom()
        {
            if (SetSeed)
                Random.InitState(Seed);
        }
        
        public void GenerateMap()
        {
            TileCoordinates.CreateCircle();
            
            InitRandom();

            foreach (var coordinates in TileCoordinates.Coordinates)
            {
                var height = Random.Range(0f, 1f);

                var tileType = TileTypeManager.GetTileInfos(height);
                
                MapManager.SetTile(coordinates, tileType, height * MaxHeight);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MapTrueRandomGeneration))]
    class MapGenerationManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var mapGen = target as MapTrueRandomGeneration;

            if (Application.isPlaying && GUILayout.Button("Regenerate"))
            {
                mapGen.GenerateMap();
            }
        }
    }
#endif
}
