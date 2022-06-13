using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Patrol : Enemy
{
    private float _waitBtwPoints = 2f;
    
    
    //[SerializeField] private Vector3[] _points;
    [SerializeField] private Vector3 _destination;

    private float _stopMoveCount = 0f;
    private bool _hasArrived = false;
    //private Boss_Combat _bc;

    /*private void Awake()
    {
        //Cogemos las referencias de los puntos para patrullar o ya las asignamos por el inspector.
        _anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _gm = FindObjectOfType<GameManager>();
        _barManagement = GetComponentInChildren<SpriteBarManagement>();

        //_bc = GetComponent<Boss_Combat>();

    }*/
    
    private void Start()
    {
          InitBoss();
    }


    public void InitBoss()
    {
        
        NormalSetUp("BOSSSS", 100, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 15, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        
        ShowCharacterInformation();
        _barManagement.InitializeBar(_generalStats.Health, PaperConstants.HP_BAR_NEUTRAL);
        _destination = GenerateNextDestination();
        //_navMesh.speed = 2f; 
        
        FollowTarget(_destination);
    }

    private Vector3 GenerateNextDestination()
    {
        var nextPosX = Random.Range(-10, 10);
        var nextPosZ = Random.Range(-10, 10);

        var nextPos = transform.position + new Vector3(nextPosX, 0f, nextPosZ);

        return nextPos;
    }

    public override void Movement()
    {
        if (_navMesh.remainingDistance < 0.15f && !_hasArrived)
        {
            _hasArrived = true;
            _stopMoveCount = 0f;
        }

        if (_hasArrived)
        {
            _stopMoveCount += Time.deltaTime;
            
            if (_stopMoveCount > 1f)
            {
                _hasArrived = false;
                _destination = GenerateNextDestination();
                FollowTarget(_destination);
            }
        }
    }

    public override void Init(){
        
        Debug.Log("INIT BOSS pATROL");
        InitBoss();
    }
    
        
    
}
