using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes a falloff noise map and then overlays it on a noisemap to drop edges to 0 making more of an island formation
public static class IslandNoiseMap {

    public static float[,] ParseToIslandNoise(float[,] noiseMap)
    {
        float[,] islandFalloff = GenerateIslandFalloff(noiseMap.GetLength(0), noiseMap.GetLength(1));

        for (int x = 0; x < noiseMap.GetLength(0); x++)
        {
            for (int y = 0; y < noiseMap.GetLength(1); y++)
            {
                noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - islandFalloff[x, y]);
            }
        }

        return noiseMap;
    }

    public static float[,] GenerateIslandFalloff(int width, int height)
    {
        float[,] map = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float yVal = y / (float)height * 2 - 1;
                float xVal = x / (float)width * 2 - 1;

                float val = Mathf.Max(Mathf.Abs(xVal), Mathf.Abs(yVal));
                map[x, y] = Evaluate(val);
                
            }
        }

        return map;
    }

    static float Evaluate(float value)
    {
        float a = 3f;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }

}