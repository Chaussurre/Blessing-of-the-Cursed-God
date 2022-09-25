using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace map.HexTilemap
{
    public class TilemapManager : MonoBehaviour
    {
        public float radius;
        public float depth;

        [SerializeField] private Tile Prefab;
        protected  Dictionary<Vector2, Tile> Tiles = new();

        public Tile GetTile(Vector2Int coordinates)
        {
            if (Tiles.ContainsKey(coordinates))
                return Tiles[coordinates];
            return null;
        }

        public Tile GetTile(Vector3Int coordinates)
        {
            return GetTile(ConvertCoordinates(coordinates));
        }

        public Vector3 CoordinatesToLocalPosition(Vector2Int coordinates)
        {
            return (Tile.up + Tile.leftUp) * (radius * coordinates.y) +
                   (Tile.leftUp + Tile.leftDown) * (radius * coordinates.x);
        }

        private Vector2Int ConvertCoordinates(Vector3Int coordinates)
        {
            return new Vector2Int(coordinates.x + coordinates.z, coordinates.y - coordinates.z);
        }

        public void SetTile(Vector2Int coordinates, Tile.TileInfos infos)
        {
            if (Tiles.ContainsKey(coordinates))
            {
                Tiles[coordinates].SetInfos(infos);
            }
            else
            {
                var tile = Instantiate(Prefab, transform);
                tile.transform.localScale = new Vector3(radius, depth, radius);
                tile.transform.localPosition = CoordinatesToLocalPosition(coordinates);
                tile.SetInfos(infos);
                Tiles[coordinates] = tile;
            }
        }

        public void SetTile(Vector3Int coordinates, Tile.TileInfos infos)
        {
            SetTile(ConvertCoordinates(coordinates), infos);
        }
    }
}
