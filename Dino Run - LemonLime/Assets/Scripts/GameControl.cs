/*
GameControl.cs
by Deepti Ramani
01/31/2020
This class keeps track of score and game's current state (intro, win, lose, etc)
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public bool inGame = false;
    public bool gameOver = false;

    //score
    public int score = 0;
    public int highScore = 0;
    public float scoreTimer = 0.0f;
    public bool flicker = false;
    public GameObject ScoreText;
    public GameObject HighScoreText;

    //game over
    public GameObject GameOverText;
    public GameObject RestartButton;

    //game objects
    public GameObject Camera;
    public Vector3 initCameraPos = new Vector3(0, 0, -10);
    public GameObject Player;
    public Vector3 initPlayerPos = new Vector3(-7.5f, -1.25f, 0);

    // Awake is called before start
    void Awake()
    {
        Camera = GameObject.Find("Main Camera");
        ScoreText = Camera.transform.Find("ScoreText").gameObject;
        HighScoreText = Camera.transform.Find("HighScoreText").gameObject;
        GameOverText = Camera.transform.Find("GameOverText").gameObject;
        Player = GameObject.Find("Player");

        //set up UI
        ScoreText.GetComponent<TextMesh>().text = "00000";
        HighScoreText.GetComponent<TextMesh>().text = "HI 00000";
        GameOverText.GetComponent<TextMesh>().text = "G A M E  O V E R";
        //hide end game ui
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //player jumps to start game if not already in game & if game isn't ended
        if (Input.GetAxis("Jump") > 0 && !inGame && !gameOver)
        {
            inGame = true;
            Camera.GetComponent<CameraScroll>().currSpeed = Camera.GetComponent<CameraScroll>().baseSpeed;
            Player.GetComponent<PlayerController>().currHorizontalSpeed = Player.GetComponent<PlayerController>().baseHorizontalSpeed;
            Player.GetComponent<PlayerController>().currHorizontalSpeed = Player.GetComponent<PlayerController>().baseHorizontalSpeed;
            Player.GetComponent<PlayerController>().jumpSpeed = Player.GetComponent<PlayerController>().baseJumpSpeed;
            Player.GetComponent<PlayerController>().isGrounded = true;
        }
        //increment score while game is playing
        if (inGame)
        {
            //update score + print
            scoreTimer += Time.deltaTime;
            if (scoreTimer > 0.1f)
            {
                score++;
                scoreTimer = 0.0f;
                //update score when not flickering
                if (!flicker)
                {
                    ScoreText.GetComponent<TextMesh>().text = string.Format(String.Format("{0:00000}", score));
                }
            }
            //flicker on 100s
            if(score % 100 == 0 && score > 0)
            {
                flicker = true;
                StartCoroutine(FlickerText(ScoreText));
            }
        }

        //for debugging (auto quit/restart)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    //flickers text on/off
    IEnumerator FlickerText(GameObject Text)
    {
        yield return new WaitForSeconds(0.3f);
        Text.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Text.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Text.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Text.SetActive(true);
        flicker = false;
    }

    //call this when the game ends to check high score and print game over message
    public void GameOver()
    {
        inGame = false;
        gameOver = true;

        //stop moving and display end game ui
        Camera.GetComponent<CameraScroll>().currSpeed = 0.0f;
        Player.GetComponent<PlayerController>().currHorizontalSpeed = 0.0f;
        Player.GetComponent<PlayerController>().jumpSpeed = 0.0f;
        GameOverText.SetActive(true);
        RestartButton.SetActive(true);

        //check high score
        if(score > highScore)
        {
            highScore = score;
            HighScoreText.GetComponent<TextMesh>().text = "HI " + string.Format(String.Format("{0:00000}", highScore));
        }
    }

    //reset values for starting over
    public void Restart()
    {
        inGame = true;
        gameOver = false;

        //reset UI
        score = 0;
        ScoreText.GetComponent<TextMesh>().text = "00000";
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);

        //regenerate ground
        Camera.GetComponent<GroundGeneration>().InstantiateInitGround();

        //reset camera & player movement
        Camera.GetComponent<CameraScroll>().currSpeed = Camera.GetComponent<CameraScroll>().baseSpeed;
        Camera.transform.position = initCameraPos;
        Player.GetComponent<PlayerController>().currHorizontalSpeed = Player.GetComponent<PlayerController>().baseHorizontalSpeed;
        Player.GetComponent<PlayerController>().jumpSpeed = Player.GetComponent<PlayerController>().baseJumpSpeed;
        Player.transform.position = initPlayerPos;
    }
}
