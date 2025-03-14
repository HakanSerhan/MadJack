using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XLocker : MonoBehaviour
{
    private float startX;
    void Start()
    {
        startX = transform.position.x;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}
