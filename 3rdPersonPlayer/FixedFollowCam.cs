using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowCam : MonoBehaviour {

    public GameObject target;
    public float camDistance;
    public float camAngle;
    public float maxZoomDist;

    public float rotateDegrees;
    private Vector3 offset;

    //initialise
	void Start () {
        setOffset();
        transform.Rotate(new Vector3(camAngle , 0, 0));
	}
    
    //runs every frame
    void Update()
    {
        Zoom();
        Revolve();
    }
	
	void LateUpdate () {
        Vector3 nextPosition = target.transform.position + offset;
        transform.position = nextPosition;
        transform.Translate(Vector3.right * Time.deltaTime);
	}

    //calculates world position relative to target, keeping camera at set linear distance
    //along a circumference around the target
    void setOffset()
    {
        //calculates offset of Y axis and the 2D radius of the circle, should be in speerate method
        float radianAngle = DegreeToRadian(camAngle);
        float offsetY = Mathf.Sin(radianAngle) * camDistance;
        float flatDist = Mathf.Cos(radianAngle) * camDistance;

        //calculates position along the circumfrance
        float rotateRad = DegreeToRadian(rotateDegrees);
        float offsetX = flatDist * Mathf.Sin(rotateRad);
        float offsetZ = flatDist * Mathf.Cos(rotateRad);

        offset = new Vector3(offsetX, offsetY, -offsetZ);
    }

    //scales the linear distance from target from input
    void Zoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        if (zoom > 0f && camDistance > 20)
        {
            camDistance -= 1f;
            setOffset();
        }
        else if (zoom < 0f && camDistance < maxZoomDist)
        {
            camDistance += 1f;
            setOffset();
        }
    }

    //sets the angle from the target and faces camera to target
    void Revolve()
    {
        float revolve = Input.GetAxis("CamRotate");

        if (revolve > 0f)
        {
            if (rotateDegrees >= 360)
                rotateDegrees = 0;
            rotateDegrees += 1;
            transform.Rotate(Vector3.up, -1, Space.World);
            setOffset();
        }

        else if (revolve < 0f)
        {
            if (rotateDegrees <= 0)
                rotateDegrees = 360;
            rotateDegrees -= 1;
            transform.Rotate(Vector3.up, 1, Space.World);
            setOffset();
        }
    }

    float DegreeToRadian(float angle)
    {
        return (angle * Mathf.PI) / 180;
    }
}
