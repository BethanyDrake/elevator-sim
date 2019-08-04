using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void StartLevel2()
    {

    }
    public void StartLevel3()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) {
        //     SceneManager.LoadScene("MainScene");
        // }
    }
}
