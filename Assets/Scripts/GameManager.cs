﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //FALTA GESTIONAR QUE LOS ENEMIGOS SEPAN QUE EL PLAYER SE MUERE

    //FALTA GESTIONAR EL ARRAY DE ENEMIGOS
    //FALTA CREAR UN CONTADOR DE MAPA??
    
    
    //CREAR LAS FUNCIONES NECESARIAS PUBLICAS Y PENSAR EN EL ESQUEMA
    
    [SerializeField] private CameraMovement _cameraMove;
    
    
    #region Prefabs
    [SerializeField] private GameObject _map;

    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerSpawnPoint;
    
    [SerializeField] private GameObject[] _enemiesSpawns;
    [SerializeField] private GameObject[] _enemiesPrefabs;
    
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform _bossSpawn;
    #endregion

    #region PlayerInterfaceInfo

    [SerializeField] private int _totalPlayerCoins;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private float _inGameProgress = 0f;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private bool _playerDead;

    [SerializeField] private TMP_Text _totalGameTimeText;

    #endregion

    [SerializeField] private int _totalNumEnemies = 0;
    [SerializeField] private int _currentNumEnemies = 0;

    private float _totalGameTime = 0f;
    private float _count = 0f;
    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMovement>();
        
    }
    
    private void Start()
    {
        _totalNumEnemies = 0;
        _currentNumEnemies = 0;
        
        var temp = "Coins: 0";
        _coinText.text = temp;
        _totalPlayerCoins = 0;
        _inGameProgress = 0f;
        _totalGameTime = 0f;
        _count = 0f;
        
        SpawnPlayer();
        SpawnEnemies();
        SpawnBoss();
        
        ProgressInGame();
    }
    
    
    //CONTADOR TIMER TOTAL
    /*
    private void Update()
    {
        _count += Time.deltaTime;
        if (_count > 1f)
        {
            _totalGameTime += _count;
            _count = 0f;
            TimerGameText();
        }
        
    }*/



    private void GenerateMap()
    {
        
    }

    private void SpawnPlayer()
    {
        Instantiate(_player, _playerSpawnPoint.position, Quaternion.identity);
        _cameraMove.Center();

    }
    
    private void SpawnEnemies()
    {
        var temp = GameObject.FindGameObjectsWithTag("SpawnEnemy");
        _enemiesSpawns = temp;

        for (int i = 0; i < _enemiesSpawns.Length; i++)
        {
            if (_enemiesPrefabs[0])
            {
                //TENER EN CUENTA SI EL SETUP LO HACEMOS AQUI SI QUEREMOS ALGO ESPECIAL??
                Instantiate(_enemiesPrefabs[0], _enemiesSpawns[i].transform.position, Quaternion.identity);
                _totalNumEnemies += 1;
            }
            
        }

        _currentNumEnemies = _totalNumEnemies;
    }

    private void SpawnBoss()
    {
        if (_bossPrefab)
        {
            
            var boss = Instantiate(_bossPrefab, _bossSpawn.position, Quaternion.identity);
            boss.transform.localScale = new Vector3(2f, 2f, 2f);

        }
    }
    public void RecolectCoin(int ammount)
    {
        _totalPlayerCoins += ammount;
        var newCoinsText = "Coins: " + _totalPlayerCoins;
        _coinText.text = newCoinsText;
    }

    public void ProgressInGame()
    {
        //Debug.Log(_currentNumEnemies + "CURRENT E");
        var porcentajeRestante = (float) _currentNumEnemies / _totalNumEnemies;
        _inGameProgress = (1f - porcentajeRestante) * 100f;
        //Debug.Log(_inGameProgress + "PROG");
        var texto = _inGameProgress + " %";
        _progressText.text = texto;
    }

    public void EnemyKilled()
    {
        _currentNumEnemies -= 1;
        if (_currentNumEnemies == 0)
        {
            Debug.Log("NO QUEDAN ENEMIGOS EN EL MAPA");
        }
        
        ProgressInGame();
    }

    private void TimerGameText()
    {
        var totalTime = Mathf.RoundToInt(_totalGameTime);
        _totalGameTimeText.text = totalTime.ToString();
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("GameResume");
    }
    
    
    
    
    
    
    
    
}
