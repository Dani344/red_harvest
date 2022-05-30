using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLookCamera : MonoBehaviour
{
    private Camera _camera;
    private SpriteRenderer _hpBarSprite;

    private void Awake()
    {
        _camera = Camera.main;
        _hpBarSprite = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        _hpBarSprite.transform.forward = _camera.transform.forward;
    }
    
    
    
    
}
