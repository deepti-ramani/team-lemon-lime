﻿/*
Powerup.cs
by Deepti Ramani
02/05/2020

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: add more types
public enum PowerupType
{
    IncreaseJumpHeight
}

public class PowerUp : MonoBehaviour
{
    public PowerupType type = PowerupType.IncreaseJumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
