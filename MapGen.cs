using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    public MapData mapData;
    public WorldFeature[] worldFeatures;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    public FlatArea[] flatAreas;

    private void Start()
    {
        GenerateMap();
        PlaceObjects.PlaceAll(gameObject, worldFeatures);
    }

    public void GenerateMap()
    {
        //necessary componenets to render a mesh
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();

        //noise map
        float[,] noiseMap = NoiseMap.GenerateTerrainNoise(mapData.width, mapData.height, mapData.seed, mapData.scale, mapData.octaves, mapData.persistance, mapData.lacunarity, mapData.offset, mapData.heightCurve);
        float[,] islandNoise = IslandNoiseMap.ParseToIslandNoise(noiseMap);
        islandNoise = FlattenAreas.Flatten(islandNoise, flatAreas);

        //makes the height level textures
        Texture2D texture = TextureGen.GenerateColourTexture(islandNoise, mapData.terrainLevels);
        //makes the mesh
        MeshData meshData = MeshGen.GenerateTerrain(islandNoise, mapData.heightMultiplier);
        //renders the mesh
        DrawMesh(meshData, texture);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.mesh;
        meshCollider.sharedMesh = meshData.mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}

//level for different texture colour
[System.Serializable]
public struct TerrainLevel
{
    public string name;
    public float height;
    public Color colour;
}
//unused
[System.Serializable]
public struct FlatArea
{
    public int width;
    public int length;
    [Range(0,1)]
    public float minElevation;
    [Range(0, 1)]
    public float maxElevation;
}
