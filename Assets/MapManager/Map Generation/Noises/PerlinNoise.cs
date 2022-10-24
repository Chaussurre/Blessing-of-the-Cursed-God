using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MathExtra.NoiseMaps
{   
    [Serializable]
    public struct PerlinNoise
    {
        public int seed;
        public float scaleInput;
        public float radialOffset; //change this value to rotate gradients

        public PerlinNoise(int seed, float scaleInput)
        {
            this.seed = seed;
            this.scaleInput = scaleInput;
            radialOffset = 0;
        }

        public Vector2 GetGradient(Vector2Int coordinates)
        {
            //Associate each unique coordinates with an unique integer
            int delta = (coordinates.x + coordinates.y) * (coordinates.x + coordinates.y + 1) / 2 + coordinates.y;

            Random.InitState(seed + delta);
            float angle = Random.Range(0, 360) + radialOffset;
            angle *= Mathf.Deg2Rad;
            
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public float GetNodeWeight(Vector2Int NodeCoordinates, Vector2 pos)
        {
            return Vector2.Dot(GetGradient(NodeCoordinates), pos - NodeCoordinates);
        }

        public float NoiseValue(Vector2 pos)
        {
            //scaling
            pos *= scaleInput;
            
            //coordinates of the cell
            Vector2Int BottomLeftCoordinates = new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
            
            //Get all weight from each corner of the cell
            float weightBotLeft = GetNodeWeight(BottomLeftCoordinates, pos);
            float weightBotRight = GetNodeWeight(BottomLeftCoordinates + Vector2Int.right, pos);
            float weightTopLeft = GetNodeWeight(BottomLeftCoordinates + Vector2Int.up, pos);
            float weightTopRight = GetNodeWeight(BottomLeftCoordinates + Vector2Int.one, pos);
            
            //interpolating
            float lerpx = pos.x - BottomLeftCoordinates.x;
            float lerpy = pos.y - BottomLeftCoordinates.y;
            
            float lerpBot = Mathf.Lerp(weightBotLeft, weightBotRight, lerpx);
            float lerpTop = Mathf.Lerp(weightTopLeft, weightTopRight, lerpx);
            
            return Mathf.Lerp(lerpBot, lerpTop, lerpy) * 0.5f + 0.5f;
        }
    }
}
