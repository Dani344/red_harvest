using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFrontal : Enemy
{
    
    [SerializeField] private GameObject _projectilPrefab;
    private float _count;
    
    private void Start()
    { 
        NormalSetUp("DUMMY", 100, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 15, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        //ShowCharacterInformation();
        
        _barManagement.InitializeBar(_generalStats.Health, PaperConstants.HP_BAR_NEUTRAL);
        

        _count = 0f;
        _isRange = false;
        _newPathCount = 0f;
        //Maybe no es necesario esta linea.
        //_targetPosition = Vector3.zero;
        
        //FUNCION PARA VER SI HACE EL IDIOTA EL MOB
        //_navMesh.isPathStale
        SetSpawnPoint();
        
    }
    
    private void Update()
    {
        //TEST//
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnSpawnPoint();
        }
        */
        //END TEST///
        
        if (!_isActive) return;
        
        if (_isActive && _isReturning)
        {
            _navMesh.isStopped = false;
            //_isActive = (_navMesh.remainingDistance < 0.25f);
            //_isReturning = _isActive;
            //return;
            
            
            if (_navMesh.remainingDistance < 0.15f)
            {
                _isReturning = false;
                _isActive = false;
            }
            return;
            
        }

        CheckTargetInRange();
        _newPathCount += Time.deltaTime;

        if (_newPathCount > _timeForUpdatePath)
        {
            if (!_isRange)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_targetPos.position);
            }
            _newPathCount = 0f;
        }
        
        
        if (_isRange)
        {
            //vigilar el isStopped si lo necesitamos
            _navMesh.isStopped = true;
            _count += Time.deltaTime;
            if (_count > _generalStats.BaseAttackSpeed)
            {
                Shoot();
                _count = 0f;
            }
        }
        
        _targetPos = _targetGO.transform;
        
        //Checkea que no esté en la base el player
        if (_playerScript)
        {
            if (_playerScript.isPlayerSafeZone() || !_playerScript.isPlayerAlive())
            {
                ReturnSpawnPoint();
            }
        }
    }


    private void Shoot()
    {
        if (_projectilPrefab)
        {
            var temp = Instantiate(_projectilPrefab, transform.position + transform.forward, Quaternion.identity);
            temp.transform.LookAt(_targetPos);
            var newProjectil = temp.GetComponent<Projectil>();
            //COMPROBAR EL DAÑO YA QUE DE MOMENTO ES CONSTANTE
            transform.LookAt(_targetPos);
            var dire = _targetPos.position - transform.position;
            dire.Normalize();
            //Debug.Log(newProjectil);
            /*
            newProjectil.ProjectilTargetSetUp(
                dire,
                _generalStats.MissileSpeed,
                50f,
                2f,
                true,
                _targetPos
                );*/
            newProjectil.ProjectilNoTargetSetUp(dire, _generalStats.MissileSpeed, 40f, true);

        }
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLISION ENEMYFRONTAL");
        }
    }*/
}
