using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorMove : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject person;

    void Start()
    {
        floorHeight = LevelSettings.instance.floorHeight;
    }

    public KeyCode upKey;
    public KeyCode downKey;

    public bool movingUp = false;
    public bool movingDown = false;
    public bool stationary = true;
    public float velocity = 0F;
    public float topSpeed = 10F;
    public float acceleration = 1F;
    public bool actualDirectionIsUp = false;
    public bool stopping = false;
    public float targetFloor;
    public bool tooFast = false;
    public bool calculatedTargetFloor;
    public int maxCapacity = 4;
    public float floorHeight;
    public List<PersonController> people = new List<PersonController>();


    public List<GameObject> peopleOnBoard = new List<GameObject>();
    public void AddPerson(GameObject person) {
       people.Add(person.GetComponent("PersonController") as PersonController);
    }

    public bool GetOn(GameObject person){
        if (peopleOnBoard.Count < maxCapacity) {
            peopleOnBoard.Add(person);
            return true;
        }
        return false;

    }

    public void GetOff(GameObject person) {
        peopleOnBoard.Remove(person);
    }


    Vector3 GetPositionOffset(int index)
    {
        float offset = .2F;
        switch (index)
        {
            case 0:
                return new Vector3(-offset, -offset, 0);
            case 1:
                return new Vector3(offset, -offset, 0);
            case 2:
                return new Vector3(-offset, offset, 0);
            case 3:
                return new Vector3(offset, offset, 0);
            case 4:
                return new Vector3(0, 0, 0);
        }
        return new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(upKey)) {
            movingUp = true;
            stopping = false;
        }
        if (Input.GetKeyUp(upKey)) {
            movingUp = false;
        }

         if (Input.GetKeyDown(downKey)) {
            movingDown = true;
            stopping = false;
        }
        if (Input.GetKeyUp(downKey)) {
            movingDown = false;
        }

        if (movingUp && velocity < topSpeed) {
            velocity += acceleration * Time.deltaTime;
        }
        if (movingDown && velocity > -topSpeed) {
            velocity -= acceleration * Time.deltaTime;
        }


        //if the player has stopped moving, decalerrate to half max speed and continue until the next level

        if (!movingDown && !movingUp && !stopping) {
            stopping = true;
            tooFast = true;
            calculatedTargetFloor = false;
        }
        if (stopping && tooFast) {
            if (velocity > topSpeed/2) velocity -= acceleration * Time.deltaTime;
            else if (velocity < -topSpeed/2) velocity += acceleration * Time.deltaTime;
            else {
                tooFast = false;
            }
        }
        if (stopping && !tooFast && !calculatedTargetFloor) {
            if (velocity > 0)  {
                actualDirectionIsUp = true;
                targetFloor = Mathf.Ceil(transform.position.y/floorHeight) *floorHeight;
                Debug.Log("calculated target floor!" + targetFloor);
            }
            else {
                actualDirectionIsUp = false;
                targetFloor = Mathf.Floor(transform.position.y/floorHeight)*floorHeight;
                Debug.Log("calculated target floor!" + targetFloor);

            }
            calculatedTargetFloor = true;
        }

        if (calculatedTargetFloor && stopping) {

            if ( (actualDirectionIsUp && transform.position.y > targetFloor) ||
             (!actualDirectionIsUp && transform.position.y < targetFloor)
            ) {
                velocity = 0;
                transform.position = new Vector2(transform.position.x, targetFloor);
                stopping = false;
                foreach (GameObject person in peopleOnBoard) {
                    PersonController personController = person.GetComponent("PersonController") as PersonController;
                    personController.arriveAtFloor(targetFloor, gameObject);
                }



                foreach (PersonController person in people)
                {
                    person.arriveAtFloor(targetFloor, gameObject);
                }


            }
        }

        if (velocity != 0) {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
        }


        int i = 0;
        foreach (GameObject person in peopleOnBoard)
        {
            person.transform.position = (transform.position + GetPositionOffset(i));
            i++;
        }


    }
}
