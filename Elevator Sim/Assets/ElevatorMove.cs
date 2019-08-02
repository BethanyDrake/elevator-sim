using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


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
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) {
            movingUp = true;
            stopping = false;
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            movingUp = false;
        }

         if (Input.GetKeyDown(KeyCode.A)) {
            movingDown = true;
            stopping = false;
        }
        if (Input.GetKeyUp(KeyCode.A)) {
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
            }
        }

        if (velocity != 0) {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
        }

    }
}
