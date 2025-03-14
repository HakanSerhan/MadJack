using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed;

    private void Update()
    {
        var position = player.position;
        transform.position = Vector3.Slerp(transform.position, new Vector3(position.x, position.y, position.z - 10), cameraSpeed);
    }
}