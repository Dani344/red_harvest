using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAoEDmg : MonoBehaviour
{
    private Collider _col;
    private bool _explose = false;
    private float _count = 0f;
    [SerializeField] private float _timeForExplo = 5f;

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void Start()
    {
        _col.enabled = false;
        _count = 0f;
    }

    private void Update()
    {
        _count += Time.deltaTime;
        if (_count > _timeForExplo && !_explose)
        {
            _col.enabled = true;
            _explose = true;
        }

        if (_explose && _count > _timeForExplo + 0.2f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PaperConstants.TAG_PLAYER))
        {
            var player = other.GetComponent<Character>();
            if (!player) return;
            
            player.TakeDamage(100, 0);
        }
    }
}
