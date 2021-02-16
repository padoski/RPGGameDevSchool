using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float Fire_Time;
    private Player player;
    private Animator Fire_Anim;


    private void Start()
    {
        Fire_Anim = GetComponent<Animator>();
        player = GetComponent<Player>();

        
        GameObject PlayerObject= GameObject.FindWithTag("Player");

        if (PlayerObject != null)
        {
            player = PlayerObject.GetComponent<Player>();

        }

        StartCoroutine(FireOn(Fire_Time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Player"){
            // W ciaglej pracy
        }
    }

    //Utworzenie ognia
    IEnumerator FireOn (float Time)
    {
        yield return new WaitForSeconds(Time);
        Fire_Anim.SetBool("FireOff", true);
        Destroy(this.gameObject,1f);
    }
}