using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DayNightCycle : MonoBehaviour
{
    int score = GameObject.Find("GameControl").GetComponent<GameControl>().score;
    GameObject groundPrefab = PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(GameObject.FindWithTag("Ground")));
    GameObject obstaclePrefab = PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(GameObject.FindWithTag("Obstacle")));
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get latest score value
        score = GameObject.Find("GameControl").GetComponent<GameControl>().score;
        //Check if we are at a multiple of 700
        if (score % 7 == 0 && score % 100 == 0) {
            //Begin invert colors within 1 second.

            //(Not Yet) Plus: Colors from Pink and Yellow to Blue and Green

        }
    }
}
