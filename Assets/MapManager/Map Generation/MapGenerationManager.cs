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
    public class MapGenerationManager : MonoBehaviour
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
            InitRandom();
            
            var coordinates = TileCoordinates.Coordinates;

            foreach (var c in coordinates)
            {
                //var height = Random.Range(0f, 1f);

                //var tile = MapManager.GetTile(c);
                //if (tile)
                //    continue;

                var height =  
                    c.z == 0 ? 0.9f :
                    c.y == 0 ? 0.5f : 
                    c.x == 0 ? 0.2f :
                    0;
                
                var tileType = TileTypeManager.GetTileInfos(ref height);
                
                MapManager.SetTile(c, tileType, height * MaxHeight);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MapGenerationManager))]
    class MapGenerationManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var mapGen = target as MapGenerationManager;

            if (Application.isPlaying && GUILayout.Button("Regenerate"))
            {
                mapGen.GenerateMap();
            }
        }
    }
#endif
}
