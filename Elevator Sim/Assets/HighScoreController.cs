using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{

    public static int highscore = 0;

    public Text text;

    public static void UpdateHighscore(int score)
    {
        if (score > highscore)
        {
            highscore = score;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Highscore: "+ highscore;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
