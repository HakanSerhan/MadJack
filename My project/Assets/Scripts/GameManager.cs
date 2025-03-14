using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text healthText;
    public int health;
    public GameObject retryPanel;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = PlayerPrefs.GetInt("can");
        PlayerController.Instance.playerHealth = health;
        healthText.text = "Health : " + health;

        Debug.Log("health:" + PlayerPrefs.GetInt("can"));
    }

    public void RetryButton()
    {
        PlayerPrefs.SetInt("clickPoint", PlayerPrefs.GetInt("currentCheckPoint"));
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
