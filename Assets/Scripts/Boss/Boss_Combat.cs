using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Combat : Enemy
{
   
    [SerializeField] private GameObject _AoePrefab;
    [SerializeField] private GameObject _projectilPrefab;

    private float _aoe_timer = 0f;
    private float _aoe_timeBtwShoot = 2f;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _gm = FindObjectOfType<GameManager>();
        _barManagement = GetComponentInChildren<SpriteBarManagement>();

        _targetPos = _targetGO.transform;
    }
   

    public void Combat()
    {
        Debug.Log("PIU");
    }

    public override void Movement(){
        Debug.Log("MOVIMIENTO COMBATE");

        _aoe_timer += Time.deltaTime;
        if (_aoe_timer > _aoe_timeBtwShoot){
            //Range_AoE();
            ShootAoE();
            _aoe_timer = 0f;
        }

    }

    //HAY QUE PLANTEAR EL ASUNTO BIEN
    public void Shoot(){

    }

    private void Range_AoE(){
        if (_targetPos){
            if (_AoePrefab){
                Instantiate(_AoePrefab, _targetPos.position + new Vector3(0f, 0.2f, 0.2f), Quaternion.identity);
            }
        }
    }

    private void ShootAoE(){
        var rot = 0;
        
        for (int i = 0; i < 8; i++){
            var projGO = Instantiate(_projectilPrefab, transform.position, Quaternion.Euler(0, rot,0));
            var proj = projGO.GetComponent<Projectil>();
            

            var dire = projGO.transform.forward;

            //DEBERIA ESTAR EN RADIANES!!! 2PI = 360
            //var dire = new Vector3( Mathf.Cos(45º),0f,Mathf.Sin(30º));
            proj.ProjectilNoTargetSetUp(dire, 8f, 10f, true);
            rot += 45;
        }
    }

    private void Shoot3ProjectilesWithTarget(){

    }
    
}
