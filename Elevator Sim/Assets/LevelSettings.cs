using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{

    public int numFloors = 9;

    public int level;
    public float floorHeight;
    public float rockBottom;
    public float rockTop;
    public static LevelSettings instance;
    public float squareSize;

    public float leftWall;
    public float rightWall;

    int FloorPositionToNumber(float floorPosition)
    {

        Debug.Log(Time.time +"calculatingFloorPosition "+ floorPosition);
        var res = (floorPosition - rockBottom) / floorHeight;
        Debug.Log(Time.time +"res "+ res);
        return (int) res;
    }
    class WaitArea
    {
        public List<GameObject> peopleWaiting = new List<GameObject>();
    }
    WaitArea[] floors;
    static public void StartWaiting(GameObject person, float floorPosition)
    {
        instance._StartWaiting(person, floorPosition);
    }

    public void _StartWaiting(GameObject person, float floorPosition)
    {

        var floor = FloorPositionToNumber(floorPosition);
        WaitArea waitArea = floors[floor];
        waitArea.peopleWaiting.Add(person);
    }


    static public void StopWaiting(GameObject person, float floor)
    {
        instance._StopWaiting(person, floor);
    }
    public void _StopWaiting(GameObject person, float floor)
    {
        WaitArea waitArea = floors[FloorPositionToNumber(floor)];
        waitArea.peopleWaiting.Remove(person);
    }

    static public float GetWaitPoint(GameObject person, float floor)
    {

        Debug.Log("instance?" + instance);
        Debug.Log("waitpount?" + instance.waitPoint);

        return instance._GetWaitPoint(person, floor);
    }

    public float _GetWaitPoint(GameObject person, float floor)
    {
        var offset = 0.25F;
        WaitArea waitArea = floors[FloorPositionToNumber(floor)];
        var index = waitArea.peopleWaiting.IndexOf(person);

        var res = waitPoint - offset * index;
        Debug.Log("waitPoint gotten:" + res);
        return res;
    }
    public float waitPoint = -3.5F;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        floors = new WaitArea[instance.numFloors];
        for (int i = 0; i < numFloors; i++)
        {
            floors[i] = new WaitArea();
        }




    }

    // Update is called once per frame
    void Update()
    {

    }
}
