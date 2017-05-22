using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RandomlyPlacedWF : WorldFeature {

    public override List<Vector3> GetPositions(Vector3[] allVertices, GameObject targetObject)
    {
        List<Vector3> positions = new List<Vector3>();

        //adds a position based on conditions and a probability to handle density
        for (int i = 0; i < allVertices.Length; i++)
        {
            if (allVertices[i].y > 0 && allVertices[i].y / mapData.heightMultiplier > foundAtLow && allVertices[i].y / mapData.heightMultiplier < foundAtHigh && Random.Range(0,10) < Density)
            {
                float offsetX = Random.Range(-5f, 5f);
                float offsetY = Random.Range(-5f, 5f);
                positions.Add(DropToTerrain((allVertices[i] * 10) + new Vector3(offsetX, 20, offsetY), targetObject));
            }
        }

        return positions;
    }
}
