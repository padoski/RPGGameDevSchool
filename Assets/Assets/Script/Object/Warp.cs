﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{

    //Ref do nastepnej mapy
    public string Next_Map;
    public CanvasManagement Canv;
    public float Delay_Time = 1.5f;


    private void Awake()
    {
        GameObject CanvasObject = GameObject.FindWithTag("Canvas"); 
        if (CanvasObject == null)
        {
            Canv = CanvasObject.GetComponent<CanvasManagement>();
        }
    }


    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Canv.PlayerOut();
            Canv.CurrentMap = Next_Map;
            yield return new WaitForSeconds(Delay_Time);

            SceneManager.LoadScene(Next_Map);

        }
    }


}
