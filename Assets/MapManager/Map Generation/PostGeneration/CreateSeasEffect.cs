using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;

namespace Map.Generation
{
    public class CreateSeasEffect : PostGenerationEffect
    {
        [Min(0)]
        public float differenceLandSea;

        public TileType SeaTile;
        public TileType OceanFloorTile;

        public Material OceanMat;
        
        public override void ApplyEffect(TilemapManager mapManager , List<Vector3Int> coordinates)
        {
            var tileMapPos = mapManager.transform.localPosition;
            tileMapPos = new Vector3(tileMapPos.x, -mapManager.SeaHeight * mapManager.Scale, tileMapPos.z);
            mapManager.transform.localPosition = tileMapPos;

            foreach (var coord in coordinates)
            {
                var tile = mapManager.GetTile(coord);

                if (tile.Height <= mapManager.SeaHeight + differenceLandSea)
                {
                    var bottomHeight = Mathf.Min(mapManager.SeaHeight - differenceLandSea, tile.Height);


                    if (OceanFloorTile)
                    {
                        mapManager.SetTile(coord, OceanFloorTile, bottomHeight);
                        mapManager.SetTile(coord, SeaTile, mapManager.SeaHeight, onTop: true);
                    }
                    else
                        mapManager.SetTile(coord, SeaTile, mapManager.SeaHeight);

                    tile.SetMaterial(OceanMat);
                }
            }
        }
    }
}
