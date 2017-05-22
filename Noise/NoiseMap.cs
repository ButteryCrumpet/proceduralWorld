using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Perlin noise generator
//mostly derived, needs personal rewrite
public static class NoiseMap {

	public static float[,] GenerateTerrainNoise(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, AnimationCurve heightCurve)
    {
        float[,] noiseMap = new float[width, height];
        
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        //generates octaves
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
            scale = 0.0001f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        //generates map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {

                    float sampleX = (x - (width / 2f)) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - (height / 2f)) / scale * frequency + octaveOffsets[i].y;

                    float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //Debug.Log(perlin);
                    noiseHeight += perlin * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                
                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;
                
                noiseMap[x, y] = noiseHeight;
            }
        }

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = heightCurve.Evaluate(Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]));
            }
        }

        return noiseMap;

    }
}