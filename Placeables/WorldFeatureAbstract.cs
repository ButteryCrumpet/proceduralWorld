using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//should turn all random.range into seeded random

//abstract class for world feattures (maybe need better name) rocks, grass, flowers, trees/forests, towns
//all placeable models
public abstract class WorldFeature : ScriptableObject{

    //in Unity undefined public variables are defined in the editor
    public string Name;
    public GameObject[] Prefabs;

    [Range(0,1)]
    public float foundAtLow;
    [Range(0, 1)]
    public float foundAtHigh;
    [Range(0, 10)]
    public int Density;
    [Range(0, 1)]
    public float scaleVariance;
    [Range(0,1)]
    public float rotationVariance;
    [Range(0, 1)]
    public float ensureConnectionLevel;

    public bool hasPriority;
    public MapData mapData;

    private GameObject parent;
    private List<GameObject> instatiatedObjects;

    public abstract List<Vector3> GetPositions(Vector3[] allVertices, GameObject targetObject);

    //places objects from Prefabs[] into the world, random scale/rotation
    public void Place(Vector3[] allVertices, GameObject targetObject)
    {
        List<Vector3> positions = GetPositions(allVertices, targetObject);
        parent = new GameObject(name);

        foreach (Vector3 position in positions)
        {
            if (position != new Vector3(0, 0, 0))
            {
                float randomScale = Random.Range(0, scaleVariance);
                Quaternion rotation = new Quaternion(Random.Range(0, rotationVariance * 90), Random.Range(0, rotationVariance * 90), Random.Range(0, rotationVariance * 90), Random.Range(0, rotationVariance * 90));
                instatiatedObjects.Add(Instantiate(Prefabs[Random.Range(0, Prefabs.Length - 1)], position, rotation, parent.transform));
                instatiatedObjects[instatiatedObjects.Count - 1].transform.localScale += new Vector3(randomScale, randomScale, randomScale);
            }
        }
    }

    //destroys instatiated objects -broken- (I think)
    public void DestroyAll()
    {
        if (instatiatedObjects != null)
        {
            foreach (GameObject instdObject in instatiatedObjects)
            {
                DestroyImmediate(instdObject);
            }
        }
    }
    
    //as objects can be grouped relative to each other, makes sure objects connect to the ground
    public Vector3 DropToTerrain(Vector3 position, GameObject targetObject)
    {
        RaycastHit hit;
        Physics.Raycast(position, Vector3.down, out hit); //searches downwards for meshes and returns information of mesh hit

        if (hit.transform == targetObject.transform) //if hits target mesh returns new coord
            position = position - new Vector3(0, hit.distance + ensureConnectionLevel, 0);
        else 
            position = new Vector3(0, 0, -200); //quick fix need better option // this can be done in ternery format anyway //!!write it like TryParse etc with an out!!

        return position;
    }

}
