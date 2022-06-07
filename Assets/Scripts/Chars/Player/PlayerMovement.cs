using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : Character
{
    [SerializeField] private float _distanceRaycast = 200f;
    
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _destination;

    #region AttackPrefabs

    [SerializeField] private GameObject _aaPrefab;
    [SerializeField] private GameObject _ballPrefab, _poisonPrefab;
    [SerializeField] private GameObject _AoePrefab;
    [SerializeField] private GameObject _ultiPrefab;
    
    #endregion
    
    #region Shield
    
    //Temporalmente
    [SerializeField] private GameObject _shield;
    private Collider _shieldCol;
    private MeshRenderer _shieldRender;
    
    #endregion
    
    #region CoolDownsAndUI

    private Image _abilityQ, _abilityW, _abilityE, _abilityR;
    private Image _healthBarMain;
    private float _coolDownQ, _coolDownW, _coolDownE, _coolDownR;
    
    private Image _castBar;
    [SerializeField] private bool _isGoingBase = false;
    
    private bool _isOnCoolDownQ, _isOnCoolDownW, _isOnCoolDownE, _isOnCoolDownR;
    
    #endregion

    [SerializeField] private GameObject _baseParticlePrefab;
    [SerializeField] private bool _isSafeZone;
    [SerializeField] private bool _isPlayerAlive;
    
    private void Awake()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        
        //HP BAR CHILDREN
        _barManagement = GetComponentInChildren<SpriteBarManagement>();
        
        //MAIN HP BAR REFEERENCE
        var mainHp = GameObject.FindWithTag(PaperConstants.TAG_MAIN_HPBAR);
        _healthBarMain = mainHp.GetComponent<Image>();

        var castBar = GameObject.FindWithTag(PaperConstants.TAG_CAST_BAR);
        _castBar = castBar.GetComponent<Image>();

        //ZONA COOLDOWN INIT/UI
        var icons = GameObject.FindGameObjectsWithTag(PaperConstants.TAG_ABILITIES_ICONS);

        _abilityQ = icons[0].GetComponent<Image>();
        _abilityW = icons[1].GetComponent<Image>();
        _abilityE = icons[2].GetComponent<Image>();
        _abilityR = icons[3].GetComponent<Image>();
        //END ZONA CD
        
        
        ///ZONA SHIELD
        _shield = GameObject.FindWithTag(PaperConstants.TAG_SHIELD);

        _shieldCol = _shield.GetComponent<Collider>();
        _shieldRender = _shield.GetComponent<MeshRenderer>();
        ///ENDZONA SHIELD

        _gm = FindObjectOfType<GameManager>();
        
        //ZONA NEW
        _navMesh = GetComponent<NavMeshAgent>();

    }
    
    private void Start()
    {
        _direction = Vector3.zero;
        _destination = transform.position;
        InitPlayer();
    }
    private void Update()
    {
        //No permitimos los controles si esta muerto
        if (!_isPlayerAlive) return;
        
        //Iría a base only.
        //if (_isGoingBase) return;
        
        //Boton izquierdo raton
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //LayerMask mask = LayerMask.GetMask("Ground");
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, _distanceRaycast))
            {
                //Si es enemigo, seleccionarlo ¿?
                if (hit.collider.CompareTag(PaperConstants.TAG_ENEMY))
                {
                    Debug.Log("Selecciono Enemy");
                }
                //Si es terreno, no hacer nada.

                if (hit.collider.CompareTag(PaperConstants.TAG_GROUND))
                {
                    Debug.Log("TERRENO CLICK IZQ");
                }
                
                //Si es objeto, informar qué es.
                //Si es player, mostrar info tmb.??
                
            }
        }
        
        
        
        //Boton derecho raton
        if (Input.GetMouseButtonDown(1))
        {
            _isGoingBase = false;
            _castBar.fillAmount = 0f;
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask(PaperConstants.LAYER_GROUND);
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, _distanceRaycast, mask))
            {
                //_isGoingBase = false;
                
                //Segun el tag nos moveremos/atacaremos o seleccionaremos algo.
                
                //Caso de movimiento cuando hit sea Ground/el terreno.
                //Debug.Log(hit.collider.CompareTag("Ground"));
                
                _destination = new Vector3(hit.point.x, 0f, hit.point.z);
                _direction = _destination - transform.position;

                if (CheckAngleNewDestination(transform.forward, _direction) > 45f)
                {
                    StopMove();
                    Debug.Log("SE APLICA STOP MOVE");
                    
                }
                
                
                //_direction = _destination - transform.position;
                //_direction.y = 0f;
                //_direction.Normalize();
                
                //DEBERIAMOS LEERPEARLA
                transform.LookAt(_destination);

                //StopMove();
                
                _navMesh.SetDestination(_destination);
                _anim.SetFloat("isMoving", 1f);

                //Caso de que sea enemigo
                //-- atacarle con basic attack teniendo en cuenta el rango?? Si no está a rango, avanzar hasta estar en rango.
            }
        }
        //_________________________________________________
        //Mechanical Test for Dmg
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakePhysicDamage(55);
            
            Debug.Log("Vida Actual: " + _currentHp);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var temp = Instantiate(_AoePrefab, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TestFinishGame();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _isGoingBase = true;
            
            StopMove();
            var particle = Instantiate(_baseParticlePrefab, transform.position, Quaternion.identity);
            
        }

        if (_isGoingBase)
        {
            _castBar.fillAmount += Time.deltaTime / 5; //quiero que sean 3 segundos aprox de casteo
            if (_castBar.fillAmount > 0.99f)
            {
                //var particle = Instantiate(_baseParticlePrefab, transform.position, Quaternion.identity);
                _isGoingBase = false;
                _castBar.fillAmount = 0f;
                RespawnBase();
                StopMove();
            }
        }
        
        ///SOLO PARA LA ANIMATION DE MOMENTO
        if (_navMesh.remainingDistance < 0.1f)
        {
            //StopMove();
            _anim.SetFloat("isMoving", 0f);
        }
        else
        {
            _anim.SetFloat("isMoving", 1f);
        }
        
        //ATTACK ZONE
        AttacksInputs();
        UtilityInput();
        
        //PORQUE EL VALOR BASE DE LA IMAGEN DE ENCIMA DEL CHARACTER ES 3 por eso * 0.33f.
        _healthBarMain.fillAmount = _barManagement.GetSizeBar();
    }

    private void InitPlayer()
    {
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

        _coolDownQ = PaperConstants.COOLDOWN_Q;
        _coolDownW = PaperConstants.COOLDOWN_W;
        _coolDownE = PaperConstants.COOLDOWN_E;
        _coolDownR = PaperConstants.COOLDOWN_R;
        
        
        //ShowCharacterInformation();
        
        _barManagement.InitializeBar(_generalStats.Health, PaperConstants.HP_BAR_FRIENDLY);
        
        _castBar.fillAmount = 0f;
        _currentSpeed = _generalStats.MoveSpeed;
        _shieldCol.enabled = false;
        _shieldRender.enabled = false;

        _isPlayerAlive = true;
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
        LayerMask mask = LayerMask.GetMask(PaperConstants.LAYER_GROUND);
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500f, mask))
        {
            
            var mousePosition = hit.point;
            
            transform.LookAt(hit.point);
            //_healthBar.transform.forward = _camera.transform.forward;
            
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
                temp.ProjectilNoTargetSetUp(dire, _generalStats.MissileSpeed, 50f, false);
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
                LayerMask mask = LayerMask.GetMask(PaperConstants.LAYER_GROUND);
                    
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
                temp.ProjectilNoTargetSetUp(transform.forward, _generalStats.MissileSpeed, 10, false);
            }
        }
    }

    public void StopMove()
    {
        //_destination = new Vector3(transform.position.x, 0f, transform.position.z);
        _navMesh.SetDestination(transform.position);
        _navMesh.velocity = Vector3.zero;
    }

    private float CheckAngleNewDestination(Vector3 currentDestination, Vector3 newDestination)
    {
        var angle = Vector3.Angle(currentDestination, newDestination);
        
        //Debug.Log(angle);
        
        return angle;
    }
    

    public bool isPlayerSafeZone()
    {
        return _isSafeZone;
    }

    public bool isPlayerAlive()
    {
        return _isPlayerAlive;
    }

    //se utiliza como evento en la animacion de la muerte del jugador!!!
    public void PlayerDeath()
    {
        _isPlayerAlive = false;
    }
    public void SetPlayerSafe(bool safe)
    {
        _isSafeZone = safe;
    }
    
    
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5f);
        _shieldCol.enabled = false;
        _shieldRender.enabled = false;
    }

    private IEnumerator CoolDown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
    }
    //Global cooldown ??

    public void TestFinishGame()
    {
        _gm.FinishGame();
    }

    private void RespawnBase()
    {
        transform.position = _gm._playerSpawnPoint.position;
        var temp = _camera.GetComponent<CameraMovement>();
        temp.Center();
        
    }
    
    
}
