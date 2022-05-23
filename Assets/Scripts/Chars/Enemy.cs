using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    
    //FALTA QUE LOS ENEMIGOS SEPAN QUE EL JUGADOR O TARGET MUERE PARA DEJAR
    //DE DISPARAR!"!!!!
    [SerializeField] protected NavMeshAgent _navMesh;
    [SerializeField] protected bool _isActive;
    [SerializeField] protected Vector3 _targetPos;
    [SerializeField] protected bool _isRange;

    [SerializeField] protected GameObject _playerTarget;
    [SerializeField] protected PlayerMovement _playerScript;

    [SerializeField] protected Vector3 _spawnPoint;

    [SerializeField] protected bool _isReturning = false;


    protected float _newPathCount;
    protected float _timeForUpdatePath = 2f;
    
    //CUIDADO POR QUE SOLO CONTEMPLAMOS AL PLAYER


  protected void SetSpawnPoint()
    {
        var pos = transform.position;
        _spawnPoint = pos;
    }
    
    
    public void ActiveEnemy(GameObject miTarget)
    {
        
        _playerTarget = miTarget;
        if (_playerTarget)
        {
            _isActive = true;
            var newTargetPlayer = _playerTarget.transform.position;
            _targetPos = newTargetPlayer;

            _playerScript = _playerTarget.GetComponent<PlayerMovement>();

            transform.LookAt(_targetPos);
            FollowTarget(_targetPos);
            AggroHPBarColor(Color.red);
        }
        //SetTarget();
        //_navMesh.SetDestination(_targetPos);
        
    }

    private void SetPlayerTargetReference()
    {
        _playerTarget = GameObject.FindWithTag("Player");
    }
    
    
    protected float GetDistanceToTarget(Vector3 target)
    {
        var temp = target - transform.position;
        var distance = temp.magnitude;
        return distance;
    }

    protected void FollowTarget(Vector3 target)
    {
        //es necesario cambiar su destino? hazlo
        _navMesh.SetDestination(target);
        
        //no? pasando... (check timer...)
    }

    private void StopFollow()
    {
        _navMesh.isStopped = true;
        _isRange = true;
    }

    protected bool CheckTargetInRange()
    {
        if (GetDistanceToTarget(_targetPos) < _generalStats.AttackRange)
        {
            _isRange = true;
            StopFollow();
        }
        else
        {
            _isRange = false;
        }

        return _isRange;
    }

    public void AggroHPBarColor(Color color)
    {
        _healthBar.color = color;
    }
    
    
    public void ReturnSpawnPoint()
    {
        _navMesh.SetDestination(_spawnPoint);
        _isReturning = true;
        AggroHPBarColor(Color.yellow);
        //Recuperar la vida.
    }

    public bool isEnemyActive()
    {
        return _isActive;
    }
    
    public bool isEnemyReturning(){
        return _isReturning;
    }
    
    
    
    public void EnemyTakeDamage(int dmg, int what)
    {
        
        TakeDamage(dmg, what);
        if (!isEnemyActive())
        {
            
        }
        
    }
}
