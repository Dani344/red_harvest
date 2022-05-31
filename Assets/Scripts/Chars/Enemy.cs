
using UnityEngine;


public class Enemy : Character
{
    
    //FALTA QUE LOS ENEMIGOS SEPAN QUE EL JUGADOR O TARGET MUERE PARA DEJAR
    //DE DISPARAR!"!!!!
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
        
        _targetGO = miTarget;
        if (_targetGO)
        {
            _isActive = true;
            var newTargetPlayer = _targetGO.transform.position;
            _targetPos = newTargetPlayer;

            _playerScript = _targetGO.GetComponent<PlayerMovement>();

            transform.LookAt(_targetPos);
            FollowTarget(_targetPos);
            _barManagement.BarUpdateColor(PaperConstants.HP_BAR_COMBAT);
        }
        //SetTarget();
        //_navMesh.SetDestination(_targetPos);
        
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
    
    
    //Para plantear que se active si recibe daño -> probablemente evento.
    public void EnemyTakeDamage(int dmg, int what)
    {
        
        TakeDamage(dmg, what);
        if (!isEnemyActive())
        {
            
        }
        
    }
}
