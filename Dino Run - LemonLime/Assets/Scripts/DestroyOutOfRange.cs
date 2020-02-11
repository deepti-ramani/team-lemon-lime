/*
DestroyOutOfRange.cs
by Deepti Ramani
01/31/2020
This class destroys objects if they gets out of range (like obstacles, ground, etc)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfRange : MonoBehaviour
{
    public GameObject Camera;
    public float distanceToDestroy = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        //generate if camera approaches edge of ground
        if (Camera.transform.position.x - transform.position.x > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
