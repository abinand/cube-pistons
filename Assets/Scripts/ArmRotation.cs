using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * speed * Time.deltaTime, Space.Self);
    }

    public void MoveFaster()
    {
        // move fast and reverse directions
        speed = -speed * 2;
    }

    public void MoveSlower()
    {
        // move slow and reverse directions
        speed = -speed / 2;
    }
}
