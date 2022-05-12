using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    
    [SerializeField] private GameObject _boss;
    #endregion

    #region PlayerInterfaceInfo

    [SerializeField] private int _totalPlayerCoins;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private bool _playerDead;

    #endregion

    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMovement>();
        
    }
    
    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();

        var temp = "Coins: 0";
        _coinText.text = temp;
        _totalPlayerCoins = 0;
    }
    
    /*
    private void Update()
    {
        
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
                
            }
            
        }
    }

    public void RecolectCoin(int ammount)
    {
        _totalPlayerCoins += ammount;
        var newCoinsText = "Coins: " + _totalPlayerCoins;
        _coinText.text = newCoinsText;
    }
    
    
}
