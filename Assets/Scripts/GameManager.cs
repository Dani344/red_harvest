using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private CameraMovement _cameraMove;
    
    
    #region Prefabs

    [SerializeField] private GameObject _map;
    
    
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemies;
    [SerializeField] private GameObject _boss;
    
    #endregion

    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMovement>();
    }
    
    private void Start()
    {
        SpawnPlayer();
    }
    
    private void Update()
    {
        
    }



    private void GenerateMap()
    {
        
    }

    private void SpawnPlayer()
    {
        Instantiate(_player, _spawnPoint.position, Quaternion.identity);
        _cameraMove.Center();

    }
}
