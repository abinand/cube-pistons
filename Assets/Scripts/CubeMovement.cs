using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float startingHeight = 20.0f;
    [SerializeField] private float endingHeight = -10.0f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float shrinkSpeed = 9f;

    private bool easeIn = false;
    private bool canShrink = false;
    private float timeElapsed = 0f;
    private float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        ResetCube();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shrink();
        if (transform.position.y < endingHeight)
        {
            ResetCube();
        }
    }

    private void Shrink()
    {
        if (canShrink)
        {
            float horizontalShrinkage = shrinkSpeed * Time.deltaTime;
            if (transform.localScale.x > 2)
            {
                transform.localScale -= new Vector3(horizontalShrinkage, 0, horizontalShrinkage);
            }
            else
            {
                canShrink = false;
                currentSpeed = speed;
            }
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);
        if (easeIn)
        {
            // reduce speed every 0.5 seconds
            timeElapsed += Time.deltaTime;
            if (timeElapsed > 0.5f)
            {
                timeElapsed -= 0.5f;
                currentSpeed = currentSpeed * 0.5f;
            }
            transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

            // stop moving when in position and allow shrinking
            if (transform.position.y < 0.01f)
            {
                currentSpeed = 0f;
                canShrink = true;
                easeIn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        easeIn = true;
    }

    void ResetCube()
    {
        transform.localScale = new Vector3(10, 2, 10);
        transform.position = new Vector3(0, startingHeight, 0);
    }
}
