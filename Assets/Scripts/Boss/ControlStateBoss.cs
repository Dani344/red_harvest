using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateBoss : Enemy
{
    [SerializeField] private Boss_Patrol _bp;
    [SerializeField] private Boss_Combat _bc;

    [SerializeField] private Enemy[] states;
    [SerializeField] private Enemy _currentState;
    
    private Collider _col;
    
    
    
    private void Awake()
    {
        states = new Enemy[2];
        //states[0] = new Boss_Patrol();
        //states[1] = new Boss_Combat();

    }

    private void Start()
    {

        states[0] = _bp;
        states[1] = _bc;

        _currentState = states[0];

        _currentState.Init();

        //RefreshInfo();
        
        
    }

    private void Update()
    {
        TestControl();
        _currentState.Movement();

        //Si detecta enemigo cambio a combate
    


    }
    

    private void RefreshInfo()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = _currentState;
        }
    }

    private void ChangeState(int newState)
    {
        RefreshInfo();
        _currentState = states[newState];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PaperConstants.TAG_PLAYER))
        {
            //COMBAT
        }
    }
    
    
    private void TestControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentState == states[1]){
                _currentState = _bp;
            }else{
                _currentState = _bc;
            }
            
        }
    }

    //TEST OVERRIDE
    public override void Init(){
        Debug.Log("INIT BOSS");
        //base.Init();
    }

}
