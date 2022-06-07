﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectil : Projectil
{
    private void Update()
    {
        
        //Debug.Log(_target.name);
        //QUizas mecanica adicional
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _hasTarget = true;
            transform.LookAt(_target);
        }
        
        if (!_hasTarget) return;
        
        MovementTarget();
        
        //TIMER PARA QUE MUERA.
        //HACER DAÑO SI O SI SI FIJA TARGET Y SIEMPRE DA.
        //NO HACER DAÑO SI PUEDES ESQUIVAR T < 1 en SLERP
    }

    
    
    private void MovementTarget()
    {
        _dire = _target.position - transform.position;
        _dire.Normalize();
        Debug.Log(Time.deltaTime);
        //ES DIRE DIRECTAMENTE
        var QuatLookRot = Quaternion.LookRotation(_target.position - transform.position);
        Debug.DrawRay(transform.position, _dire);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, QuatLookRot, _rotSpeed * Time.deltaTime);
        
        //1 para nunca fallar
        //0 para nunca dar.
        
        transform.position += transform.forward * _speed * Time.deltaTime;
        //transform.Translate(transform.forward * _speed * Time.deltaTime);


    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(PaperConstants.TAG_WALLS))
        {
           Die();
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

                    Die();

                }
            }
        }
    }
    
}
