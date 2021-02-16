using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObject : MonoBehaviour
{
    
    public int Value;

    public GameObject Particle;
    
    public float Time;

    private Player PlayerScript;


    private void Start()
    {
        PlayerScript = GetComponent<Player>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) PlayerScript = playerObject.GetComponent<Player>();
        StartCoroutine(DestroyThis(Time));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            PlayerScript.Health(Value);
            Instantiate(Particle, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }
    IEnumerator DestroyThis (float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}