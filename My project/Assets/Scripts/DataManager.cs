using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private void Awake()
    {
        CheckPoint();
        PlayerController();
        
        for (int i = 1; i < 10; i++)
        {
            PlayerPrefs.SetInt("checkPoint" + i,0);
        }
    }

    private static void CheckPoint()
    {
        for (int i = 1; i < 10; i++)
        {
            if (!PlayerPrefs.HasKey("checkPoint" + i))
            {
                PlayerPrefs.SetInt("checkPoint" + i,0);
            }
        }
        
        if (!PlayerPrefs.HasKey("checkPoint" + 0))
        {
            PlayerPrefs.SetInt("checkPoint" + 0,1);
        }
        
        if (!PlayerPrefs.HasKey("clickPoint"))
        {
            PlayerPrefs.SetInt("clickPoint",1);
        }
        
        if (!PlayerPrefs.HasKey("currentCheckPoint"))
        {
            PlayerPrefs.SetInt("currentCheckPoint",0);
        }
    }

    private static void PlayerController()
    {
        if (!PlayerPrefs.HasKey("can"))
        {
            PlayerPrefs.SetInt("can",100);
        }
        
        if (!PlayerPrefs.HasKey("bullet"))
        {
            PlayerPrefs.SetInt("bullet",0);
        }
    }
}
