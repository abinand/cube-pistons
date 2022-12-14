using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwayMovement : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float xBoundMax = 5.0f;
    [SerializeField] private float xBoundMin = 0.0f;
    [SerializeField] private float zBoundMax = 5.0f;
    [SerializeField] private float zBoundMin = 0.0f;
    [SerializeField] private CubeMovement cubeMovement;

    private bool isMovingAway;
    private float currentSpeed;
    private ArmRotation pistonArmRotation;

    private void Start()
    {
        // all pistons are moving away intitially
        isMovingAway = true;
        currentSpeed = speed;
        cubeMovement.shrinkStarted.AddListener(ChangeDirection);
        cubeMovement.shrinkCompleted.AddListener(ChangeDirection);
        pistonArmRotation = gameObject.GetComponentInChildren<ArmRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        // move pistons
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

        // check for boundaries
        if (transform.position.x > xBoundMax)
        {
            transform.position = new Vector3(xBoundMax, transform.position.y, transform.position.z);
        }
        if (transform.position.x < xBoundMin)
        {
            transform.position = new Vector3(xBoundMin, transform.position.y, transform.position.z);
        }
        if (transform.position.z < zBoundMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundMin);
        }
        if (transform.position.z > zBoundMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundMax);
        }
    }

    void ChangeDirection()
    {
        direction = -direction;
        isMovingAway = !isMovingAway;

        // speed up when moving towards the cube
        if (!isMovingAway)
        {
            currentSpeed *= 2;
            pistonArmRotation.MoveFaster();
        }
        else
        {
            currentSpeed = speed;
            pistonArmRotation.MoveSlower();
        }
    }
}
