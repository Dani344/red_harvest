using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //FALTA IMPLEMENTAR EL TEXTMESH PRO
    //FALTA CREAR LAS COINS Y EL CANVAS
    //FALTA HACER EL COMPORTAMIENTO DE LA ZONA
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

    [SerializeField] private int _playerCoins;
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
    
    
}
