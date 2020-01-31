using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float speedMultiplier = 1.001f;
    public float baseSpeed = 0.01f;
    public float currSpeed = 0.01f;
    public float maxSpeed = 0.15f;

    public Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = baseSpeed;
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //update position
        newPos = transform.position;
        newPos.x += currSpeed;
        newPos.z = -10;
        transform.position = newPos;
        if(currSpeed <= maxSpeed)
        {
            currSpeed *= speedMultiplier;
        }
    }
}
