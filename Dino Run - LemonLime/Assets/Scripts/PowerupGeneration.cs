using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGeneration : MonoBehaviour
{
    public GameObject[] PowerupList = new GameObject[Enum.GetNames(typeof(PowerupType)).Length];
    public GameObject ObjectToInstantiate;
    public float firstPos = 40.0f;
    public float minDist = 15.0f;
    public float maxDist = 25.0f;
    public float targetPos;
    public Vector3 currPosToGenerate = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //first one is hardcoded
        targetPos = firstPos;
    }

    // Update is called once per frame
    void Update()
    {
        //generate if we have moved far enough to instantiate
        if (targetPos - transform.position.x <= 0.0001)
        {
            //instantiate
            ObjectToInstantiate = PowerupList[UnityEngine.Random.Range(0, PowerupList.Length)];
            currPosToGenerate.x = transform.position.x + 10.0f;
            currPosToGenerate.y = ObjectToInstantiate.transform.position.y;
            Instantiate(ObjectToInstantiate, currPosToGenerate, transform.rotation);

            //choose next position to instantiate
            targetPos = transform.position.x + UnityEngine.Random.Range(minDist, maxDist);
        }
    }
}
