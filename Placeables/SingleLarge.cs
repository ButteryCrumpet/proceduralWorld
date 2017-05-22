using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rewrite completely use circle collidor!
[CreateAssetMenu()]
public class SingleLarge : WorldFeature
{
    float radius;

    public override List<Vector3> GetPositions(Vector3[] allVertices, GameObject targetObject)
    {
        List<Vector3> positions = new List<Vector3>();
        foreach(Vector3 vector in allVertices)
        {
            List<RaycastHit> hits = eightwayRay(vector + new Vector3(0, 0.25f, 0));

            bool areaClear = true;

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null)
                {
                    areaClear = false;
                    break;
                }
            }

            if (areaClear)
            {
                positions.Add(vector);
                Debug.Log("Found suitable");
            }
        }

        positions.Add(allVertices[Random.Range(0, allVertices.Length - 1)]);
        
        return positions;
    }

    //drop from height at corners and test length

    public List<RaycastHit> eightwayRay(Vector3 vector)
    {
        List<RaycastHit> hits = new List<RaycastHit>();
        for (int i = 0; i < 8; i++)
        {
            RaycastHit hit = new RaycastHit();
            switch (i)
            {
                case 0:
                    Physics.Raycast(vector, new Vector3(1, 0, 0), out hit, 5f);
                    break;
                case 1:
                    Physics.Raycast(vector, new Vector3(1, 0, 1), out hit, 5f);
                    break;
                case 2:
                    Physics.Raycast(vector, new Vector3(0, 0, 1), out hit, 5f);
                    break;
                case 3:
                    Physics.Raycast(vector, new Vector3(-1, 0, 1), out hit, 5f);
                    break;
                case 4:
                    Physics.Raycast(vector, new Vector3(1, 0, 1), out hit, 5f);
                    break;
                case 5:
                    Physics.Raycast(vector, new Vector3(-1, 0, 0), out hit, 5f);
                    break;
                case 6:
                    Physics.Raycast(vector, new Vector3(-1, 0, -1), out hit, 5f);
                    break;
                case 7:
                    Physics.Raycast(vector, new Vector3(0, 0, -1), out hit, 5f);
                    break;
                case 8:
                    Physics.Raycast(vector, new Vector3(1, 0, -1), out hit, 5f);
                    break;
            }

            hits.Add(hit);
        }

        return hits;
    }
}
