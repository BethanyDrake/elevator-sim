using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public float revolutionsPerMinute = 1;
    public float moveSpeed = 0.8F;
    public Vector3 startingPosition;
    public GameObject clockFace;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var rotationPerSecond = -360 * revolutionsPerMinute / 60;
        var angle = rotationPerSecond * Time.deltaTime;
        transform.RotateAround(clockFace.transform.position, new Vector3(0, 0, 1), angle);
        Debug.Log(transform.rotation.eulerAngles);
    }
}
