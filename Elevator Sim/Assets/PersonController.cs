using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    // Start is called before the first frame update

    public float currentFloor = -4;
    public float targetFloor = 2;

    public GameObject targetElevator = null;
    public bool onElevator = false;
    public float fastSpeed = 5;
    public float slowSpeed = 2;
    public float speed = 5;
    public float initalPosition = -2.5F;
    public float targetPosition = -2.5F;
    void Start()
    {
        targetPosition = initalPosition;
        MoveToTarget(targetPosition, fastSpeed);

    }



    public float direction = 0;
    public void arriveAtFloor(float floor, GameObject elevator) {

        if (targetFloor != currentFloor && floor == currentFloor && targetElevator == null && !onElevator) {
            targetElevator = elevator;
            Debug.Log("get on!");
            Debug.Log(elevator.transform.position);
            targetPosition = elevator.transform.position.x;
            MoveToTarget(targetPosition, fastSpeed);


        }
    }
    // Update is called once per frame

    void MoveToTarget(float targetPosition, float newSpeed) {
        speed = newSpeed;
        if (targetPosition - transform.position.x < 0) {
                direction = -1;
            }
            else if (targetPosition - transform.position.x > 0) {
                direction = 1;
            }
            else direction = 0;
    }
    void GetOn() {
        if (targetElevator.transform.position.y != currentFloor) {
            targetElevator = null;
            targetPosition = -4;
            MoveToTarget(targetPosition, slowSpeed);

        }
        else onElevator = true;

    }
    void GetOff() {
        onElevator = false;
        currentFloor = targetElevator.transform.position.y;
        targetElevator = null;
        targetPosition = -6;
        direction = -1;

    }
    void Update()
    {

        if (onElevator) {
            this.transform.position = new Vector2(transform.position.x, targetElevator.transform.position.y);
            ElevatorMove elevatorMoveScript =(targetElevator.GetComponent("ElevatorMove") as ElevatorMove);
            if (elevatorMoveScript.velocity == 0 && targetElevator.transform.position.y == targetFloor) {
                GetOff();
            }
        }

        if (direction != 0 && !onElevator) {

            if (direction > 0 && targetPosition - transform.position.x < 0) {
                direction = 0;
                transform.position = new Vector2(targetPosition, transform.position.y);
                if (targetElevator) {
                    GetOn();
                }

            }
            else if (direction < 0 && targetPosition - transform.position.x > 0) {
                direction = 0;
                transform.position = new Vector2(targetPosition, transform.position.y);
                if (targetElevator) {
                    GetOn();
                }
            } else

            transform.position = new Vector2(transform.position.x + direction * Time.deltaTime * speed, transform.position.y);
        }




    }
}
