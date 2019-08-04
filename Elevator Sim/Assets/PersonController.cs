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
    public float superSlowSpeed = 1;
    public float speed = 5;
    public float initalPosition = -2.5F;
    public float targetPosition = -2.5F;
    public float waitTime = 5;





    float GetProductiveLocation()
    {
        return Random.Range(LevelSettings.instance.leftWall, LevelSettings.instance.rightWall);
    }
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.color;
        targetPosition = GetProductiveLocation();
        SetNewTarget();
        if (targetFloor != currentFloor) {
            LevelSettings.StartWaiting(gameObject, currentFloor);
            targetPosition = LevelSettings.GetWaitPoint(gameObject, currentFloor);
        }
        MoveToTarget(targetPosition, fastSpeed);
        timeSinceArrived = 0;
    }


    void SetNewTarget() {
        targetFloor = FloorPosition(Mathf.Floor(Random.value * LevelSettings.instance.numFloors) + LevelSettings.instance.rockBottom);
    }



    public float timeSinceArrived = 0;
    public float direction = 0;
    public void arriveAtFloor(float floor, GameObject elevator) {

        if (targetFloor != currentFloor && floor == currentFloor && targetElevator == null && !onElevator) {
            targetElevator = elevator;
            Debug.Log("get on!");
            Debug.Log(elevator.transform.position);
            targetPosition = elevator.transform.position.x;
            MoveToTarget(targetPosition, fastSpeed);
            LevelSettings.StopWaiting(gameObject, currentFloor);


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
            LevelSettings.StartWaiting(gameObject, currentFloor);
            targetPosition = LevelSettings.GetWaitPoint(gameObject, currentFloor);
            MoveToTarget(targetPosition, slowSpeed);

        }
        else  {
            Debug.Log("getting on (person)");
            var success = (targetElevator.GetComponent("ElevatorMove") as ElevatorMove).GetOn(gameObject);
             Debug.Log("success ?" + success);
            if (success) {
                onElevator = true;
                LightUpTarget(new Vector2(targetElevator.transform.position.x, targetFloor));
            }
            else {
                targetElevator = null;
                targetPosition = LevelSettings.instance.waitPoint;
                MoveToTarget(targetPosition, slowSpeed);

            }


        }

    }

    public GameObject targetIndicatorPrefab;

    public GameObject targetIndicator;
    void LightUpTarget(Vector2 position)
    {
        targetIndicator = Instantiate(targetIndicatorPrefab, position,  Quaternion.identity);
        var indicatorSize = LevelSettings.instance.squareSize * 1.2F;
        targetIndicator.transform.localScale = new Vector3(indicatorSize, indicatorSize, indicatorSize);
    }
    void GetOff()
    {
        Destroy(targetIndicator);
        targetIndicator = null;
        onElevator = false;
        currentFloor = targetElevator.transform.position.y;
        transform.position = new Vector2(transform.position.x, currentFloor);
        (targetElevator.GetComponent("ElevatorMove") as ElevatorMove).GetOff(gameObject);
        targetElevator = null;
        targetPosition = GetProductiveLocation();
        MoveToTarget(targetPosition, fastSpeed);

    }

    public float timeSinceLastProduction = 0;
    public bool flashing = false;
    float flashTime = 0.2F;
    public Color originalColour;
    public Color flashColour = Color.white;
    public float timeToProduce = 1.5F;
    public float timeSinceFlashed = 0;

    void ContinueFlashing()
    {
        timeSinceFlashed += Time.deltaTime;
        if (timeSinceFlashed >= flashTime)
        {
            flashing = false;
            spriteRenderer.color = originalColour;
        }
    }

    void DoProductiveStuff()
    {
        timeSinceLastProduction += Time.deltaTime;
        if (timeSinceLastProduction >= timeToProduce)
        {
            timeSinceLastProduction = 0;
            ProductivityTextController.instance.UpdateProductivity(1);

            flashing = true;
            timeSinceFlashed = 0;
            spriteRenderer.color = flashColour;

        }
        timeSinceArrived += Time.deltaTime;
        if (timeSinceArrived > waitTime) {
            SetNewTarget();
            if (targetFloor != currentFloor) {
                LevelSettings.StartWaiting(gameObject, currentFloor);
                targetPosition = LevelSettings.GetWaitPoint(gameObject, currentFloor);
                MoveToTarget(targetPosition, fastSpeed);
            }
            timeSinceArrived = 0;
        }
    }

    void DoMovementStuff()
    {
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
    void Update()
    {

        if (flashing)
        {
            ContinueFlashing();
        }

        if (targetFloor == currentFloor) {
            DoProductiveStuff();
            if (direction == 0)
            {
                targetPosition = GetProductiveLocation();
                MoveToTarget(targetPosition, superSlowSpeed);
            }

        }

        if (onElevator) {

            ElevatorMove elevatorMoveScript =(targetElevator.GetComponent("ElevatorMove") as ElevatorMove);
            if (elevatorMoveScript.velocity == 0 && targetElevator.transform.position.y == targetFloor) {
                GetOff();
            }
        }

        if (direction != 0 && !onElevator) {
            DoMovementStuff();
        }



    }

    public float FloorPosition(float floorNumber)
    {
        return (floorNumber - LevelSettings.instance.rockBottom) * LevelSettings.instance.floorHeight + LevelSettings.instance.rockBottom;
    }
}
