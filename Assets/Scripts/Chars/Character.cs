﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{

    protected Camera _camera;
    
    [SerializeField] protected float _currentSpeed;
    [SerializeField] protected float _currentHp;
    [SerializeField] protected float _currentMana;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody _rb;

    //Barra de salud
    [SerializeField] protected SpriteRenderer _healthBar;

    [SerializeField] protected TMP_Text _hpBarTextNumber;
    
    
    //Comportamiento de métodos generales
    //Poco más

    protected GeneralStats _generalStats;
    protected GameManager _gm;


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
                HpBarUpdate();
                Die();
                return;
            }
            HpBarUpdate();
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
            HpBarUpdate();
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
            HpBarUpdate();
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

    
    //CUIDADO CON LOS VALORES ASOCIADOS EN EL INSPECTOR
    //ES PROTECTED POR QUE LOS ENEMIGOS LA DEBEN USAR EN EL START.
    //EVITAR QUE ACTUALICE HP Y MIRE A LA CAMARA (DOS FUNCIONES DISTINTAS MEJOR)))
    protected void HpBarUpdate()
    {
        var temp = _currentHp / _generalStats.Health;
        temp *= 3f;
        _healthBar.size = new Vector2(temp, 0.2f);
        var mitexto = _currentHp + " / " + _generalStats.Health;
        
        _hpBarTextNumber.text = mitexto;
        
        HpBarLookCamera();
    }

    protected void HpBarLookCamera()
    {
        _healthBar.transform.forward = _camera.transform.forward;
    }
    
    private void Die()
    {
        _anim.SetTrigger("Die");
        if (!gameObject.CompareTag("Player"))
        {
            _gm.EnemyKilled();
            Destroy(gameObject);
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
    }
    


}
