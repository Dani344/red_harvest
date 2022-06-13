
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour
{
    
    [SerializeField] protected float _currentSpeed;
    [SerializeField] protected float _currentHp;
    [SerializeField] protected float _currentMana;
    [SerializeField] protected bool _isRange;
    [SerializeField] protected bool _isActive;
    [SerializeField] protected GameObject _targetGO;
    
    
    #region ReferencesComponents
    
    protected Camera _camera;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected SpriteBarManagement _barManagement;
    protected NavMeshAgent _navMesh;
    protected PlayerMovement _playerScript;
    protected GeneralStats _generalStats;
    protected GameManager _gm;
    
    #endregion
    
    protected Transform _targetPos;
    protected Vector3 _spawnPoint;
    
    protected void NormalSetUp(string name, int health, int mana, float healthReg, float manaReg, float armor,
        float magicResist, float attackDamage, float critDamage, int attackRange, int abilityPower, int moveSpeed, float baseAttackSpeed,
        float attackWinUp, float asratio, float bonusAS, int missileSpeed, int level, int neededExp, int xp)
    {
        //Basic Stats
        _generalStats = new GeneralStats();
        _generalStats.Name = name;
        _generalStats.Health = health;
        _generalStats.Mana = mana;
        _generalStats.HealthRegen = healthReg;
        _generalStats.ManaRegen = manaReg;
        _generalStats.Armor = armor;
        _generalStats.MagicResist = magicResist;

        _generalStats.AttackDamage = attackDamage;
        _generalStats.AbilityPower = abilityPower;

        _generalStats.CritDamage = critDamage;
        _generalStats.AttackRange = attackRange;

        _generalStats.MoveSpeed = moveSpeed;
        
        //AttackSpeedStats
        _generalStats.BaseAttackSpeed = baseAttackSpeed;
        _generalStats.AttackWinUp = attackWinUp;
        _generalStats.AS_Ratio = asratio;
        _generalStats.BonusAs = bonusAS;

        _generalStats.MissileSpeed = missileSpeed;
        
         //Experience Stats
        _generalStats.Level = level;
        _generalStats.NeededExperience = neededExp;
        _generalStats.Experience = xp;

        _currentHp = _generalStats.Health;
        _currentSpeed = _generalStats.MoveSpeed;
        
        //BOSS
    }

    protected void SetUpDefaultForTest()
    {
        _generalStats = new GeneralStats();
        Debug.Log("Health: "  + _generalStats.Health + "mana: " + _generalStats.Mana);
    }
    
    //MEJORAR LOS TAKE DAMAGE!!!!
    //Tenemos en cuenta el armor
    protected void TakePhysicDamage(int damage)
    {
        int realDamage = (int)(damage - _generalStats.Armor);
        if (realDamage > 0)
        {
            _currentHp -= realDamage;
            if (_currentHp < 1)
            {
                _currentHp = 0f;
                Die();
                return;
            }
        }
    }

    //Tenemos en cuenta el mr
    protected void TakeMagicDamage(int damage)
    {
        int realDamage = (int)(damage - _generalStats.MagicResist);
        if (realDamage > 0)
        {
            _currentHp -= realDamage;
            if (_currentHp < 1)
            {
                Die();
                
            }
            //HpBarUpdate();
        }
    }

    //tenemos en cuenta solo la vida.
    protected void TakeTrueDamage(int damage)
    {
        if (damage > 0)
        {
            _currentHp -= damage;
            if (_currentHp < 1)
            {
                Die();
                return;
            }
            //HpBarUpdate();
        }
    }

    protected void Heal(int heal)
    {
        //Cuidado si justo muere el player
        _currentHp += heal;
        if (_currentHp > _generalStats.Health)
        {
            _currentHp = _generalStats.Health;
        }
    }
    
    private void Die()
    {
        _anim.SetTrigger("Die");
        
        if (!gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "Boss")
            {
                Debug.Log("HEMOS MATADO BOSS");
                _gm.FinishGame();
            }
            else
            {
                _gm.EnemyKilled();
                Destroy(gameObject);
            }
            
        }
        else
        {
            var player = gameObject.GetComponent<PlayerMovement>();
            player.StopMove();
            player.PlayerDeath();
            //player.Die();
        }
    }

    protected void ShowCharacterInformation(){
        _generalStats.ShowInfo();
    }

    public void TakeDamage(int dmg, int whatDmg)
    {
        switch (whatDmg)
        {
            case 0:
                TakePhysicDamage(dmg);
                break;
            case 1:
                TakeMagicDamage(dmg);
                break;
            case 2:
                TakeTrueDamage(dmg);
                break;
            default: 
                Debug.Log("Error en takeDamage de character");
                break;
        }
        //TakePhysicDamage(dmg);
        _barManagement.UpdateLifeBar(_currentHp, _generalStats.Health);
    }
    
    //TEST OVERIDE INIT
    public virtual void Init()
    {
        Debug.Log("INIT CHARACTER");
    }

    public virtual void Movement()
    {
        
    }
    
    public void GenerateStatsEmpty(){
        _generalStats = new GeneralStats();
    }

    public void RefreshGeneralStats(GeneralStats gs){
        _generalStats = gs;
    }

    public GeneralStats GetGeneralStats(){
        return _generalStats;
    }
    
}
