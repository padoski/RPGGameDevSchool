using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManagement : MonoBehaviour
{
    public int Max_HP;
    public GameObject Target;
    public Material Hit_Material;
    private Material Default_Material;
    public GameObject HitParticle;
    public GameObject DeathParticle;
    public GameObject Drop;
    private SpriteRenderer Sr;
    public float Time_Material;

   
 

    void Start()
    {
        GameObject TargetObject = GameObject.FindGameObjectWithTag("GenericEnemy");
        if (TargetObject!= null)
        {
            Target = TargetObject.GetComponent<GameObject>();
        }
        Sr = GetComponent<SpriteRenderer>();
        Default_Material = Sr.material;
    }

    public void LostHp()
    {
        --Max_HP;

        
        if (Hit_Material != null)
        {
            Sr.material = Hit_Material;
            StartCoroutine(RestoreDefaultMaterial(Time_Material));
        }

        //Tworzymy instancje jesli nie null
        if (HitParticle!= null)
        { Instantiate(HitParticle, transform.position, transform.rotation);
        }

        //Kiedy ginie
        if (Max_HP <= 0)
        {
            Instantiate(DeathParticle, transform.position, transform.rotation);
            if (Drop != null) Instantiate(Drop, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }

    IEnumerator RestoreDefaultMaterial(float Time)
    {
        yield return new  WaitForSeconds(Time);
        Sr.material = Default_Material;
    }

}

