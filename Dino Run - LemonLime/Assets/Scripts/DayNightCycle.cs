using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DayNightCycle : MonoBehaviour
{
    int score = GameObject.Find("GameControl").GetComponent<GameControl>().GetScore();
    GameObject groundPrefab = PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(GameObject.FindWithTag("Ground")));
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
