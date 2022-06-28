using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Combat : Enemy
{
   
    [SerializeField] private GameObject _AoePrefab;
    [SerializeField] private GameObject _projectilPrefab;
    [SerializeField] private GameObject _shootPrefab;

    [SerializeField] private Enemy _bp;
    

    private float _aoe_timer = 0f;
    private float _aoe_timeBtwShoot = 2f;

    private float _shoot_aoe_timer = 0f;

    private float _triple_timer = 0f;
    private float _shootBTWtriple = 0.5f;

    private int tripleShootNum = 0;

    
    private void Start()
    {
        _bp = GetComponent<Boss_Patrol>();
        
        tripleShootNum = 0;
        _targetGO = _bp.GetTargetGO();
        _targetPos = _targetGO.transform;
        this.GenerateStatsEmpty();
    }


    public override void Movement(){
        
        RefreshStats();
        _targetPos = _targetGO.transform;
        CombatManagement();
        
    }

    private void TripleShoot()
    {
        if (_targetPos)
        {
            if (_shootPrefab)
            {
                //Frontal
                var projGO = Instantiate(_shootPrefab, transform.position, Quaternion.identity);
                var proj = projGO.GetComponent<Projectil>();
                var dire = _targetPos.position - transform.position;
                //Debug.Log("PROJ1: " + dire);
                //dire.Normalize();
                
                proj.ProjectilTargetSetUp(dire, 3f, 70f, 3f, true, _targetPos);
                
                /*
                //DERECHO 30º
                projGO = Instantiate(_shootPrefab, transform.position + new Vector3(2f,0f,0f), Quaternion.identity);
                proj = projGO.GetComponent<Projectil>();

                //dire = new Vector3(dire.x * Mathf.Sin(Mathf.PI / 4f), dire.y, dire.z * Mathf.Cos(Mathf.PI / 4f));
                //Debug.Log("PROJ2: " + dire);
                proj.ProjectilTargetSetUp(dire, 5f, 40f, 3f, true, _targetPos);
                
                //IZQUIERDO -30º
                projGO = Instantiate(_shootPrefab, transform.position +new Vector3(-2f,0f,0f), Quaternion.identity);
                proj = projGO.GetComponent<Projectil>();
                
                //dire = new Vector3(-dire.x, dire.y, -dire.z);
                //Debug.Log("PROJ3: " + dire);
                proj.ProjectilTargetSetUp(dire, 5f, 40f, 3f, true, _targetPos);
                */
            }
        }
    }
    private void Range_AoE(){
        
        if (_targetPos){
            if (_AoePrefab){
                Instantiate(_AoePrefab, _targetPos.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
            }
        }
    }

    private void ShootAoE(){
        var rot = 0;
        
        for (int i = 0; i < 8; i++){
            var projGO = Instantiate(_projectilPrefab, transform.position , Quaternion.Euler(0, rot,0));
            var proj = projGO.GetComponent<Projectil>();
            

            var dire = projGO.transform.forward;

            //DEBERIA ESTAR EN RADIANES!!! 2PI = 360
            //var dire = new Vector3( Mathf.Cos(45º),0f,Mathf.Sin(30º));
            proj.ProjectilNoTargetSetUp(dire, 8f, 45f, true);
            rot += 45;
        }
    }

    private void CombatManagement()
    {
        _aoe_timer += Time.deltaTime;
        _shoot_aoe_timer += Time.deltaTime;
        _triple_timer += Time.deltaTime;
        if (_aoe_timer > _aoe_timeBtwShoot){
            Range_AoE();
            _aoe_timer = 0f;
        }

        if (_shoot_aoe_timer > 1.5f * _aoe_timeBtwShoot)
        {
            ShootAoE();
            _shoot_aoe_timer = 0f;
        }

        if (_triple_timer > 1.5f * _aoe_timeBtwShoot)
        {
            _shootBTWtriple += Time.deltaTime;
            if (_shootBTWtriple > 1f)
            {
                _shootBTWtriple = 0f;
                tripleShootNum++;
                TripleShoot();
                
                if (tripleShootNum == 3)
                {
                    _triple_timer = 0f;
                    tripleShootNum = 0;
                }
            }
            
            
        }
        
    }

    public void RefreshStats(){
        //this.GenerateStatsEmpty();
        var stats = _bp.GetGeneralStats();
        this.RefreshGeneralStats(stats);
        //this._generalStats
    }
    
}
