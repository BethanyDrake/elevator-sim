using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject personPrefab;
    public float time = 0;
    public float hireRate = 5;
    public int maxPeople = 10;
    public int numPeople = 0;
    public List<GameObject> people = new List<GameObject>();
    public ElevatorMove[] elevators = new ElevatorMove[10];
    public Vector3 spawnPoint;
    void Start()
    {

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
