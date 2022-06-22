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
    
    //Player Info Canvas
    [SerializeField] private Image _healthBarMain;
    [SerializeField] private Image[] _playerAbilities;
    [SerializeField] private Image _castBar;
    
    //PlayerINFO TMP
    [SerializeField] private TMP_Text _totalCoinsText;
    [SerializeField] private TMP_Text _totalProgressText;
    [SerializeField] private TMP_Text _totalMonolites;
    
    //Selected Info
    [SerializeField] private CanvasGroup _charSelectedInfo;
    [SerializeField] private Image _healthBarSelectedChar;
    [SerializeField] private TMP_Text _percentageHealthSelectedText;
    [SerializeField] private TMP_Text _nameSelectedText;

    #endregion

    public UI_Events _uiEvents;
    
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
        
        HideCharInfo();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActiveSelectedCharInfo();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowCharInfo(name, 0.4f);
        }
    }

    private void SubscribeEvents()
    {
        //_uiEvents._changeHealthPlayer += ChangeHealthPlayer;
        //_uiEvents._changeCooldownImage += ChangeCooldownImage;

        _uiEvents._changeTotalCoins += ChangeTotalCoins;
        _uiEvents._changeTotalProgress += ChangeTotalProgress;
        _uiEvents._monoliteActivated += MonoliteActivated;
        _uiEvents._RefreshCharSelected += RefreshCharSelected;
        _uiEvents._ShowCharInfo += ShowCharInfo;
        _uiEvents._hideCharInfo += HideCharInfo;

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

    private void RefreshCharSelected(string name, float percentageHealth)
    {
        _nameSelectedText.text = name;
        var percentage = percentageHealth * 100 + "%";
        _percentageHealthSelectedText.text = percentage;
        _healthBarSelectedChar.fillAmount = percentageHealth;
    }

    private void ShowCharInfo(string name, float percentageHealth)
    {
        ActiveSelectedCharInfo();
        var percentage = percentageHealth * 100f + "%";
        _percentageHealthSelectedText.text = percentage;
        _healthBarSelectedChar.fillAmount = percentageHealth;
        _nameSelectedText.text = name;
    }

    

    
    
    
    //METHOD NO EVENTS
    private void ActiveSelectedCharInfo()
    {
        _charSelectedInfo.alpha = 1f;
        _charSelectedInfo.interactable = true;
        _charSelectedInfo.blocksRaycasts = true;
    }
    
    
    //PUBLIC METHOD
    public void HideCharInfo()
    {
        _charSelectedInfo.alpha = 0f;
        _charSelectedInfo.interactable = false;
        _charSelectedInfo.blocksRaycasts = false;
    }
    public void ChangeHealthImage(float fillAmount)
    {
        _healthBarMain.fillAmount = fillAmount;
    }

    public void RefreshCastBar(float fillAmount)
    {
        _castBar.fillAmount = fillAmount;
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
        _uiEvents._RefreshCharSelected -= RefreshCharSelected;
        _uiEvents._ShowCharInfo -= ShowCharInfo;
        _uiEvents._hideCharInfo -= HideCharInfo;
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
