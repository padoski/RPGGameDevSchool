﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Next_Map = "InnerHouse";

    
    public void LevelOne()
    {
        SceneManager.LoadScene(Next_Map);
    }
}
