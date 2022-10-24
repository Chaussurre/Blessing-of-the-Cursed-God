using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common.Serialization;
using map.HexTilemap;
using MathExtra.NoiseMaps;
using UnityEditor;
using UnityEngine;

namespace Map.Generation
{
    public class MapGenHeightByNoise : MonoBehaviour
    {
        public PerlinNoise perlin;
        public WorleyNoise worley;

        public TileCircleSpawner TilesCoordinates;
        public TilemapManager MapManager;
        public TileTypeByHeight TileType;

        public float MaxHeight;

        [Range(0, 1)] public float ShiftHeight;
        [Range(0, 1)] public float flatten;
        
        void Start()
        {
            Generate();
        }

        public void Generate()
        {
            TilesCoordinates.CreateCircle();
            MapManager.UpdateBounds(TilesCoordinates.Coordinates);

            foreach (var coordinates in TilesCoordinates.Coordinates)
            {
                var LocalPos = MapManager.CoordinatesToLocalPosition(coordinates);
                var pos = new Vector2(LocalPos.x, LocalPos.z);
                pos += (Vector2)MapManager.BottomLeftCornerBound * MapManager.radius;
                pos = new Vector2(pos.x / MapManager.BoundSize.x, pos.y / MapManager.BoundSize.y);

                var height = (perlin.NoiseValue(pos) * worley.NoiseValue(pos) - ShiftHeight) * (1 - flatten);
                var type = TileType.GetTileInfos(ref height);
                
                MapManager.SetTile(coordinates, type, height * MaxHeight);
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(MapGenHeightByNoise))]
    public class MapGenHeightByNoiseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var MapGen = target as MapGenHeightByNoise;

            if (Application.isPlaying && MapGen)
            {
                if (GUILayout.Button("Regenerate"))
                    MapGen.Generate();
            }
        }
    }
#endif
}
