using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField]
    private int lives;

    [SerializeField]
    private int score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text endLabel;

    // PUBLIC PROPERTIES
    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
            livesLabel.text = "Lives: " + lives.ToString();
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            scoreLabel.text = "Score: " + score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Lives = 5;
        Score = 0;
    }
}
