using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTriggers : MonoBehaviour
{
    public GameObject RoadParent;
    public GameObject RoadPrefab;
    public GameObject Road;
    private bool isTriggered = false;
    private float destroyTimer = 5f;
    private void Awake()
    {
        RoadParent = GameObject.Find("RoadParent");
        destroyTimer = 5f;
        //RoadPrefab = Resources.Load<GameObject>("Road");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && !isTriggered)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            spawnPosition = new Vector3(this.transform.position.x, RoadPrefab.transform.position.y + 17, 0);
            Instantiate(RoadPrefab, spawnPosition, Quaternion.identity, RoadParent.transform);
            isTriggered = true;

        }
    }
    private void Update()
    {
        if (isTriggered)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                Destroy(Road.gameObject);
            }
        }
    }

}
