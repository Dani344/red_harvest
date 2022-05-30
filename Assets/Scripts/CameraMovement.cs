using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _hScreenPercentage, _vScreenPercentage;

    [SerializeField] private float _angle;
    [SerializeField] private float _distance;

    private float _distanceMin = 5f;
    private float _distanceMax = 20f; 
    
    
    
    // private void Awake(){
        
    // }

    //FALTARIA ACTIVARLO CUANDO EMPIEZE LA PARTIDA PARA QUE SE CENTRE BIEN. Pero esperar
    //a que se cree el player desde el gm.
    private void Start(){
        //CenterAtPlayer();
    }

    private void Update(){
        if (Input.GetKey(KeyCode.Space)){
            CenterAtPlayer();
        }
        MoveCamera();
        
        var scrollMouse = Input.mouseScrollDelta;

        if (scrollMouse.y < 0f)
        {
            _distance += Time.deltaTime * PaperConstants.ZOOM_CAMERA_SENSIVITY;
        }
        else if (scrollMouse.y > 0f)
        {
            _distance -= Time.deltaTime * PaperConstants.ZOOM_CAMERA_SENSIVITY;
        }

        _distance = Mathf.Clamp(_distance, PaperConstants.ZOOM_CAMERA_MIN_DISTANCE, PaperConstants.ZOOM_CAMERA_MAX_DISTANCE);
        
        
        //Debug.Log(_distance);
        
    }
    // private void FixedUpdate(){
    //     //MoveCamera();
    // }


    private void MoveCamera(){
        Vector3 mousePos = Input.mousePosition;
        int w = Screen.currentResolution.width;
        int h = Screen.currentResolution.height;

        //Debug.Log(mousePos.x + "," + mousePos.y);
        
        //Horizontal
        if (mousePos.x < w * PaperConstants.HIGH_SCREEN_PERCENTAGE){
            transform.position -=  Vector3.forward * _movementSpeed * Time.deltaTime;
        }else if (mousePos.x > w - (w * PaperConstants.HIGH_SCREEN_PERCENTAGE)){
                transform.position += Vector3.forward * _movementSpeed * Time.deltaTime;
        }

        //Vertical
        if (mousePos.y < h * PaperConstants.VERTICAL_SCREEN_PERCENTAGE){
            transform.position += Vector3.right * _movementSpeed * Time.deltaTime;
        }else if (mousePos.y > h - (h * PaperConstants.VERTICAL_SCREEN_PERCENTAGE)){
                transform.position -= Vector3.right * _movementSpeed * Time.deltaTime;
        }
    }

    private void CenterAtPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag(PaperConstants.TAG_PLAYER);

        float angleRad = Mathf.Deg2Rad * (90 - _angle);

        float y = Mathf.Cos(angleRad) * _distance;
        float x = Mathf.Sin(angleRad) * _distance;

        float h = _distance / Mathf.Sqrt(2);
        transform.position = player.transform.position + new Vector3(x, y, 0);
        transform.LookAt(player.transform);

    }

    public void Center()
    {
        CenterAtPlayer();
        //var temp = FindObjectOfType<PlayerMovement>();
        //temp.HealthBarLookCamera(transform);
    }

}
