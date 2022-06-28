using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectil
{
    
    private void Start()
    {
        _count = 0f;
        //_dire = Vector3.zero;
        //Faltaria inicializar el tipo de daño que hace este projectil.
    }

    private void Update()
    {
        _count += Time.deltaTime;
        if (_count > _maxTimeLife)
        {
            Die();
        }
        
        transform.position += new Vector3(_dire.x * _speed * Time.deltaTime, 0f, _dire.z * _speed * Time.deltaTime);
        
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
                //temp.ActiveEnemy();
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
