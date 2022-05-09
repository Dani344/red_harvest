using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : Character
{
    //private Camera _camera;
    [SerializeField] private float _distanceRaycast = 200f;
    
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _destination;
    //[SerializeField] private Rigidbody _rb;

    #region Attack
    
    [SerializeField] private GameObject _ballPrefab, _poisonPrefab;
    [SerializeField] private GameObject _ultiPrefab;
    
    #endregion

    //Temporalmente
    [SerializeField] private GameObject _shield;
    [SerializeField] private Collider _shieldCol;
    [SerializeField] private MeshRenderer _shieldRender;

    #region CoolDownsAndUI

    [SerializeField] private Image _abilityQ, _abilityW, _abilityE, _abilityR;
    [SerializeField] private float _coolDownQ, _coolDownW, _coolDownE, _coolDownR;

    private bool _isOnCoolDownQ, _isOnCoolDownW, _isOnCoolDownE, _isOnCoolDownR;
    
    #endregion
    
    //private Transform _healthTransIni;
    private void Awake()
    {
       _camera = Camera.main;
        
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _healthBar = GetComponentInChildren<SpriteRenderer>();

        //ZONA COOLDOWN INIT/UI
        var icons = GameObject.FindGameObjectsWithTag("AbilitiesIcons");

        _abilityQ = icons[0].GetComponent<Image>();
        _abilityW = icons[1].GetComponent<Image>();
        _abilityE = icons[2].GetComponent<Image>();
        _abilityR = icons[3].GetComponent<Image>();
        //END ZONA CD
        
        
        ///ZONA SHIELD
        _shield = GameObject.FindWithTag("Shield");
        _shieldCol = _shield.GetComponent<Collider>();
        _shieldRender = _shield.GetComponent<MeshRenderer>();
        ///ENDZONA SHIELD

    }
    
    private void Start()
    {
        _direction = Vector3.zero;
        _destination = transform.position;
        
        //------ABILITIES-----
        _abilityQ.fillAmount = 0f;
        _abilityW.fillAmount = 0f;
        _abilityE.fillAmount = 0f;
        _abilityR.fillAmount = 0f;

        _isOnCoolDownQ = false;
        
        //!!!!!//
        
        
        //this.SetUpDefaultForTest();
        this.NormalSetUp("DK", 500, 300,5.5f,11f, 25f,30f,
            56f, 1.75f, 450, 0, 4,
            0.9f, 0.15f, 0f, 0f, 10, 1, 100, 0);
        
        ShowCharacterInformation();

        _hpBarTextNumber.text = _currentHp + " / " + _generalStats.Health;

        _currentSpeed = _generalStats.MoveSpeed;
        _shieldCol.enabled = false;
        _shieldRender.enabled = false;
    }
    private void Update()
    {
        //Boton izquierdo raton
        if (Input.GetMouseButtonDown(0))
        {
            //Tirar raycast
            //Si es enemigo, seleccionarlo ¿?
            //Si es terreno, no hacer nada.
            //Si es objeto, informar qué es.
            //Si es player, mostrar info tmb.??
            Debug.Log("FALTA PROGRAMAR ESTE BOTON");
        }
        
        //Boton derecho raton
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ground");
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, _distanceRaycast, mask))
            {
                
                //Segun el tag nos moveremos/atacaremos o seleccionaremos algo.
                
                //Caso de movimiento cuando hit sea Ground/el terreno.
                //Debug.Log(hit.collider.CompareTag("Ground"));
                _destination = new Vector3(hit.point.x, 0f, hit.point.z);
                _direction = _destination - transform.position;
                _direction.y = 0f;
                _direction.Normalize();
                transform.LookAt(_destination);
                
                
                //Controlar que solo mire a la camara una vez se haya centrado.
                _healthBar.transform.forward = _camera.transform.forward;


                //Caso de que sea enemigo
                //-- atacarle con basic attack teniendo en cuenta el rango?? Si no está a rango, avanzar hasta estar en rango.
            }
        }
        
        //Mechanical Test for Dmg
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakePhysicDamage(55);
            
            Debug.Log("Vida Actual: " + _currentHp);
        }
        
        
        //Comprobamos si ha llegado al destino del click
        if (DistanceToPoint(_destination) > 0.05f)
        {
            transform.position += new Vector3(_direction.x * _currentSpeed * Time.deltaTime, 0f, _direction.z * _currentSpeed * Time.deltaTime);
            _anim.SetFloat("isMoving", 1f);
        }else{
            //Hacer un lerp en caso de necesidad
            _anim.SetFloat("isMoving", 0f);
        }

        //ATTACK ZONE
        AttacksInputs();
        UtilityInput();
    }

    private float DistanceToPoint(Vector3 destination)
    {
        var temp = destination - transform.position;
        var distance = temp.magnitude;
        return distance;
    }

    private void UtilityInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopMove();
        }
        
        //Mapa con M
        
        //AutoAttack con A
        
        //Flash con F
        
        //MovementSpeed con D
    }
    
    //Permitir instaHechizo o previsualizar.
    private void AttacksInputs(){
        if (Input.GetKeyDown(KeyCode.Q)){
            AbilityQ();
        }

        if (Input.GetKeyDown(KeyCode.W)){
            AbilityW();
        }
        //Controlar si está activo o no.
        if (Input.GetKeyDown(KeyCode.E)){
            AbilityE();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            AbilityR();
        }

        //---COOLDOWNS MANAGEMENT---
        //HACER UNA CLASE GLOBAL QUE PASEMOS Q CD y EL BOoLEANO
        if (_isOnCoolDownQ)
        {
            _abilityQ.fillAmount -= 1 / _coolDownQ * Time.deltaTime;
            if (_abilityQ.fillAmount <= 0)
            {
                _abilityQ.fillAmount = 0f;
                _isOnCoolDownQ = false;
            }
        }
        
        if (_isOnCoolDownW)
        {
            _abilityW.fillAmount -= 1 / _coolDownW * Time.deltaTime;
            if (_abilityW.fillAmount <= 0)
            {
                _abilityW.fillAmount = 0f;
                _isOnCoolDownW = false;
            }
        }
        
        if (_isOnCoolDownE)
        {
            _abilityE.fillAmount -= 1 / _coolDownE * Time.deltaTime;
            if (_abilityE.fillAmount <= 0)
            {
                _abilityE.fillAmount = 0f;
                _isOnCoolDownE = false;
            }
        }
        
        if (_isOnCoolDownR)
        {
            _abilityR.fillAmount -= 1 / _coolDownR * Time.deltaTime;
            if (_abilityR.fillAmount <= 0)
            {
                _abilityR.fillAmount = 0f;
                _isOnCoolDownR = false;
            }
        }

    }

    private Vector3 AbilityDirection()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500f, mask))
        {
            
            var mousePosition = hit.point;
            
            transform.LookAt(hit.point);
            _healthBar.transform.forward = _camera.transform.forward;
            
            var abilityDire = mousePosition - transform.position;
            return abilityDire;
        }
        else
        {
            return Vector3.zero;
            Debug.Log("Error con algun lanzamiento");
        }
    }
    private void AbilityQ()
    {
        Debug.Log("Q Attack");
        if (_isOnCoolDownQ == false)
        {
            _isOnCoolDownQ = true;
            _abilityQ.fillAmount = 1f;
            if (_ballPrefab)
            {
                var spellQ = Instantiate(_ballPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                var temp = spellQ.GetComponent<Projectil>();
            
                //Falta calibrar el daño que enviamos al projectil
                var dire = AbilityDirection();
                StopMove();
                temp.ProjectilSetUp(dire, _generalStats.MissileSpeed, 50f);
                //temp.ProjectilSetUp(transform.forward, _generalStats.MissileSpeed, 50f);
                
            }
        }
        
    }

    private void AbilityW()
    {
        Debug.Log("W Attack");
        if (_isOnCoolDownW == false)
        {
            _isOnCoolDownW = true;
            _abilityW.fillAmount = 1f;
            if (_poisonPrefab)
            {
                RaycastHit hit;
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("Ground");
                    
                if (Physics.Raycast(ray, out hit, _distanceRaycast, mask))
                {
                    var pos = hit.point;
                    Instantiate(_poisonPrefab, pos, Quaternion.identity);
                }
            }
        }
    }

    private void AbilityE()
    {
        Debug.Log("E Shield");
        if (_isOnCoolDownE == false)
        {
            _isOnCoolDownE = true;
            _abilityE.fillAmount = 1f;
            _shieldCol.enabled = true;
            _shieldRender.enabled = true;
            StartCoroutine(CoolDown());
        }
    }

    private void AbilityR()
    {
        Debug.Log("R Attack");
        if (_isOnCoolDownR == false)
        {
            _isOnCoolDownR = true;
            _abilityR.fillAmount = 1f;
            if (_ultiPrefab)
            {
                var playerRot = transform.rotation;
                var ulti = Instantiate(_ultiPrefab, transform.position + Vector3.up/2f, playerRot);
                var temp = ulti.GetComponent<Projectil>();
            
                //Falta gestionar el daño que le enviaremos al projectil
                temp.ProjectilSetUp(transform.forward, _generalStats.MissileSpeed, 10);
            }
        }
    }

    private void StopMove()
    {
        _destination = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public void HealthBarLookCamera(Transform camTrans)
    {
        _healthBar.transform.LookAt(camTrans);
        //_healthTransIni = _healthBar.GetComponent<Transform>();
    }
    
    private IEnumerator CoolDown()
    {
        Debug.Log("CUENTA ATRAS INICIADA");
        yield return new WaitForSeconds(5f);
        _shieldCol.enabled = false;
        _shieldRender.enabled = false;
    }

    private IEnumerator CoolDown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
    }
    //Global cooldown ??
    
    
    
}
