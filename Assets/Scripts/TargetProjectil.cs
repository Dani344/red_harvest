using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectil : Projectil
{
    
    [SerializeField] private Transform _target;
    private bool _hasTarget;
    

    //HAY QUE CONTEMPLAR QUE SIEMPRE MIRE AL OBJETIVO
    //HAY QUE CONTROlAR QUE EL PROJECTIL QUE PUEDES ESQUIVAR LO hAGA BIEN
    //INCORPORAR TIMER EN CASO DE QUE NUNCA DE.

    
    // Update is called once per frame
    void Update()
    {
        if (!_hasTarget) return;

        _dire = _target.position - transform.position;
        _dire.Normalize();
        
        ProjectilMovement();

    }


    public void SetTarget(Transform target){
        _hasTarget = true;
        _target = target;
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
