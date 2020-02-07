/*
PlayerController.cs
by Kaijie Zhou and Deepti Ramani
01/30/2020
This class controls the player's input + movement options (jump & duck)
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //game control for end game
    public GameObject GameControl;
    public RandomContainer randomC;

    //animator for switching animations
    public Animator myAnimator;

    //Variables for horizontal movement
    public float speedMultiplier = 0.001f;
    public float baseHorizontalSpeed = 0.05f;
    public float currHorizontalSpeed = 0.0f;
    public float maxHorizontalSpeed = 0.11f;
    public Vector3 newXPos;

    //Variables for vertical movement
    public float jumpSpeed = 0.0f;
    public float baseJumpSpeed = 10.0f;
    public float jumpMultiplier = 3.0f;

    //height to jump
    public float baseJumpHeight = 1.0f;
    public float maxJumpHeight = 1.0f;
    public float currJumpHeight = 0.0f;
    public float quickFallMultiplier = 2.0f;

    //Jump cooldown timer
    public float maxJumpCooldown = 1.0f;
    public float currJumpCooldown = 0.0f;

    //powerup timer
    public float maxPowerupTime = 5.0f;
    public float currPowerupTime = 0.0f;

    //player states
    public bool isJump = false;
    public bool isDown = false;
    public bool isDuck = false;
    public bool isDead = false;
    public bool isGrounded = true;
    public bool isPoweredUp = false;

    //find ground for jump
    public float groundPosition;
    public string groundToFind = "Ground";

    //original positionto check if climax of jump
    public Vector3 OriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.Find("GameControl");
        myAnimator = gameObject.GetComponentInChildren<Animator>();
        randomC = GameObject.FindObjectOfType(typeof(RandomContainer)) as RandomContainer;

        OriginalPosition = gameObject.transform.position;

        //set groundPosition to top of ground platform Y.
        groundPosition = GameObject.FindWithTag(groundToFind).transform.position.y;

        currJumpCooldown = maxJumpCooldown;

        //set pos
        newXPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move horizontally at an increasing speed (should match camera)
        newXPos = transform.position;
        newXPos.x += currHorizontalSpeed;
        transform.position = newXPos;
        if (currHorizontalSpeed != 0 && currHorizontalSpeed <= maxHorizontalSpeed)
        {
            //TODO: += or *=
            currHorizontalSpeed += speedMultiplier * Time.deltaTime;
        }
        groundPosition = GameObject.FindWithTag(groundToFind).transform.position.y;

        //vertical movement uses up/down arrows and space bar as input
        isJump = Input.GetAxis("Jump") > 0;
        if (isJump)
        {
            currJumpHeight = Input.GetAxis("Jump") * maxJumpHeight;
        }
        isDown = Input.GetAxis("Jump") < 0;

        //cooldown after jump
        if (currJumpCooldown > 0.0f)
        {
            currJumpCooldown -= Time.fixedDeltaTime;
        }

        //player can jump if they are on the ground & finished with cooldown
        if (isGrounded && isJump && currJumpCooldown <= 0.0f)
        {
            isGrounded = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, currJumpHeight * jumpSpeed, 0);
        }
        //at peak, start falling
        if (!isGrounded && gameObject.transform.position.y - OriginalPosition.y >= currJumpHeight * jumpMultiplier)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, currJumpHeight * jumpSpeed * -1, 0);
        }
        //fall down twice as fast
        if (!isGrounded && isDown)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, currJumpHeight * jumpSpeed * quickFallMultiplier * -1, 0);
        }

        //player can duck if they are on the ground and press down arrow
        if (isGrounded && isDown)
        {
            //TODO: duck
            isDuck = true;
            Debug.Log("Duck");
        }
        else
        {
            isDuck = false;
        }

        //powerup timer
        if (isPoweredUp)
        {
            currPowerupTime += Time.deltaTime;
            if (currPowerupTime >= maxPowerupTime)
            {
                currPowerupTime = 0.0f;
                ResetPowerups();
            }
        }

        SelectAnimation();
    }

    //collisions with obstacles and ground
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if it hits an obstacle, quit
        if (collider.gameObject.tag == "Obstacle")
        {
            isDead = true;
            GameControl.GetComponent<GameControl>().GameOver();
            transform.position = new Vector3(transform.position.x, OriginalPosition.y, transform.position.z);
        }

        //if collided with ground, its running
        if (collider.gameObject.tag == "Ground") {
            isGrounded = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        //if collided with a powerup, change appropriate stat and destroy powerup
        if (collider.gameObject.tag == "Powerup")
        {
            isPoweredUp = true;
            PowerupType type = collider.gameObject.GetComponent<PowerUp>().type;
            Destroy(collider.gameObject);

            //TODO: add more types
            switch (type)
            {
                case PowerupType.IncreaseJumpHeight:
                    maxJumpHeight = 1.5f;
                    break;
            }
        }
    }

    //switch between animations
    public void SelectAnimation()
    {
        //dead has highest priority
        if (isDead)
        {
            myAnimator.SetInteger("State", 3);
        }
        //then jump
        else if (isJump)
        {
            randomC.PlaySound();
            myAnimator.SetInteger("State", 4);
        }
        //then duck
        else if (isDuck)
        {
            //go down for duck and change collider
            transform.position = new Vector3(transform.position.x, OriginalPosition.y - 0.3f, transform.position.y);
            myAnimator.SetInteger("State", 2);
        }
        //then normal running has the lowest priority
        else
        {
            if (isGrounded)
            {
                transform.position = new Vector3(transform.position.x, OriginalPosition.y, transform.position.y);
            }
            myAnimator.SetInteger("State", 1);
        }
    }

    //reset for next try
    public void ResetValues()
    {
        isJump = false;
        isDown = false;
        isDuck = false;
        isDead = false;
        isGrounded = true;

        transform.position = OriginalPosition;
        jumpSpeed = baseJumpSpeed;
        currHorizontalSpeed = baseHorizontalSpeed;
    }

    //reset all changes caused by powerups
    private void ResetPowerups()
    {
        maxJumpHeight = baseJumpHeight;
    }
}
