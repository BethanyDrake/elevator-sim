﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void BackToMainMenu()
    {
        HighScoreController.UpdateHighscore(ProductivityTextController.instance.productivity, LevelSettings.instance.level);
        SceneManager.LoadScene("StartScreen");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
