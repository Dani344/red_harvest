﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxTimeLife;
    [SerializeField] protected float _dmg;
    protected float _count;
    protected Vector3 _dire;
    
    //Setear las cosas bien. Falta declarar el tipo de daño que tiene el projectil
    
    
    //Falta definir un daño base de la bala y un multiplicador cuando hagamos
    //El set up al invocarla.
    public void ProjectilSetUp(Vector3 dire, float speed, float dmg)
    {
        //dmg = _dmg * dmg;
        SetDirection(dire);
        SetSpeed(speed);
        SetDamage(dmg);
    }

    public void ProjectilSetUp(Vector3 dire, float speed)
    {
        SetDirection(dire);
        SetSpeed(speed);
        SetDamage(30f);
    }
    
    
    protected void Die()
    {
        Destroy(gameObject);
    }
    public void SetDirection(Vector3 dire)
    {
        _dire = dire;
        _dire.Normalize();
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetDamage(float dmg)
    {
        _dmg = dmg;
    }

    public float GetBaseDmg()
    {
        return _dmg;
    }
    

}
