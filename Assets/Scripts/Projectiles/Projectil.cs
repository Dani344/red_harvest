using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxTimeLife;
    [SerializeField] protected float _dmg;
    [SerializeField] protected float _rotSpeed;

    [SerializeField] protected bool _isEnemyProjectil = false;
    [SerializeField] protected float rotSpeed = 1f;
    [SerializeField] protected Transform _target;
    protected bool _hasTarget;
    protected int _dmgType;
    protected float _count;
    protected Vector3 _dire;
    
    //Setear las cosas bien. Falta declarar el tipo de daño que tiene el projectil
    
    
    //Falta definir un daño base de la bala y un multiplicador cuando hagamos
    //El set up al invocarla.
    public void ProjectilNoTargetSetUp(Vector3 dire, float speed, float dmg, bool isEnemyProjectil)
    {
        //dmg = _dmg * dmg;
        SetDirection(dire);
        SetSpeed(speed);
        SetDamage(dmg);
        _isEnemyProjectil = isEnemyProjectil;
        Debug.Log("NO TARGET SET UP");
    }

    public void ProjectilTargetSetUp(Vector3 dire, float speed, float dmg, float rotSpeed, bool isEnemyProjectil, Transform target)
    {
        SetDirection(dire);
        SetSpeed(speed);
        SetDamage(30f);
        SetRotSpeed(rotSpeed);
        SetTargetProjectil(target);
        _isEnemyProjectil = isEnemyProjectil;
    }
    
    
    protected void MovementNoTarget(float factor = 1f){
        
        transform.position += new Vector3(_dire.x * _speed * factor * Time.deltaTime, 0f, _dire.z * _speed * factor *  Time.deltaTime);
        
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

    public void SetRotSpeed(float rotSpeed)
    {
        _rotSpeed = rotSpeed;
    }

    public void SetTargetProjectil(Transform target)
    {
        _target = target;
        if (_target)
        {
            _hasTarget = true;
        }
    }
    public float GetBaseDmg()
    {
        return _dmg;
    }
    

}
