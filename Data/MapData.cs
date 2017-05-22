using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data for map all in one nice place
[CreateAssetMenu()]
public class MapData : ScriptableObject {

    public int width;
    public int height;
    public int seed;
    public float scale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    [Range(1,50)]
    public float heightMultiplier;
    public AnimationCurve heightCurve;

    public bool autoUpdate;

    public TerrainLevel[] terrainLevels;
}
