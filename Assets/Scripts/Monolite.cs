using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolite : MonoBehaviour
{
    
    [SerializeField] private Color _color;
    [SerializeField] private MeshRenderer _cubeRenderer;
    [SerializeField] private bool _monoliteActive = false;
    [SerializeField] private Light _light;
    private GameManager _gm;
    
    private void Awake()
    {
        //Los get components despues.
        _gm = FindObjectOfType<GameManager>();
        var mat = _cubeRenderer.material;
        _color = mat.color;
    }

    private void Start()
    {
        _monoliteActive = false;
        _light.color = Color.white;
        _color = Color.black;
    }
    
    /*
    private void Update()
    {
        //Mover el cubo o la animacion activarla en modo loop.
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PaperConstants.TAG_PLAYER) && !_monoliteActive)
        {
            ActiveMonolite();
        }
    }

    private void ActiveMonolite()
    {
        _monoliteActive = true;
        _color = new Color(95f, 255f, 0f);
        _light.color = new Color(95f, 255f, 0f);
        _gm.MonoliteActivated();
    }

    /* // De momento no se necesita desactivar.
    private void DeactiveMonolite()
    {
        _monoliteActive = false;
        _color = Color.black;
        _light.color = Color.white; 
    }*/
    
}
