using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{

    class WaitArea
    {
        public List<GameObject> peopleWaiting = new List<GameObject>();
    }
    static WaitArea[] floors = new WaitArea[9];
    static public void StartWaiting(GameObject person, float floor)
    {
        WaitArea waitArea = floors[(int) floor + 4];
        waitArea.peopleWaiting.Add(person);
    }
    static public void StopWaiting(GameObject person, float floor)
    {
        WaitArea waitArea = floors[(int) floor + 4];
        waitArea.peopleWaiting.Remove(person);

    }

    static public float GetWaitPoint(GameObject person, float floor)
    {
        var offset = 0.25F;
        WaitArea waitArea = floors[(int) floor + 4];
        var index = waitArea.peopleWaiting.IndexOf(person);
        return -3.5F - offset * index;
    }
    static public float waitPoint = -3.5F;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            floors[i] = new WaitArea();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
