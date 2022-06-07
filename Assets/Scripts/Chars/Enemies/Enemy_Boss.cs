using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Boss : Enemy
{
    /// <Skills>
    /// - bolitas aoe
    /// - meteoritos
    /// - Zonas dañinas
    /// - teledirigidos
    /// - animaciones
    /// 
    /// </Skills>
    /// <returns></returns>

    [Header("Prefabs")] [SerializeField] private GameObject _balls, _meteo;

    private void Awake()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        //_gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        NormalSetUp("BOSS", 1000, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 10, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        
        //ShowCharacterInformation();
        
        //HpBarUpdate();
        
        SetSpawnPoint();
        //AggroHPBarColor(Color.yellow);
    }
    

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    
    
    
    
}
