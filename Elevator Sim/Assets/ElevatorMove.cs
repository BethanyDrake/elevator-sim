﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorMove : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject person;

    void Start()
    {

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
    public List<PersonController> people = new List<PersonController>();

    public void AddPerson(GameObject person) {
       people.Add(person.GetComponent("PersonController") as PersonController);
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
                targetFloor = Mathf.Ceil(transform.position.y);}

            else {
                actualDirectionIsUp = false;
                targetFloor = Mathf.Floor(transform.position.y);

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
                foreach (PersonController person in people) {
                    person.arriveAtFloor(targetFloor, gameObject);
                }
            }
        }

        if (velocity != 0) {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
        }

    }
}
