using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Combat : Enemy
{
    private Boss_Patrol _bs;

    private void Awake()
    {
        
    }
    private void Start()
    {
        _navMesh.speed = 4f;
    }
    

    public void Combat()
    {
        Debug.Log("PIU");
    }

    
    
    
}
