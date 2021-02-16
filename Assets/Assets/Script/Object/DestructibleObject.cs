using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public string Destroy_State;
    private Animator Object_Anim;

    // Start is called before the first frame update
    void Start()
    {
        Object_Anim = GetComponent<Animator>();
        
    }

    public void Destroy_Object()
    {
        // Breakable
        Object_Anim.Play(Destroy_State);
    }
   

    private void Update()
    {
        //Kiedy czas animacji dobiega konca, obiekt zostanie zniszczony
        AnimatorStateInfo Info = Object_Anim.GetCurrentAnimatorStateInfo(0);
        if (Info.IsName(Destroy_State)&& Info.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}