using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace map.HexTilemap
{
    [CreateAssetMenu]
    public class TileType : ScriptableObject
    {
        [Serializable]
        public struct TileInfos
        {
            public Color color;
            public float Height;
        }

        public TileInfos Infos;
    }
}
