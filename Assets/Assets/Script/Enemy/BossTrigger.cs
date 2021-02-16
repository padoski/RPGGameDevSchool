using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    private Player PlayerScrpit;
    private Boss EnemyBoss;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScrpit = GetComponent<Player>();
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null) PlayerScrpit = PlayerObject.GetComponent<Player>();


        EnemyBoss = GetComponent<Boss>();
        GameObject BossObject = GameObject.FindGameObjectWithTag("Boss");
        if (BossObject != null) EnemyBoss = BossObject.GetComponent<Boss>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerScrpit.StartCoroutine(PlayerScrpit.FreezePlayer(3.5f));
            //Wejscie do jaskini motyw zobaczenia bossa
            EnemyBoss.Broken_Egg = true;
            Destroy(this.gameObject);
        }
}
}
