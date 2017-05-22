using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlaceObjects
{
    public static void PlaceAll(GameObject gameObject, WorldFeature[] worldFeatures)
    {

        DestroyObjects(worldFeatures);

        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        foreach (WorldFeature worldFeature in worldFeatures)
        {
            worldFeature.Place(mesh.vertices, gameObject);
        }
    }

    public static void DestroyObjects(WorldFeature[] worldFeatures)
    {
        foreach (WorldFeature worldFeature in worldFeatures)
        {
            worldFeature.DestroyAll();
        }
    }
}
