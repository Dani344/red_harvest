using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaUImanager : MonoBehaviour
{
    private UI_Manager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UI_Manager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //uiManager._uiEvents._changeHealthPlayer?.Invoke(100);
            //_changeHealthPlayer?.Invoke(100);n
            
        }
    }

}
