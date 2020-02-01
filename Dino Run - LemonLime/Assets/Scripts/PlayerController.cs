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
    bool isQuickFall;
    bool isGrounded;

    public float groundPosition = 0;
    //Jump cooldown timer
    float JumpCooldown = 0.1f;
    //Timer for time key held to make jump height different
    float JumpHeightTimer = 0f;
    //original positionto check if climax of jump
    Vector3 OriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = gameObject.transform.position;
        //set groundPosition to top of ground platform Y.

    }

    // Update is called once per frame
    void Update()
    {
        //Key Events: Up / Space and down arrow
        isJump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
        isQuickFall = Input.GetKey(KeyCode.DownArrow);

    }

    private void FixedUpdate()
    {
        JumpCooldown -= Time.fixedDeltaTime;
        if (isJump && isGrounded && JumpCooldown == 0f)
        {
            //record amount of time key held down for then multiply temp variable of how high to jump
            JumpHeightTimer += Time.fixedDeltaTime;
            //Is player on the ground?
            isGrounded = transform.position.y == groundPosition;
            //once JumpHeightTimer is past a minimum value, multiply JumpHeightTimer to amount for jump. 

            //Temp: Minimum 0.5f for timer, 5 for jump height  
            if (JumpHeightTimer >= .5f)
            {
                //Use Rigidbody velocity and vector3.
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, JumpHeightTimer * 5 * baseSpeed, 0);
            }
            //once reached highest point of jump, start falling at accurate velocity.

            //Temp: same as with jumping.Climax of jump capped 1 second.
            if ((gameObject.transform.position.y - OriginalPosition.y) == (JumpHeightTimer * 5 * 1f))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, (JumpHeightTimer * 5) * baseSpeed * -1, 0);
            }
        }

        if (isQuickFall)
        {
            //Temp: fall down at twice the rate of normal.
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, (JumpHeightTimer * 5) * baseSpeed * -2, 0);
        }
        
    }
}
