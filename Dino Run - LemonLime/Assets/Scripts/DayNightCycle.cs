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
        
    }
}
