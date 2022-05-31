using System;
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
    //private Character _char;
    
    //TEndria que tener referencia a la current life/maxLife bla bla bla.
    //Ergo character.

    private void Awake()
    {
        _camera = Camera.main;
        _hpBarSprite = GetComponent<SpriteRenderer>();
        _barText = GetComponentInChildren<TMP_Text>();
        //_char = GetComponentInParent<Character>();
    }
    
    private void Update()
    {
        _hpBarSprite.transform.forward = _camera.transform.forward;
    }

    public void InitializeBar(int maxHp, int mode)
    {
        BarUpdateSprite(maxHp, maxHp);
        BarUpdateText(maxHp, maxHp);
        BarUpdateColor(mode);
    }

    public void UpdateLifeBar(float currentHp, int maxHp)
    {
        BarUpdateSprite(currentHp, maxHp);
        BarUpdateText(currentHp, maxHp);
    }
    public void BarUpdateText(float currentHp, int maxHp)
    {
        var hpText = currentHp + " / " + maxHp;
        _barText.text = hpText;
    }

    public void BarUpdateSprite(float currentHp, int maxHp)
    {
        //Actualizar el tamaño del sprite;
        var temp = currentHp / maxHp;
        temp *= 3f;
        _hpBarSprite.size = new Vector2(temp, 0.2f);
        var hpText = currentHp + " / " + maxHp;
        _barText.text = hpText;
    }

    public void BarUpdateColor(int mode)
    {
        switch (mode)
        {
            //Neutral
            case 0:
                _hpBarSprite.color = Color.yellow;
                break;
            //Combat- Aggro
            case 1:
                _hpBarSprite.color = Color.red;
                break;
            //Friendly
            case 2:
                _hpBarSprite.color = Color.green;
                break;
                default:
                    Debug.Log("ERROR EN EL COLOR BAR");
                    break;
        }
        
    }

    public float GetSizeBar()
    {
        var size = _hpBarSprite.size.x * 0.33f;
        return size;
    }
    
    
    
    
}
