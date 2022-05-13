using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gm;
    private void Awake()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Reproducir sonido
            _gm.RecolectCoin(1);
            Destroy(gameObject);
            
        }
    }
}
