/*
PlayerController.cs
by Kaijie Zhou
Edited by Deepti Ramani
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

    public Animator myAnimator;

    //Variables for horizontal movement
    public float speedMultiplier = 1.001f;
    public float baseHorizontalSpeed = 0.02f;
    public float currHorizontalSpeed = 0.00f;
    public float maxHorizontalSpeed = 0.1f;
    public Vector3 newXPos;

    //Variables for vertical movement
    public float jumpSpeed = 0.0f;
    public float baseJumpSpeed = 8.0f;
    public float jumpMultiplier = 3.0f;

    //Timer for time key held to make jump height different
    public float jumpHeight = 0.0f;
    public float quickFallMultiplier = 2.0f;

    //Jump cooldown timer
    public float maxJumpCooldown = 1.0f;
    public float jumpCooldown;

    //player states
    public bool isJump = false;
    public bool isDown = false;
    public bool isDuck = false;
    public bool isDead = false;
    public bool isGrounded = true;

    //find ground for jump
    public float groundPosition;
    public string groundToFind = "Ground";

    //original positionto check if climax of jump
    Vector3 OriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.Find("GameControl");
        myAnimator = gameObject.GetComponentInChildren<Animator>();

        OriginalPosition = gameObject.transform.position;

        //set groundPosition to top of ground platform Y.
        groundPosition = GameObject.FindWithTag(groundToFind).transform.position.y;

        jumpCooldown = maxJumpCooldown;

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
        groundPosition = GameObject.FindWithTag(groundToFind).transform.position.y;

        //vertical movement uses up/down arrows and space bar as input
        isJump = Input.GetAxis("Jump") > 0;
        if (isJump)
        {
            jumpHeight = Input.GetAxis("Jump") * 0.75f;
        }
        isDown = Input.GetAxis("Jump") < 0;

        //cooldown after jump
        if (jumpCooldown > 0.0f)
        {
            jumpCooldown -= Time.fixedDeltaTime;
        }

        //player can jump if they are on the ground & finished with cooldown
        if (isGrounded && isJump && jumpCooldown <= 0.0f)
        {
            isGrounded = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpHeight * jumpSpeed, 0);
        }
        //at peak, start falling
        if (!isGrounded && gameObject.transform.position.y - OriginalPosition.y >= jumpHeight * jumpMultiplier)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpHeight * jumpSpeed * -1, 0);
        }
        //fall down twice as fast
        if (!isGrounded && isDown)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpHeight * jumpSpeed * quickFallMultiplier * -1, 0);
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
        }

        //if collided with ground, its running
        if (collider.gameObject.tag == "Ground") {
            isGrounded = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
        else if(isJump)
        {
            myAnimator.SetInteger("State", 4);
        }
        //then duck
        else if(isDuck)
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
}
