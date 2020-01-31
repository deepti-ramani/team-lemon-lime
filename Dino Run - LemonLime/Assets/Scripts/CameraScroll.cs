using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float speedMultiplier = 1.1f;
    public float baseSpeed = 2.0f;
    public float currSpeed = 2.0f;

    public Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = baseSpeed;
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        newPos.x += currSpeed * Time.deltaTime;
        transform.position = newPos;
        currSpeed *= speedMultiplier * Time.deltaTime;
    }
}
