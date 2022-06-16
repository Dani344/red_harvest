using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    #region REFERENCES

    [SerializeField] private GameManager _gm;
    [SerializeField] private Image _healthBarMain;
    [SerializeField] private Image[] _playerAbilities;
    [SerializeField] private Image _castBar;
    
    //TExtos Pros ASIGNADO INSPECTOR
    [SerializeField] private TMP_Text _totalCoinsText;
    [SerializeField] private TMP_Text _totalProgressText;
    [SerializeField] private TMP_Text _totalMonolites;
    #endregion

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
        _gm = FindObjectOfType<GameManager>();
        _uiEvents = FindObjectOfType<UI_Events>();

        var mainHp = GameObject.FindWithTag(PaperConstants.TAG_MAIN_HPBAR);
        _healthBarMain = mainHp.GetComponent<Image>();

        var castBar = GameObject.FindWithTag(PaperConstants.TAG_CAST_BAR);
        _castBar = castBar.GetComponent<Image>();
        
        

        var icons = GameObject.FindGameObjectsWithTag(PaperConstants.TAG_ABILITIES_ICONS);
        _playerAbilities = new Image[icons.Length];
        
        for (int i = 0; i < icons.Length; i++)
        {
            _playerAbilities[i] = icons[i].GetComponent<Image>();
        }
        
        //FALTA COINS/PROGRESSION/MONGOLITES/
    }

    private void Start()
    {
        SubscribeEvents();
        
        var coinTextInit = "Coins: 0";
        _totalCoinsText.text = coinTextInit;

        var progressTextInit = "0%";
        _totalProgressText.text = progressTextInit;

        var monolites = "Mongolites: 0";
        _totalMonolites.text = monolites;

        for (int i = 0; i < _playerAbilities.Length; i++)
        {
            _playerAbilities[i].fillAmount = 0f;
        }
        
    }

    private void SubscribeEvents()
    {
        //_uiEvents._changeHealthPlayer += ChangeHealthPlayer;
        //_uiEvents._changeCooldownImage += ChangeCooldownImage;

        _uiEvents._changeTotalCoins += ChangeTotalCoins;
        _uiEvents._changeTotalProgress += ChangeTotalProgress;
        _uiEvents._monoliteActivated += MonoliteActivated;

    }

    //EVENTS METHODS
    private void ChangeTotalCoins(int totalPlayerCoins)
    {
        var newCoinsText = "Coins: " + totalPlayerCoins;
        _totalCoinsText.text = newCoinsText;
    }

    private void ChangeTotalProgress(float progress)
    {
        var newProgressText = progress + "%";
        _totalProgressText.text = newProgressText;
    }

    private void MonoliteActivated(int totalMonolites)
    {
        var monolitesText = "Monolites: " + totalMonolites;
        _totalMonolites.text = monolitesText;
    }
    //PUBLIC METHOD
    public void ChangeHealthImage(float fillAmount)
    {
        _healthBarMain.fillAmount = fillAmount;
    }

    public void InitCooldownImage(int abilityIndex)
    {
        if (_playerAbilities[abilityIndex])
        {
            _playerAbilities[abilityIndex].fillAmount = 1f;
        }
    }
    
    public void RefreshCooldownImage(int abilityIndex, float diferencialFillAmmount)
    {
        if (_playerAbilities[abilityIndex])
        {
            _playerAbilities[abilityIndex].fillAmount -= diferencialFillAmmount;
            if (_playerAbilities[abilityIndex].fillAmount < 0f)
            {
                _playerAbilities[abilityIndex].fillAmount = 0f;
            }
        }
    }
    
    
    //FALTA SUSCRIBIR TODOS LOS QUE HAYA
    private void OnDestroy()
    {
        _uiEvents._changeTotalCoins -= ChangeTotalCoins;
        _uiEvents._changeTotalProgress -= ChangeTotalProgress;
        _uiEvents._monoliteActivated -= MonoliteActivated;
    }
    

    public void RefreshSelectedInfoEnemy()
    {
        Debug.Log("Refresh Enemy Selected");
    }
    

    public void ShowOptionMenu()
    {
        Debug.Log("OPTION MENU");
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER OR VICTORY");
    }
    
    
}
