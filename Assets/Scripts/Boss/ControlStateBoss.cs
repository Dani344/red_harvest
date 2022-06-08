﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateBoss : MonoBehaviour
{
    [SerializeField] private Boss_Patrol _bp;
    [SerializeField] private Boss_Combat _bc;

    [SerializeField] private Enemy[] states;
    [SerializeField] private Enemy _currentState;
    
    private Collider _col;
    
    
    
    private void Awake()
    {
        states = new Enemy[2];
        states[0] = new Boss_Patrol();
        states[1] = new Boss_Combat();
    }

    private void Start()
    {
        _currentState = states[0];
        
    }

    private void Update()
    {
        
        TestControl();

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
            //Combat();
            //_indexState = 1;
        }
    }
}
