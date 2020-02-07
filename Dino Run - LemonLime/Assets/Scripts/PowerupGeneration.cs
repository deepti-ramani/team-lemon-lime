using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] PowerupList = new GameObject[Enum.GetNames(typeof(PowerupType)).Length];
    public float minTimeUntilPowerup = 10.0f;
    public float maxTimeUntilPowerup = 20.0f;
    public float currTimeUntilPowerup = 10.0f;
    public Vector3 posToGenerate;

    // Start is called before the first frame update
    void Start()
    {
        posToGenerate = transform.position;
        currTimeUntilPowerup = maxTimeUntilPowerup;
    }

    // Update is called once per frame
    void Update()
    {
        currTimeUntilPowerup -= Time.deltaTime;

        if(currTimeUntilPowerup <= 0)
        {
            //Instantiate(PowerupList[UnityEngine.Random.Range(0, PowerupList.Length)], , transform.rotation);
        }
    }
}
