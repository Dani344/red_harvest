
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //FALTA GESTIONAR QUE LOS ENEMIGOS SEPAN QUE EL PLAYER SE MUERE

    //FALTA GESTIONAR EL ARRAY DE ENEMIGOS
    //FALTA CREAR UN CONTADOR DE MAPA??
    
    
    //CREAR LAS FUNCIONES NECESARIAS PUBLICAS Y PENSAR EN EL ESQUEMA
    
    [SerializeField] private CameraMovement _cameraMove;
    [SerializeField] private PlayerMovement _playerScript;
    [SerializeField] private ControlStateBoss _controlStateBoss;
    
    #region Prefabs
    [SerializeField] private GameObject _map;

    [SerializeField] private GameObject _playerGO;
    
    [SerializeField] public Transform _playerSpawnPoint;

    [SerializeField] private GameObject[] _enemiesSpawns;
    [SerializeField] private GameObject[] _enemiesPrefabs;
    
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform _bossSpawn;
    #endregion

    #region PlayerInterfaceInfo

    [SerializeField] private int _totalPlayerCoins;
    [SerializeField] private float _inGameProgress = 0f;
    //[SerializeField] private bool _playerDead;
    [SerializeField] private TMP_Text _totalGameTimeText;
    [SerializeField] private int _monolitesActivated = 0;

    #endregion

    [SerializeField] private int _totalNumEnemies = 0;
    [SerializeField] private int _currentNumEnemies = 0;
    

    private float _totalGameTime = 0f;
    private float _count = 0f;

    [SerializeField] private UI_Manager _uiManager;
    
    
    
    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMovement>();
        _uiManager = FindObjectOfType<UI_Manager>();

    }
    
    private void Start()
    {
        _totalNumEnemies = 0;
        _currentNumEnemies = 0;
        
        _totalPlayerCoins = 0;
        _inGameProgress = 0f;
        _totalGameTime = 0f;
        _count = 0f;

        _monolitesActivated = 0;
        
        SpawnPlayer();
        SpawnEnemies();
        //SpawnBoss();
        
        ProgressInGame();
    }
    
    //CONTADOR TIMER TOTAL
    private void Update()
    {
        _count += Time.deltaTime;
        if (_count > 1f)
        {
            _totalGameTime += _count;
            _count = 0f;
            TimerGameText();
        }
        
        TestGM();
        
        if (!_playerScript.isPlayerAlive())
        {
            Debug.Log("MUERTO GM");
            _controlStateBoss.PlayerDied();
        }
        
    }

    private void TestGM()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpawnBoss();
        }
    }
    
    private void SpawnPlayer()
    {
        var player = Instantiate(_playerGO, _playerSpawnPoint.position, Quaternion.identity);
        _cameraMove.Center();
        _playerScript = player.GetComponent<PlayerMovement>();
        _playerScript.SetPlayerAlive();
    }
    
    private void SpawnEnemies()
    {
        var temp = GameObject.FindGameObjectsWithTag(PaperConstants.TAG_SPAWN_ENEMY);
        _enemiesSpawns = temp;

        for (int i = 0; i < _enemiesSpawns.Length; i++)
        {
            var enemigo = Random.Range(0, 3);
            if (_enemiesPrefabs[enemigo])
            {
                //TENER EN CUENTA SI EL SETUP LO HACEMOS AQUI SI QUEREMOS ALGO ESPECIAL??
                Instantiate(_enemiesPrefabs[enemigo], _enemiesSpawns[i].transform.position, Quaternion.identity);
                _totalNumEnemies += 1;
            }
            
        }
        //Instantiate(_enemiesPrefabs[1], )
        
        _currentNumEnemies = _totalNumEnemies;
    }

    private void SpawnBoss()
    {
        if (_bossPrefab)
        {
            
            var boss = Instantiate(_bossPrefab, _bossSpawn.position, Quaternion.identity);
            boss.transform.localScale = new Vector3(2f, 2f, 2f);
            boss.name = "Boss";
            _controlStateBoss = boss.GetComponent<ControlStateBoss>();

        }
    }
    public void RecolectCoin(int ammount)
    {
        _totalPlayerCoins += ammount;
        _uiManager._uiEvents._changeTotalCoins?.Invoke(_totalPlayerCoins);
        //var newCoinsText = "Coins: " + _totalPlayerCoins;
        //_coinText.text = newCoinsText;
    }

    public void ProgressInGame()
    {
        //Debug.Log(_currentNumEnemies + "CURRENT E");
        var porcentajeRestante = (float) _currentNumEnemies / _totalNumEnemies;
        _inGameProgress = (1f - porcentajeRestante) * 100f;
        
        _uiManager._uiEvents._changeTotalProgress?.Invoke(_inGameProgress);
        
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
        SceneManager.LoadScene(PaperConstants.SCENE_RESUME);
    }

    public void MonoliteActivated()
    {
        _monolitesActivated += 1;
        if (_monolitesActivated == 3)
        {
            SpawnBoss();
        }
        _uiManager._uiEvents._monoliteActivated?.Invoke(_monolitesActivated);
        
    }

    public int GetMonolitesActives()
    {
        return _monolitesActivated;
    }
    
    
    
    
    
    
    
}
