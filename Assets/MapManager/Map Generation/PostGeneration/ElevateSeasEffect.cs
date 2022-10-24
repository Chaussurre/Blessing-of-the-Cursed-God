using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;

namespace Map.Generation
{
    public class ElevateSeasEffect : PostGenerationEffect
    {
        [Min(0)]
        public float differenceLandSea;

        public TileType Sea;
        
        public override void ApplyEffect(TilemapManager mapManager, TileTypeByHeight TileType, List<Vector3Int> coordinates)
        {
            float minHeight = float.PositiveInfinity;
            
            foreach (var coord in coordinates)
            {
                var tile = mapManager.GetTile(coord);
                if (tile.Type != Sea && tile.Height < minHeight)
                    minHeight = tile.Height;
            }

            minHeight -= differenceLandSea;
            
            foreach (var coord in coordinates)
            {
                var tile = mapManager.GetTile(coord);
                if (tile.Type == Sea)
                    tile.SetInfos(Sea, minHeight);
            }
        }
    }
}
