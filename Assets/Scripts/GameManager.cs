using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Ball ball;
    public Paddle paddle;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static string p1tag = "p1";
    public static string p2tag = "p2";

    int scoreLimit = 3;

    public int score1 = 0;
    public int score2 = 0;

    public Text score1Text;
    public Text score2Text;
    public Text winnerText;

    // Start is called before the first frame update
    void Start()
    {   
        // Singleton instance
        instance = this;

        bottomLeft = Camera.main.ScreenToWorldPoint (new Vector2 (0, 0));
        topRight = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));

        // Create ball
        Instantiate (ball);

        scoreLimit = 3;

        // Create two paddles
        Paddle paddle1 = Instantiate (paddle) as Paddle;
        Paddle paddle2 = Instantiate (paddle) as Paddle;

        winnerText.GetComponent<Text>().enabled = false;

        score1Text = score1Text.GetComponentInChildren<Text>();

        score1Text.GetComponent<Text>().color = Color.red;
        score2Text.GetComponent<Text>().color = Color.blue;

        paddle1.Init(false); // right paddle
        paddle2.Init(true); // left paddle

        resetGame();
    }

    public void addScorePlayer(string player) {
        if (player == "p1") {
            score1 += 1;
            score1Text.text = score1.ToString();
        }
        else if (player == "p2") {
            score2 += 1;
            score2Text.text = score2.ToString();
        }

        if (score1 >= scoreLimit | score2 >= scoreLimit) {
            if (score1 >= scoreLimit) {
                winnerText.text = "Player 1 wins !";
                winnerText.GetComponent<Text>().color = Color.red;
            }
            else {
                winnerText.text = "Player 2 wins !";
                winnerText.GetComponent<Text>().color = Color.blue;
            }

            winnerText.GetComponent<Text>().enabled = true;
            resetGame(); 
        }
    }

    public void resetGame() {
        score1 = 0;
        score1Text.text = score1.ToString();
        score2 = 0;
        score2Text.text = score2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
