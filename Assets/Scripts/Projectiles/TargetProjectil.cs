using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectil : Projectil
{
    
    //[SerializeField] private Transform _target;
    //[SerializeField] private float fff = 1;

    //[SerializeField] protected float rotSpeed = 1f;
    //HAY QUE CONTEMPLAR QUE SIEMPRE MIRE AL OBJETIVO
    //HAY QUE CONTROlAR QUE EL PROJECTIL QUE PUEDES ESQUIVAR LO hAGA BIEN
    //INCORPORAR TIMER EN CASO DE QUE NUNCA DE.



    /*
    private void Start(){
        var testTarget = new Vector3(100,100,100);
        _target.position = testTarget;
    }*/



    private void Update()
    {
        //Debug.Log(_target.name);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _hasTarget = true;
            transform.LookAt(_target);
        }
        
        if (!_hasTarget) return;

        Debug.Log(_target.name);
        if (_target) MovementTarget();
    }

    
    
    private void MovementTarget()
    {
        _dire = _target.position - transform.position;
        _dire.Normalize();

        var QuatLookRot = Quaternion.LookRotation(_target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, QuatLookRot, rotSpeed * Time.deltaTime);
        
        transform.position += transform.forward * _speed * Time.deltaTime;


    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(PaperConstants.TAG_WALLS))
        {
           // Die();
        }
        
        if (other.CompareTag(PaperConstants.TAG_PLAYER))
        {
            if (_isEnemyProjectil)
            {
                var temp = other.gameObject.GetComponent<Character>();
                if (temp)
                {
                    temp.TakeDamage((int) _dmg, 0);
                }
                else
                {
                    Debug.Log("ERROR EN ALGO BALL SCRIPT");
                }
                //Die();
            }
        }
        else
        {
            if (other.CompareTag(PaperConstants.TAG_ENEMY))
            {
                if (!_isEnemyProjectil)
                {
                    var temp = other.gameObject.GetComponent<Character>();
                    if (temp)
                    {
                        temp.TakeDamage((int) _dmg, 0);
                    }
                    else
                    {
                        Debug.Log("ERROR EN ALGO BALL SCRIPT");
                    }
                    //Die();
                }else{
                    Debug.Log("HOLA");
                }
            }
        }
    
    }
    
}
