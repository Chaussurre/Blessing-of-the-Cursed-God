using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MathExtra.NoiseMaps
{
    [Serializable]
    public struct WorleyNoise
    {
        public int seed;
        public float scaleInput;
        public float MaxDistance;
        public float radialOffset;

        public Vector2 GetPointPos(Vector2Int coordinates)
        {
            //Associate each unique coordinates with an unique integer
            int delta = (coordinates.x + coordinates.y) * (coordinates.x + coordinates.y + 1) / 2 + coordinates.y;

            //60 is an arbitrary numbers to make sure close seeds are not similar
            Random.InitState(seed * 60 + delta); 
            float angle = Random.Range(0f, 360f) + radialOffset;
            angle *= Mathf.Deg2Rad;

            var pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(0f, 1f);
            return pos + coordinates;
        }

        private float GetShortestDistance(Vector2 pos)
        {
            var coordiantes = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));

            float distance = float.PositiveInfinity;

            for(int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                var point = GetPointPos(coordiantes + new Vector2Int(x, y));
                float newDist = Vector2.Distance(pos, point);
                if (newDist < distance)
                {
                    distance = newDist;
                }
            }

            return distance;
        }

        public float NoiseValue(Vector2 pos)
        {
            pos *= scaleInput;

            float value = GetShortestDistance(pos);
            value = Mathf.Clamp01(value / MaxDistance);

            return value;
        }
    }
}
