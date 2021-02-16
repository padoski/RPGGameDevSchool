using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int HP_Rage;
    private Rigidbody2D rb2d;
    private Animator Anim;
    public float Speed;
    public float AtkSpeed;
    public float Stun;
    public bool Broken_Egg = false;
    public bool Summon = true;


    //Inicjowanie obiektów
    public GameObject StunParticle;
    public GameObject Egg;
    public GameObject IndleParticle;
    public GameObject Rocks;

    private bool GetSt;
    private bool IsAttackig;

    private HpManagement Hp_Boss;

    //Pozycje poczatkowe
    Vector3 StartPosition;
    public float Attack_Radius;
    Transform Player;


    //Dodanie punktów-grafy
    public GameObject[] wayPoints;
    int CurrentPosition = 0;

    //Różne rodzaje state'ów
    public enum State{ Idle,Atk1,Stun,Rage};
    public State St_Found;


    // Start is called before the first frame update
    void Start(){

        Anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Hp_Boss = GetComponent<HpManagement>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp_Boss.Max_HP <= HP_Rage)
        {
                StartCoroutine(Rageintro(true));
            
        }
      

        switch (St_Found)
            {
                case State.Idle:
                if (Broken_Egg == true) Anim.SetBool("Broken", Broken_Egg);
                               break;

                case State.Atk1:
                          Running();
                                break;

                case State.Stun:
                             break;

                case State.Rage:
                      Rage();
                            break;
            }
        }

    //Wywoływana z animacji BrokenEgg
    void Idle (){
        //
        if (Summon == true) {
            Instantiate(IndleParticle, transform.position, transform.rotation);
            Instantiate(Rocks, new Vector3(-26, 0, 0), transform.rotation);
            Summon = false;
        }
        //Po kilku sekundach przechodzi w stan ataku
        StartCoroutine(Attk_1(3f));
    }

    //Stan ataku
    IEnumerator Attk_1(float time)
    {
        //Cambiamos el estado del enemigo a ATK1.
        yield return new WaitForSeconds(time);
        St_Found = State.Atk1;
 
    }

    // Metoda w której boss na nas biegnie używa waypointów
    public void Running ()
    {
        Anim.SetBool("Run", true);

        if (St_Found == State.Atk1 )
        {
            //system way pointów
        StartCoroutine(GetStun(Stun));
        Vector3 Direction = (wayPoints[CurrentPosition].transform.position - transform.position).normalized;
        Anim.SetFloat("MovX", Direction.x);
        Anim.SetFloat("MovY", Direction.y);

        float Distance = Vector3.Distance(wayPoints[CurrentPosition].transform.position, transform.position);
        
            // konfiguracja poruszania sie
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[CurrentPosition].transform.position, Time.deltaTime * Speed);
        if (Distance <= 0)
            {//Jeśli osiągnie punkt odniesienia, przywoła jajko i będzie się poruszać losowo.

                InvokeEgg(true);
            CurrentPosition = Random.Range(0, wayPoints.Length);
        }
        }

    }

    //Wywyoływanie jajka
    void InvokeEgg(bool Test)
    {
        if (Test == true)
        {
            Instantiate(Egg, transform.position, transform.rotation);
            Test = false;
        }

    }

    //Stun
    IEnumerator GetStun(float time)
    {
        GetSt = true;

        yield return new WaitForSeconds(time);
        St_Found = State.Stun;
        StartCoroutine(Stunned());
    }

    //paraliż
   IEnumerator  Stunned ()
    {
        if (GetSt == true && St_Found != State.Rage)
        {
            StopCoroutine("GetStun");
            rb2d.velocity = Vector3.zero;
            Anim.SetTrigger("Summon");

            GameObject CloneParticle = Instantiate(StunParticle, transform.position, Quaternion.identity) as GameObject;
            GetSt = false;
            yield return new WaitForSeconds(Stun);
            Anim.SetBool("Run",true);
            Destroy(CloneParticle);
            St_Found = State.Atk1;
           

        }
    }
    // Wscieklosc
    public void Rage()
    {


        Speed = 2.5f;
        Vector3 target = new Vector3(Player.position.x,Player.position.y);
        Vector3 newPos = Vector3.MoveTowards(rb2d.position, target, Speed * Time.fixedDeltaTime);
        rb2d.MovePosition(newPos);

        float Distance = Vector3.Distance(target, transform.position);
        Vector3 Dir = (target - transform.position).normalized;

        Anim.SetFloat("MovX", Dir.x);
        Anim.SetFloat("MovY", Dir.y);

        if (Vector2.Distance(Player.position,rb2d.position)<= Attack_Radius)
        {
            if (!IsAttackig) StartCoroutine(Attack(AtkSpeed));

        }
    }

    //Metoda, której użyje w trybie wściekłości, aby wiedzieć, jak często musi wzywać miecz.
    IEnumerator Attack(float speed)
    {
        IsAttackig = true;

        Instantiate(Egg, transform.position, transform.rotation);

        yield return new WaitForSecondsRealtime(speed);

        IsAttackig = false;
    }

    IEnumerator Rageintro (bool Go)
    {
        StopCoroutine("GetStun");
        StopCoroutine("Stunned");
        if (Summon == false)
        { Anim.SetBool("Rage", Go);
            Summon = true;
        }

        yield return new WaitForSeconds(0.8f);

        Summon = true;
        Anim.SetBool("Rage", false);
        St_Found = State.Rage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Attack_Radius);
    }
   
}
