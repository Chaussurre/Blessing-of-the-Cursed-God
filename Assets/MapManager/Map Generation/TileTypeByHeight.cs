using System.Collections;
using System.Collections.Generic;
using map.HexTilemap;
using UnityEngine;

namespace Map.Generation
{
    public class TileTypeByHeight : MonoBehaviour
    {
        public List<TileType> Types;
        public float reduceHeight; //used to have larger seas
        
        public TileType GetTileInfos(ref float height)
        {
            float distance = float.PositiveInfinity;
            TileType result = null;

            height -= reduceHeight;
            
            foreach (var type in Types)
            {
                var newDistance = Mathf.Abs(type.preferredHeight - height);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    result = type;
                }
            }

            if (result != null && result.forceHeight)
                height = result.preferredHeight;
            
            return result;
        }
    }
}
