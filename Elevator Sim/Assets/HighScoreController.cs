using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{

    public static int[] highscores = {0, 0, 0};

    public Text[] texts = new Text[3];

    public static void UpdateHighscore(int score, int level)
    {
        var highscore = highscores[level];
        if (score > highscore)
        {
            highscores[level] = score;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) {
            var text = texts[i];
            var score = highscores[i];
            text.text = "Highscore: "+ score;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
