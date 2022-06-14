using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    public UI_Events _uiEvents;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _changeHealthPlayer?.Invoke(100);
            
        }
    }*/
    
    private void Awake()
    {
        _uiEvents = FindObjectOfType<UI_Events>();
    }

    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _uiEvents._changeHealthPlayer += ChangeHealthPlayer;
    }

    private void ChangeHealthPlayer(int health)
    {
        //Actualiza main vida
        Debug.Log("Actualiza Vida");
    }

    //FALTA SUSCRIBIR TODOS LOS QUE HAYA
    private void OnDestroy()
    {
        _uiEvents._changeHealthPlayer -= ChangeHealthPlayer;
    }
    
    
    public void RefreshPlayerLife(int health)
    {
        //_changeHealthPlayer?.Invoke(health);
    }

    public void RefreshSelectedInfoEnemy()
    {
        Debug.Log("Refresh Enemy Selected");
    }

    public void RefreshTotalPlayerCoins()
    {
        Debug.Log("TOTAL COINS REFRESH");
    }

    public void RefreshPlayerProgression()
    {
        Debug.Log("TOTAL PROGRESSION PLAYER");
    }

    public void RefreshTotalMonolites()
    {
        Debug.Log("Refresh Total monolites");
    }

    public void ShowOptionMenu()
    {
        Debug.Log("OPTION MENU");
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER OR VICTORY");
    }

    public void RefreshIconCooldown(Image icon, float cd = 1f)
    {
        Debug.Log("COOLDOWN");
    }
    
    
    
}
