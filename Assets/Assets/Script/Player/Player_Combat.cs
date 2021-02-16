using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    private Animator Combat_Anim;
    public Transform Point;
    public float Attack_Range = 0.5f;
    public LayerMask Enemy_Layers;
    public float AttackRate = 2f;
    float NextAttackTime = 0f;
    private Player playerScpript;
    ParticleManger PS_Aura;
    public GameObject PS_Slash;
    bool Player_Mov;

    void Start()
    {
        Combat_Anim = GetComponent<Animator>();
        playerScpript = GetComponent<Player>();
        PS_Aura = transform.GetChild(1).GetComponent<ParticleManger>();

        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject == null)
        {
           playerScpript = PlayerObject.GetComponent<Player>();
        }
    }

    void Update()
    {
        //Aktualizacja gizmo

        if (playerScpript.movimiento != Vector2.zero) Point.transform.position = new Vector3(playerScpript.transform.position.x + playerScpript.movimiento.x / 2,
                    playerScpript.transform.position.y + playerScpript.movimiento.y / 2, 0);



      //Specjalna konfiguracja ataku

    if (Input.GetKeyDown(KeyCode.LeftAlt)){
            Combat_Anim.SetTrigger("Charge");
            PS_Aura.ParticleStart();
            //Nie można sie poruszać podczas ataku
            playerScpript.P_Mov(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt)){
            Combat_Anim.SetTrigger("Attack");
            if (PS_Aura.IsLoaded()){
                float Angle = Mathf.Atan2(
                Combat_Anim.GetFloat("MovY"), Combat_Anim.GetFloat("MovX"))* Mathf.Rad2Deg;
            //Służy do konwersji wektorów na kąty

            GameObject SpecialShoot_Object = Instantiate(PS_Slash, Point.transform.position, Quaternion.AngleAxis(Angle, Vector3.forward));
           

            PowerShoot Slash = SpecialShoot_Object.GetComponent<PowerShoot>();
            Slash.PS_Mov.x = Combat_Anim.GetFloat("MovX");
            Slash.PS_Mov.y = Combat_Anim.GetFloat("MovY");
            }
            PS_Aura.ParticleStop();
            StartCoroutine(EnableMovementAfterAttack(0.5f));
        
        }
        if (Time.time >= NextAttackTime)

            if (Input.GetKeyDown(KeyCode.Space)){
               Attack();
               NextAttackTime = Time.time + 1f / AttackRate;
            }
}

    void Attack()
    {
        //Trafionemu przesylamy obiekt ktory sie z nim zderza
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(Point.position, Attack_Range, Enemy_Layers);
        Combat_Anim.SetTrigger("Attack");
       
        foreach(Collider2D enemy in HitEnemy)
        {
            if (enemy.tag == "Object")
            {
                enemy.GetComponent<DestructibleObject>().Destroy_Object();
              
            }
            if (enemy.tag == "GenericEnemy" || enemy.tag == "Boss")
            {
                enemy.GetComponent<HpManagement>().LostHp();
              
            }
        }
    }

    IEnumerator EnableMovementAfterAttack(float time){
        yield return new WaitForSeconds(time);
        playerScpript.P_Mov (false);
 
    }




    //wyswietlanie gizmo
    private void OnDrawGizmosSelected()
    {
        if (Point == null)
            return;
        Gizmos.DrawWireSphere(Point.position, Attack_Range);
    }
}
