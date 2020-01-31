using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float baseSpeed = 0f;

    //multiplier of speed based on score
    float speedMultiplier = 0f;

    bool isJump;
    bool isFall;

    //Timer for time key held to make jump height different
    float JumpHeightTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Key Events: Up / Space and down arrow
        isJump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
        isFall = Input.GetKey(KeyCode.DownArrow);
            
    }

    private void FixedUpdate()
    {
        if (isJump) {
            //record amount of time key held down for then multiply temp variable of how high to jump
            
            //Use Rigidbody velocity and vector3.

        }

    }
}
