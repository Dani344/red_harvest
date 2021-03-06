
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
    protected UI_Manager _uiManager;
    
    #endregion
    
    protected Transform _targetPos;
    protected Vector3 _spawnPoint;
    protected bool _isSelected = false;
    
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
            }
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
            _gm.KillPlayer();
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
        if (_isSelected)
        {
            var percentage = _currentHp / _generalStats.Health;
            _uiManager._uiEvents._RefreshCharSelected?.Invoke(this.name, percentage);
        }
        
    }

    public float GetCurrentHp()
    {
        return _currentHp;
    }

    public float PercentageActualHp()
    {
        var perc = _currentHp / _generalStats.Health;
        return perc;
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
    
    public void Selected(bool isSelected)
    {
        _isSelected = isSelected;
    }

    public bool isSelected()
    {
        return _isSelected;
    }
    
}
