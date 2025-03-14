using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        PlayerPrefs.SetInt("checkPoint" + gameObject.name, 1);

        Debug.Log("girdi");
        var value = int.Parse(gameObject.name);
        PlayerPrefs.SetInt("currentCheckPoint",value);

        var health = GameManager.Instance.health;
        PlayerPrefs.SetInt("can",health);

        Debug.Log("VAR:" + PlayerPrefs.GetInt("can"));
    }
}