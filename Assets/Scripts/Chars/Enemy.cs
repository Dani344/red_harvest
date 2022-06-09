
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    
    //FALTA QUE LOS ENEMIGOS SEPAN QUE EL JUGADOR O TARGET MUERE PARA DEJAR
    //DE DISPARAR!"!!!!
    [SerializeField] protected bool _isReturning = false;
    protected float _newPathCount;
    protected float _timeForUpdatePath = 2f;
    
    //CUIDADO POR QUE SOLO CONTEMPLAMOS AL PLAYER


    private void Awake()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _gm = FindObjectOfType<GameManager>();
        _barManagement = GetComponentInChildren<SpriteBarManagement>();
    }
    
    
    protected void SetSpawnPoint()
    {
        var pos = transform.position;
        _spawnPoint = pos;
    }
  
    public void ActiveEnemy(GameObject miTarget)
    {
        if (miTarget)
        {
            _targetGO = miTarget;
            _isActive = true;
            var newTargetPlayer = _targetGO.transform;
            _targetPos = newTargetPlayer;

            _playerScript = _targetGO.GetComponent<PlayerMovement>();

            transform.LookAt(_targetPos);
            FollowTarget(_targetPos.position);
            _barManagement.BarUpdateColor(PaperConstants.HP_BAR_COMBAT);
        }
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
        if (GetDistanceToTarget(_targetPos.position) < _generalStats.AttackRange)
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
    
    public void ReturnSpawnPoint()
    {
        _isReturning = true;
        _navMesh.SetDestination(_spawnPoint);
        _barManagement.BarUpdateColor(PaperConstants.HP_BAR_NEUTRAL);
        //Recuperar la vida.
    }

    public bool isEnemyActive()
    {
        return _isActive;
    }
    
    public bool isEnemyReturning(){
        return _isReturning;
    }

    public GameObject GetTargetGO()
    {
        return _targetGO;
    }

    public void SetTargetGO(GameObject target)
    {
        
        _targetGO = target;
        _targetPos = target.GetComponent<Transform>();
    }
    
    //Para plantear que se active si recibe daño -> probablemente evento.
    public void EnemyTakeDamage(int dmg, int what)
    {
        
        TakeDamage(dmg, what);
        if (!isEnemyActive())
        {
            
        }
    }

    //******TEST OVERRIDE
    public override void Init()
    {
        Debug.Log("INIT ENEMY");
        //base.Init();
        //Init();
    }
    
    //VIRTUAL AND OVERRIDE CLASSES
    public override void Movement(){
        Debug.Log("MOVEMENT ENEMY");
    }
    
    
    
}
