﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeMenu : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        Debug.Log("Vamos al menu principal");
        SceneManager.LoadScene("MainMenu");
    }

    public void OtroButton()
    {
        Debug.Log("RANDOM");
    }
}
