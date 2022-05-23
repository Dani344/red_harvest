using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseParticleTime : MonoBehaviour
{
    [SerializeField] private float _maxTimeParticle;

    private float _count = 0f;
    private void Start()
    {
        _count = 0f;
    }

    
    private void Update()
    {
        _count += Time.deltaTime;

        if (_count > _maxTimeParticle)
        {
            Destroy(gameObject);
        }
        
    }
}
