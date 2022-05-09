using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Healer : Enemy
{
    private float _count;

    private void Awake()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<SpriteRenderer>();
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        NormalSetUp("DUMMY", 100, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 10, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        
        _hpBarTextNumber.text = _currentHp + " / " + _generalStats.Health;
        
        HpBarUpdate();

        _count = 0f;
        _isRange = false;
        _newPathCount = 0f;
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }
        
        CheckTargetInRange();
        _newPathCount += Time.deltaTime;
        
        if (_newPathCount > _timeForUpdatePath)
        {
            //Debug.Log("ACTUALIZAS");
            if (!_isRange)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_targetPos);
            }
            _newPathCount = 0f;
            //CheckTargetInRange();
        }

        if (_isRange)
        {
            _navMesh.isStopped = true;
            _count += Time.deltaTime;
            
            //TENER EN CUENTA QUE BASE ATTACK SPEED ESTA GESTIONANDO EL AREA de momento
            if (_count > _generalStats.BaseAttackSpeed)
            {
                //Instanciar
                
                _count = 0f;
            }
        }
        
        _healthBar.transform.forward = _camera.transform.forward;
        _targetPos = _playerTarget.transform.position;
    }

    private void HealEnemy()
    {
        
    }
}
