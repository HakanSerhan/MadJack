using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsFollow : MonoBehaviour
{
    public Transform target;
    public float MoveSpeed = 0.12f;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(0.0360932f, /*target.position.y+ 0.4869742f*/transform.position.y+ MoveSpeed, transform.position.z);
    }
}
