using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _hScreenPercentage, _vScreenPercentage;

    [SerializeField] private float _angle;
    [SerializeField] private float _distance;
    
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
        if (mousePos.x < w * _hScreenPercentage){
            transform.position -= new Vector3(0f,0f,1f) * _movementSpeed * Time.deltaTime;
        }else if (mousePos.x > w - (w * _hScreenPercentage)){
                transform.position += new Vector3 (0f, 0f, 1f) * _movementSpeed * Time.deltaTime;
        }

        //Vertical
        if (mousePos.y < h * _vScreenPercentage){
            transform.position += new Vector3(1f, 0f, 0f) * _movementSpeed * Time.deltaTime;
        }else if (mousePos.y > h - (h * _vScreenPercentage)){
                transform.position -= new Vector3(1f, 0f, 0f) * _movementSpeed * Time.deltaTime;
        }
    }

    private void CenterAtPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");

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
        var temp = FindObjectOfType<PlayerMovement>();
        temp.HealthBarLookCamera(transform);
    }

}
