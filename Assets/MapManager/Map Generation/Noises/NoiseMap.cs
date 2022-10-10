using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoiseMap : MonoBehaviour
{
    public abstract float GetValue(Vector2 coordinates);
}
