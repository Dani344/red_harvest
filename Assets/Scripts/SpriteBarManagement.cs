using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpriteBarManagement : MonoBehaviour
{
    private Camera _camera;
    private SpriteRenderer _hpBarSprite;
    private TMP_Text _barText;
    
    //TEndria que tener referencia a la current life/maxLife bla bla bla.
    //Ergo character.

    private void Awake()
    {
        _camera = Camera.main;
        _hpBarSprite = GetComponent<SpriteRenderer>();
        _barText = GetComponentInChildren<TMP_Text>();
    }
    
    private void Update()
    {
        _hpBarSprite.transform.forward = _camera.transform.forward;
    }

    public void BarUpdateText(int currentHp, int maxHp)
    {
        var hpText = currentHp + " / " + maxHp;
        _barText.text = hpText;
    }

    public void BarUpdateSprite(int currentHp, int maxHp)
    {
        //Actualizar el tamaño del sprite;
    }

    public void BarUpdateColor(int mode)
    {
        //COLOR DE AGGRO/DEBUFFOS ETC
        //AGGRO
        //PASSIVE
        //NEUTRAL
        //STUN 
        //ETC
    }
    
    
    
    
}
