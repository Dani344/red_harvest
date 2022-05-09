using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Projectil
{

    [SerializeField] private float _scaleMult;
    
    private void Start()
    {
        _count = 0f;
        
    }

    // Update is called once per frame
    private void Update()
    {
        _count += Time.deltaTime;
        if (_count > _maxTimeLife)
        {
            Die();
        }
        
        transform.position += new Vector3(_dire.x * _speed * Time.deltaTime, 0f, _dire.z * _speed * Time.deltaTime);
        transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * _scaleMult;
    }
}
