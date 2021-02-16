using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigometricMove : MonoBehaviour
{
    //Estadisticas.
  
    public float Frequencia;
    public float Amplitud;

    Vector3 StartPosition,CurrentPosition;
    private Rigidbody2D rb2d;
    private Animator Anim;

    [Tooltip("Maxymalna odl dodatnia na osi X")]
    public float Limit_PosX;
    [Tooltip("Maxymalna odl ujemna na osi X")]
    public float LimitNeg_PosX;
   
    [Tooltip("Kierunek w ktorym zaczynasz patrzec")]
    public int Direction;
    public enum MovementObject {Circle,Sin,Cos};

    [Header("Wybierz ruch")]
     public MovementObject MovementType;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        CurrentPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        float DistanceTotal = CurrentPosition.x - StartPosition.x;

        if (DistanceTotal > Limit_PosX)Direction = -1;
            else if (DistanceTotal < LimitNeg_PosX) Direction = 1;

        //Dzięki temu Switchowi trajektoria obiektu zmieni się zgodnie z tym, co wcześniej wybraliśmy w inspektorze.


        switch (MovementType){
            case MovementObject.Circle:
                              PerfectCircle();
                break;

                case MovementObject.Sin:
                                Sin_Tracking();
                 break;

                case MovementObject.Cos:
                                Cos_Tracking();
                 break; 
            }
    }
   
        //Ruch okręzny
       public void PerfectCircle() {

        float x = Mathf.Cos(Time.time * Frequencia) * Amplitud;
        float y = Mathf.Sin(Time.time * Frequencia) * Amplitud;
        float z = rb2d.transform.position.z;
       
        rb2d.transform.position = new Vector3((StartPosition.x + x), (StartPosition.y + y), StartPosition.z);
    }

    //Sinus

    void Sin_Tracking(){

        transform.position = CurrentPosition + transform.up * Mathf.Sin(Time.time * Frequencia) * Amplitud;
        CurrentPosition += transform.right * Time.deltaTime * Frequencia * Direction;
    }


    //Cosinus
    void Cos_Tracking() {

        transform.position = CurrentPosition + transform.up * Mathf.Cos(Time.time * Frequencia) * Amplitud;
        CurrentPosition += transform.right * Time.deltaTime * Frequencia * Direction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Limit_PosX);
        Gizmos.DrawWireSphere(transform.position, LimitNeg_PosX);
    }
}


