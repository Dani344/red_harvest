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
    
    private void Awake()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<SpriteRenderer>();
        _navMesh = GetComponent<NavMeshAgent>();
        //_rb = GetComponent<Rigidbody>();


    }
    
    private void Start()
    { 
        NormalSetUp("DUMMY", 100, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 10, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        ShowCharacterInformation();
        
        HpBarUpdate();

        _count = 0f;
        _isRange = false;
        _newPathCount = 0f;
        //Maybe no es necesario esta linea.
        //_targetPosition = Vector3.zero;
        
        //FUNCION PARA VER SI HACE EL IDIOTA EL MOB
        //_navMesh.isPathStale
        SetSpawnPoint();
        AggroHPBarColor(Color.yellow);
    }


    private void Update()
    {
        //TEST//

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnSpawnPoint();
        }
        
        //END TEST///
        
        if (!_isActive) return;
        if (_isActive && _isReturning)
        {
            if (_navMesh.remainingDistance < 0.25f)
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
            //Debug.Log("ACTUALIZAS");
            if (!_isRange)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_targetPos);
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
        
        _healthBar.transform.forward = _camera.transform.forward;
        _targetPos = _playerTarget.transform.position;
    }


    private void Shoot()
    {
        
        if (_projectilPrefab)
        {
            var temp = Instantiate(_projectilPrefab, transform.position, Quaternion.identity);
            var newProjectil = temp.GetComponent<Projectil>();
            //COMPROBAR EL DAÑO YA QUE DE MOMENTO ES CONSTANTE
            transform.LookAt(_targetPos);
            _healthBar.transform.forward = _camera.transform.forward;
            var dire = _targetPos - transform.position;
            dire.Normalize();
            newProjectil.ProjectilSetUp(dire, _generalStats.MissileSpeed, 50f);
        }
        
    }
    /*
    private void FollowTarget()
    {
        //_navMesh.SetDestination(_targetPos);
        //_navMesh.SetDestination(new Vector3(50,0,50));
    }*/
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLISION ENEMYFRONTAL");
        }
    }*/
}
