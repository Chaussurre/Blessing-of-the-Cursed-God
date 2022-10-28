using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace map.HexTilemap
{
    public class TilemapManager : MonoBehaviour
    {
        public float radius;
        
        [HideInInspector] public Vector2Int BoundSize = Vector2Int.zero;
        [HideInInspector] public Vector2Int BottomLeftCornerBound = Vector2Int.zero;
        [HideInInspector] public Vector2Int TopRightCornerBound = Vector2Int.zero;

        public float Scale;
        [SerializeField] private Tile Prefab;
        [SerializeField] private Transform TileParent; 
        [SerializeField, Range(0, 1)] public float SeaHeight;
        
        protected  Dictionary<Vector2Int, Tile> Tiles = new();

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

        public Vector3 CoordinatesToLocalPosition(Vector3Int coordinates)
        {
            return CoordinatesToLocalPosition(ConvertCoordinates(coordinates));
        }

        public Vector2Int ConvertCoordinates(Vector3Int coordinates)
        {
            return new Vector2Int(coordinates.x + coordinates.z, coordinates.y - coordinates.z);
        }

        private Tile newTile(Vector2Int coordinates)
        {
            var tile = Instantiate(Prefab, TileParent);
            tile.transform.localScale = new Vector3(radius, 1, radius);
            tile.transform.localPosition = CoordinatesToLocalPosition(coordinates);
            tile.SetScale(Scale);
            
            return tile;
        }
        
        public void SetTile(Vector2Int coordinates, TileType Type, float Height, bool onTop = false)
        {
            if (Tiles.ContainsKey(coordinates))
            {
                var tile = Tiles[coordinates]; 
                
                if (onTop)
                {
                    var bottomTile = newTile(coordinates);
                    bottomTile.SetInfos(tile.Type, tile.Height);
                    tile.SetInfos(Type, Height, bottomTile);
                }
                else
                    tile.SetInfos(Type, Height);
            }
            else
            {
                var tile = newTile(coordinates);
                tile.SetInfos(Type, Height);
                Tiles[coordinates] = tile;
                UpdateBounds(coordinates);
            }
        }

        public void UpdateBounds(Vector2Int coordinates)
        {
            if (coordinates.x < BottomLeftCornerBound.x)
                BottomLeftCornerBound = new Vector2Int(coordinates.x, BottomLeftCornerBound.y);
            if (coordinates.y < BottomLeftCornerBound.y)
                BottomLeftCornerBound = new Vector2Int(BottomLeftCornerBound.x, coordinates.y);

            if (coordinates.x > TopRightCornerBound.x)
                TopRightCornerBound = new Vector2Int(coordinates.x, TopRightCornerBound.y);
            if (coordinates.y > TopRightCornerBound.y)
                TopRightCornerBound = new Vector2Int(TopRightCornerBound.x, coordinates.y);

            BoundSize = TopRightCornerBound - BottomLeftCornerBound;
        }

        public void UpdateBounds(Vector3Int coordinates)
        {
            UpdateBounds(ConvertCoordinates(coordinates));
        }

        public void UpdateBounds(IEnumerable<Vector2Int> coordinatesList)
        {
            foreach (var coordinate in coordinatesList)
                UpdateBounds(coordinate);
        }

        public void UpdateBounds(IEnumerable<Vector3Int> coordinatesList)
        {
            foreach (var coordinate in coordinatesList)
                UpdateBounds(coordinate);
        }

        public void SetTile(Vector3Int coordinates, TileType Type, float Height, bool onTop = false)
        {
            SetTile(ConvertCoordinates(coordinates), Type, Height, onTop);
        }

        public List<Vector2Int> GetNeighbors(Vector2Int coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
