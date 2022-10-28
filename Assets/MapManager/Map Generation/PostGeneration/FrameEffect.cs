using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;

namespace Map.Generation
{
    public class FrameEffect : PostGenerationEffect
    {
        public TileCircleSpawner TileCircleSpawner;
        public TileType FrameTile;

        public float frameHeight = 0.5f;
        
        public override void ApplyEffect(TilemapManager mapManager, List<Vector3Int> coordinates)
        {
            foreach (var coord in TileCircleSpawner.Exterior())
            {
                mapManager.SetTile(coord, FrameTile, frameHeight);
            }
        }
    }
}
