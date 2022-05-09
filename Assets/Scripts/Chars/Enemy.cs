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
    
    
    protected float _newPathCount;
    protected float _timeForUpdatePath = 2f;
    
    //CUIDADO POR QUE SOLO CONTEMPLAMOS AL PLAYER
    public void ActiveEnemy(GameObject miTarget)
    {
        //SetPlayerTargetReference();
        _playerTarget = miTarget;
        if (_playerTarget)
        {
            _isActive = true;
            var newTargetPlayer = _playerTarget.transform.position;
            _targetPos = newTargetPlayer;
            transform.LookAt(_targetPos);
            FollowTarget(_targetPos);
            AggroHPBarColor();
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

    private void AggroHPBarColor()
    {
        _healthBar.color = Color.red;
    }
    
    
    
    
}
