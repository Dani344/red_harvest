using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    //TEMPORALMENTE
    //IMPLEMENTAR CODIGO PARA HACERLO CORRECTAMENTE POSTERIORMENTE
    public void PlayButton()
    {
        SceneManager.LoadScene(PaperConstants.SCENE_PLAYING);
    }

    public void QuitButton()
    {
        //Debug.Log("QUEREMOS SALIR DEL JUEGO");
        Application.Quit();
    }
    
}
