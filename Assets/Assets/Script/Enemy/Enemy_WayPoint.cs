using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WayPoint : MonoBehaviour
{
    // Tworzymy tablicę do zapisu różych punktów
    [Tooltip("Enter the different positions")]
    public GameObject[] wayPoints;
    int CurrentPosition = 0;

   
    public float speed =0.6f;

    Animator Anim;
    Rigidbody2D rb2d;


    void Start(){

        Anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update(){

      //system way point
                  //Na początku tworzymy zmienną gdzie zapisujemy kierunek gdzie enemy "się patrzy" (aby dodać to do animacji)
        Vector3 Direction = (wayPoints[CurrentPosition].transform.position - transform.position).normalized;
        Anim.SetFloat("MovX", Direction.x);
        Anim.SetFloat("MovY", Direction.y);
                     //Nasępnie tworzymy zmienną dystans która informuje nas o dystansie pomiedzy nami i jednym z tych punktów.
       float Distance = Vector3.Distance(wayPoints[CurrentPosition].transform.position, transform.position);
                     // movement settings
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[CurrentPosition].transform.position, Time.deltaTime * speed);
        if (Distance <= 0)
        {//ten if informuje czy stworzyc jeszcze darmową lokaliacje w randomowym miejscuw  stosunku do nas
            CurrentPosition = Random.Range(0, wayPoints.Length);
        }
        
    }


    //Zmieniamy naszą pozycję wtedy way point dotyka playera
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Sprawdza czy nie wyjdziemy poza tablice
            if (CurrentPosition != 0) --CurrentPosition;
            else
                CurrentPosition++;
        }
    }

}
