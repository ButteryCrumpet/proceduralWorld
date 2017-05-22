using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//grouped feature, like forests, flower beds etc
[CreateAssetMenu()]
public class GroupedWorldFeature : WorldFeature {

    public int groupSizeMin;
    public int groupSizeMax;

    //dislike this method after switiching to 3rd person needs complete overhaul
    //need more natural shape (not square) and natural scaling outside smaller inside larger // height conditions should be derived from line equation
    //rather than positions list, generate from a list of structs with prefab, position and scale values?
    public override List<Vector3> GetPositions(Vector3[] allVertices, GameObject targetObject)
    {
        //gets all mesh vectors that meet height conditions
        List<Vector3> availableVectors = (from vector in allVertices where vector.y / mapData.heightMultiplier > foundAtLow && vector.y / mapData.heightMultiplier < foundAtHigh select vector).ToList();
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < Density; i++)
        {
            //gets central vector
            Vector3 position = availableVectors[Random.Range(0, availableVectors.Count - 1)] * 10;
            int groupingSizeX = Random.Range(groupSizeMin, groupSizeMax)*10;
            int groupingSizeY = Random.Range(groupSizeMin, groupSizeMax)*10;
            //for grouping size X by goruping size Y gets a position to put a tree
            for (int x = -groupingSizeX; x < groupingSizeX; x++)
            {
                for (int y = -groupingSizeY; y < groupingSizeY; y++)
                {
                    float offsetX = (x + Random.Range(-5f, 5f));
                    float offsetZ = (y + Random.Range(-5f, 5f));
                    positions.Add(DropToTerrain((position) + new Vector3(offsetX, 200, offsetZ), targetObject));
                }
            }
        }

        return positions;
    }
}
