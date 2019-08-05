using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    // Start is called before the first frame update

    public static PeopleController instance;

    public GameObject personPrefab;
    public float time = 0;
    public float hireRate = 5;
    public int maxPeople = 10;
    public int numPeople = 0;
    public List<GameObject> people = new List<GameObject>();
    public ElevatorMove[] elevators = new ElevatorMove[10];
    public float patienceThreshold;

    public Color[] angerColors = new Color[4];
    public Vector3 spawnPoint;

    public void SendEveryoneHome()
    {
        foreach(GameObject person in people)
        {
            Destroy(person);
        }
    }
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (numPeople < maxPeople && numPeople < time / hireRate) {
            var newPerson = Instantiate(personPrefab, spawnPoint, Quaternion.identity);
            people.Add(newPerson);
            numPeople++;

              foreach (ElevatorMove elevator in elevators) {
                if (elevator) {
                elevator.AddPerson(newPerson);
                }
            }
        }

        //add a new person at regular intervals?

    }
}
