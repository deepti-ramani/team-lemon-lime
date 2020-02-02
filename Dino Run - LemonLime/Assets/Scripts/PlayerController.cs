using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //game control for end game
    public GameObject GameControl;

    //Variables for horizontal movement
    public float speedMultiplier = 1.001f;
    public float baseHorizontalSpeed = 0.01f;
    public float currHorizontalSpeed = 0.00f;
    public float maxHorizontalSpeed = 0.15f;
    public Vector3 newXPos;

    //Variables for vertical movement
    public float baseSpeed = 0f;

    bool isJump;
    bool isQuickFall;
    bool isGrounded;

    public float groundPosition;
    public float buffer;
    public string groundToFind = "";
    //Jump cooldown timer
    float JumpCooldown = 0.001f;
    //Timer for time key held to make jump height different
    float JumpHeightTimer = 0f;
    //original positionto check if climax of jump
    Vector3 OriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.Find("GameControl");

        OriginalPosition = gameObject.transform.position;
        //set groundPosition to top of ground platform Y.
        groundPosition = GameObject.Find(groundToFind).transform.position.y;

        //set base horizontal speed + pos
        currHorizontalSpeed = baseHorizontalSpeed;
        newXPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move horizontally at an increasing speed (should match camera)
        newXPos = transform.position;
        newXPos.x += currHorizontalSpeed;
        transform.position = newXPos;
        if (currHorizontalSpeed <= maxHorizontalSpeed)
        {
            currHorizontalSpeed *= speedMultiplier;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if it hits an obstacle, quit
        if(collider.gameObject.tag == "Obstacle")
        {
            GameControl.GetComponent<GameControl>().GameOver();
        }
    }

    private void FixedUpdate()
    {
        //Key Events: Up / Space and down arrow
        isJump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
        isQuickFall = Input.GetKey(KeyCode.DownArrow);
        //Is player on the ground?
        isGrounded = (gameObject.transform.position.y == groundPosition + buffer) || (gameObject.transform.position.y == groundPosition + buffer + 0.1);

        //Debug
        GameObject.Find("New Text").GetComponent<TextMesh>().text = "IsJump: " + isJump + "\n isQuickFall: " + isQuickFall + "\n isGrounded: " + isGrounded;

        //set buffer to distance between player position and groundToFind.
        buffer = gameObject.transform.position.y - GameObject.Find(groundToFind).transform.position.y;
        while (JumpCooldown > 0f)
        {
            JumpCooldown -= Time.fixedDeltaTime;
        }
        
        if (isGrounded && isJump)
        {
            //record amount of time key held down for then multiply temp variable of how high to jump
            while (JumpHeightTimer <= .5f)
            {
                JumpHeightTimer += Time.fixedDeltaTime;
            }
            //once JumpHeightTimer is past a minimum value, multiply JumpHeightTimer to amount for jump. 

            //Temp: Minimum 0.5f for timer, 8 for jump height  
          
            //Use Rigidbody velocity and vector3.
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, JumpHeightTimer * 6 * baseSpeed, 0);
            //once reached highest point of jump, start falling at accurate velocity.

            //Temp: same as with jumping.Climax of jump capped 1 second.
            if ((gameObject.transform.position.y - OriginalPosition.y) == (JumpHeightTimer * 6 * .7f))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, (JumpHeightTimer * 6) * baseSpeed * -1, 0);
            }
        }

        if (isQuickFall)
        {
            //Temp: fall down at twice the rate of normal.
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, (JumpHeightTimer * 6) * baseSpeed * -2, 0);
        }
        
    }
}
