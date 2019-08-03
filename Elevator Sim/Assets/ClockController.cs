using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    // Start is called before the first frame update

    public float secondsInLevel;
    public float timeSinceLevelStarted = 0;
    public PeopleController peopleController;
    public ArrowScript arrow;

    public bool timeUp = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelStarted += Time.deltaTime;
        if (!timeUp && timeSinceLevelStarted >= secondsInLevel)
        {
            timeUp = true;
            peopleController.SendEveryoneHome();
            arrow.StopAndFlash();
        }

    }
}
