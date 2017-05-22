using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGen {

    public static Texture2D GenerateColourTexture(float[,] noiseMap, TerrainLevel[] terrainLevels)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int i = 0; i < terrainLevels.Length; i++)
                {
                    if (noiseMap[x, y] <= terrainLevels[i].height)
                    {
                        pixels[y * width + x] = terrainLevels[i].colour;
                        break;
                    }
                }
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
