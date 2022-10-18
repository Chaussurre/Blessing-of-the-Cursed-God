using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace map.HexTilemap
{
    [CreateAssetMenu]
    public class TileType : ScriptableObject
    {
        public float preferredHeight;
        public bool forceHeight;
        
        public Color color;
    }
}
