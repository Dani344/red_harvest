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
        //Debug.Log("DETECTO PLAYER DESDE ENEMIGORANGEACTIVE");
        if (other.CompareTag(PaperConstants.TAG_PLAYER))
        {
            
            if (_enemy.isEnemyActive()) return;
            if (_enemy.isEnemyReturning()) return;
            //Debug.Log("BUSCA PLAYER");
            var _player = other.gameObject;
            _enemy.ActiveEnemy(_player);
            //_rangeCollider.enabled = false;
            
            
        }
        
    }
}
