using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAoeBehaviour : MonoBehaviour
{
    
    //IMPORTANTE!
    //LA DURACION LA TENEMOS FIJADA DESDE EL SISTEMA DE PARTICULAS 'A MANO'
    [SerializeField] private int _dmgPerSecond;
    [SerializeField] private int _AoeAlivetime;
    private float _count;
    private float _deathCount = 0f;


    private void Start()
    {
        _count = 0f;
        _deathCount = 0f;
    }

    private void Update()
    {
        _deathCount += Time.deltaTime;
        if (_deathCount > _AoeAlivetime)
        {
            Destroy(gameObject);
        }

    }
    
    
    //CUIDADO QUE NO ES RECOMENDABLE EL CONTADOR Y GETEAR CHARACTER TOh EL RATO....
    private void OnTriggerStay(Collider other)
    {
        //var temp = other.GetComponent<Collider>();

        var algo = other.GetComponent<Character>();
        if (!algo) return;

        _count += Time.deltaTime;
        if (_count > 1f)
        {
            algo.TakeDamage(_dmgPerSecond, 0);
            _count = 0f;
        }
    }
    
    
}
