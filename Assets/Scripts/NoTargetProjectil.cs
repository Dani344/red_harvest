using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTargetProjectil : Projectil
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
        MovementNoTarget();
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
                Die();
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
