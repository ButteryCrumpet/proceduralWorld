using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//rewrite -unused-
public static class FlattenAreas {

	public static float[,] Flatten(float[,] noiseMap, FlatArea[] flatAreas)
    {
        foreach (FlatArea flatArea in flatAreas)
        {
            List<Coord> coords = GetPossibleCoords(flatArea, noiseMap);

            Coord randomCoord = coords[Random.Range(0, coords.Count)];

            for (int x = -flatArea.length / 2; x < flatArea.length / 2; x++)
            {
                for (int y = -flatArea.width / 2; y < flatArea.width / 2; y++)
                {
                    noiseMap[randomCoord.x + x, randomCoord.y + y] = noiseMap[randomCoord.x, randomCoord.y];
                }
            }
        }
        return noiseMap;
    }

    private static List<Coord> GetPossibleCoords(FlatArea flatArea, float[,] noiseMap)
    {
        List<Coord> coords = new List<Coord>();

        for (int x = 0; x < noiseMap.GetLength(0); x++)
        {
            for (int y = 0; y < noiseMap.GetLength(1); y++)
            {
                if (noiseMap[x, y] > flatArea.minElevation && noiseMap[x, y] < flatArea.maxElevation)
                {
                    Coord coord = new Coord();
                    coord.x = x;
                    coord.y = y;
                    coords.Add(coord);
                }
            }
        }

        return coords;
    }
}

public struct Coord
{
    public int x;
    public int y;
}