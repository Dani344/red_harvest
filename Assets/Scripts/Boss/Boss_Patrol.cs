using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Patrol : MonoBehaviour
{

    [SerializeField] private Vector3[] _points;
    private Boss_Combat _bc;

    private void Awake()
    {
        //Cogemos las referencias de los puntos para patrullar o ya las asignamos por el inspector.
        
    }
    
    private void Start()
    {
          //Activamos el estado patrol
    }

    private void Update()
    {
        transform.position += new Vector3(1, 0, 1);
    }

    public void ChangeStateToCombat()
    {
        Debug.Log("Cambiamos a combat desde patrol");
    }
}
