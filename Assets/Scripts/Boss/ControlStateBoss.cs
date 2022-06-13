using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateBoss : MonoBehaviour
{
    [SerializeField] private Boss_Patrol _bp;
    [SerializeField] private Boss_Combat _bc;

    [SerializeField] private Enemy[] states;
    [SerializeField] private Enemy _currentState;

    private bool _playerAlive = true;
    private bool _activated = false; 
    
    private Collider _col;
    private GameManager _gm;
    private void Awake()
    {
        states = new Enemy[2];
        _gm = FindObjectOfType<GameManager>(); 
        //states[0] = new Boss_Patrol();
        //states[1] = new Boss_Combat();

    }

    private void Start()
    {
        states[0] = _bp;
        states[1] = _bc;

        _currentState = states[0];
        _currentState.Init();

        _playerAlive = true;
        //RefreshInfo();
    }

    private void Update()
    {
        
        if (_playerAlive)
        {
            UpdateCurrentState(_currentState);
        }
        else
        {
            Debug.Log("PLAYER STATE ALVIE");
            //SI BOSS ALIVE O PLAYER ALIVE
            _gm.FinishGame();
        }
        
        
        
    }

    private void UpdateCurrentState(Enemy _miestado)
    {
        //Movement
        
        //if player esta vivo o boss vivo
        if (_miestado.isEnemyActive() && !_activated)
        {
            ChangeState();
            _miestado.Movement();  
            
        }
         _miestado.Movement();
        

        

        //if (_miestado.GetTargetGO())
        //{
            //Atacar
        //}

        /*
         if (_ficha.!isAlive)
         {
            //_miestado = states[2];
            //SetCurrentState(DieState);
         }
         */
    }

    public void PlayerDied()
    {
        _playerAlive = false;
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
            
        }
    }
    
    
    private void ChangeState()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            _activated = true;
            if (_currentState == states[1]){
                _currentState = _bp;
            }else
            {
                //var hasTarget = _currentState.isEnemyActive();
                var target = _currentState.GetTargetGO();
                
                _currentState.SetTargetGO(target);
                
                _currentState = _bc;
                
            }
            
        //}
    }

    
    }

//TEST OVERRIDE
//public override void Init(){
//Debug.Log("INIT BOSS");
        
//base.Init();
    
    /*
    public void InitBoss()
    {
        NormalSetUp("DUMMY", 100, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 15, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        
        _barManagement.InitializeBar(_generalStats.Health, PaperConstants.HP_BAR_NEUTRAL);
        //ShowCharacterInformation();
        
    }

}*/
