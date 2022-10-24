using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;

namespace Map.Generation
{
    public abstract class PostGenerationEffect : MonoBehaviour
    {
        abstract public void ApplyEffect(TilemapManager mapManager, TileTypeByHeight TileType, List<Vector3Int> coordinates);
    }
}
