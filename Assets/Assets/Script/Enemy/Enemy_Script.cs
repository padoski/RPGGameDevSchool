using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    //Podstawowe zmienne enemy
    public float Vision_Radius;
    public float Attack_Radius;
    public float AtkSpeed;
    public float speed;
    bool IsAttackig;
    public float KoPower;
    public float koTime;

    
    public GameObject Projectile;

    GameObject Player;
    Animator Tree_Anim;
    Rigidbody2D rb2d;

    
    Vector3 StartPosition,target;
 


    void Start()
    {
        

        Player = GameObject.FindGameObjectWithTag("Player");
        
        StartPosition = transform.position;

        Tree_Anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 Target = StartPosition;

        //sprawdzamy dystans pomiedzy enemy i player
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, Player.transform.position - transform.position,
            Vision_Radius, 1 << LayerMask.NameToLayer("Player"));

       

        Vector3 Forward = transform.TransformDirection(Player.transform.position - transform.position);
        Debug.DrawRay(transform.position, Forward, Color.red);

        
        if (hit.collider != null)
        {if (hit.collider.tag == "Player")
            {
                Target = Player.transform.position;
                
            }
        }

        // Oblicza dystans pomiędzy tagetem a obecna lokalizacją
        float Distance = Vector3.Distance(Target, transform.position);
        Vector3 Dir = (Target - transform.position).normalized;
 

        //sprawdzenie czy player jest w attack radius 
        if (Target != StartPosition && Distance < Attack_Radius)
        {
            Tree_Anim.SetFloat("TreeX", Dir.x);
            Tree_Anim.SetFloat("TreeY", Dir.y);
            Tree_Anim.Play("Walk",-1,0); // Zamraza animiacje
            if (!IsAttackig) StartCoroutine(Attack(AtkSpeed));
        }
        else {
              rb2d.MovePosition(rb2d.transform.position + Dir * (speed * Time.deltaTime));
           
            // ustawia animacje chodzenia
            Tree_Anim.speed = 1;
            Tree_Anim.SetFloat("TreeX", Dir.x);
            Tree_Anim.SetFloat("TreeY", Dir.y);
            Tree_Anim.SetBool("Walking", true);
        }
        

        if (Target == StartPosition && Distance < 0.05f)
        {
            transform.position = StartPosition;
            Tree_Anim.SetBool("Walking", false);
        }

        Debug.DrawLine(transform.position, Target, Color.green);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Vision_Radius);
        Gizmos.DrawWireSphere(transform.position, Attack_Radius);
    }
    
 
    IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            enemy.velocity = Vector2.zero;
            yield return new WaitForSeconds(koTime);

            enemy.velocity = Vector2.zero;
        }
    }
    
    // Enumerator który wie jak czesto enemy musi zaatakowac
    IEnumerator Attack(float speed)
    {
        IsAttackig = true;

        Instantiate(Projectile, transform.position, transform.rotation);

        yield return new WaitForSecondsRealtime(speed);

        IsAttackig = false;
    }
}
