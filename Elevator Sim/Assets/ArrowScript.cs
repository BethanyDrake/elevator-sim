using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public float secondsPerRotation;
    public float moveSpeed = 0.8F;
    public Vector3 startingPosition;
    public GameObject clockFace;
    public bool moving = true;
    public bool flashing = false;
    public float flashRate = 0.5F;
    // Start is called before the first frame update
    public float timeSinceLastFlash = 0;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StopAndFlash()
    {
        moving = false;
        flashing = true;

    }

    public Color[] colours = new Color[2];

    public int currColour = 0;
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            var rotationPerSecond = -360 / secondsPerRotation;
            var angle = rotationPerSecond * Time.deltaTime;
            transform.RotateAround(clockFace.transform.position, new Vector3(0, 0, 1), angle);
        }
        if (flashing)
        {
            timeSinceLastFlash += Time.deltaTime;
            if (timeSinceLastFlash >= flashRate)
            {
                currColour = (currColour + 1) % 2;
                spriteRenderer.color = colours[currColour];
                timeSinceLastFlash = 0;
            }

        }

    }
}
