using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeMechanic : MonoBehaviour
{
    //Początkowy czas, w którym wywołujemy obiekt i jak często będzie się on powtarzał.

    public GameObject I_Object;
    public float First_Invoke;
    public float Second_Invoke;

    // Start is called before the first frame update
    void Start()
    {  //Zmienna, która generuje losową wartość między pierwszym a drugim czasem wywołania.
        float Rng = Random.Range(First_Invoke, Second_Invoke);
        InvokeRepeating("InvokeObject", First_Invoke, Rng);
    }

   
    void InvokeObject()
    {
        Instantiate(I_Object, transform.position, transform.rotation);
    }
}
