/*
Score.cs
by Deepti Ramani
01/31/2020
This class keeps track of score and game state (intro, win, lose, etc)
*/

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
    public GameObject CurrScoreText;
    public GameObject HighScoreText;

    //game over
    public GameObject GameOverText;
    public GameObject RestartButton;

    public GameObject Camera;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        CurrScoreText = Camera.transform.Find("ScoreText").gameObject;
        HighScoreText = Camera.transform.Find("HighScoreText").gameObject;
        GameOverText = Camera.transform.Find("GameOverText").gameObject;

        //set up UI
        CurrScoreText.GetComponent<TextMesh>().text = "HI 00000";
        HighScoreText.GetComponent<TextMesh>().text = "HI 00000";
        GameOverText.GetComponent<TextMesh>().text = "G A M E  O V E R";

    }

    // Update is called once per frame
    void Update()
    {
        //player jumps to start game
        if (Input.GetAxis("Jump") > 0 && !gameOver)
        {
            inGame = true;

        }
        //increment score while game is playing
        if (inGame)
        {
            //update score + print
            scoreTimer += Time.deltaTime;
            if (scoreTimer > 1.0f)
            {
                score++;
                scoreTimer = 0.0f;
                CurrScoreText.GetComponent<TextMesh>().text = "" + score;
            }
            //TODO: flicker on 100s
        }
    }

    //call this when the game ends to check high score and print game over message
    public void endGame()
    {
        inGame = false;
        gameOver = true;

        //stop moving and display end game ui
        Camera.GetComponent<CameraScroll>().currSpeed = 0.0f;
        GameOverText.SetActive(true);
        RestartButton.SetActive(true);

        //check high score
        if(score > highScore)
        {
            highScore = score;
            HighScoreText.GetComponent<TextMesh>().text = "HI " + highScore;
        }
    }

    public void Restart()
    {
        gameOver = false;

        //reset UI
        score = 0;
        CurrScoreText.GetComponent<TextMesh>().text = "00000";
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);

        //reset camera movement
        Camera.GetComponent<CameraScroll>().currSpeed = Camera.GetComponent<CameraScroll>().baseSpeed;
        Camera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }
}
