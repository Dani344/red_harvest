using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectil : Projectil
{
    
    [SerializeField] private Transform _target;
    private bool _hasTarget;
    [SerializeField] private float fff = 1;

    [SerializeField] private float rotSpeed = 1f;
    //HAY QUE CONTEMPLAR QUE SIEMPRE MIRE AL OBJETIVO
    //HAY QUE CONTROlAR QUE EL PROJECTIL QUE PUEDES ESQUIVAR LO hAGA BIEN
    //INCORPORAR TIMER EN CASO DE QUE NUNCA DE.

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _hasTarget = true;
            transform.LookAt(_target);
        }
        
        if (!_hasTarget) return;

        //OPCION DE TARGET AAA QUE NUNCA FALLA Y QUE CONTROLAMOS VELOCIDAD CON EL FACTOR POR VALOR (fff) test.
        //_dire = _target.position - transform.position;
        //_dire.Normalize();

        //ProjectilMovement(fff);
        
        /*
        _dire = Vector3.MoveTowards(transform.position, _target.position, fff);
        _dire.Normalize();
        Movement();
        Debug.Log(_dire);
        */
        
        MovementEliptic();
    }


    public void SetTarget(Transform target){
        _hasTarget = true;
        _target = target;
    }

    
    private void MovementEliptic()
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
            Die();
        }
        
        if (other.CompareTag(PaperConstants.TAG_ENEMY))
        {
            var temp = other.gameObject.GetComponent<Enemy>();
            if (temp)
            {
                temp.TakeDamage((int) _dmg, 0);
            }
            else
            {
                Debug.Log("ERROR EN ALGO BALL SCRIPT");
            }
            Die();
        }
    
    }
    
}
