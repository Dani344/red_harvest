using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeActive : MonoBehaviour
{

    [SerializeField] private Collider _rangeCollider;
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        _rangeCollider = GetComponent<Collider>();
        _enemy = GetComponentInParent<Enemy>();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DETECTO PLAYER DESDE ENEMIGORANGEACTIVE");
        if (other.CompareTag("Player"))
        {
            _rangeCollider.enabled = false;
            
           
            _enemy.ActiveEnemy(other.gameObject);
            
        }
        
    }
}
