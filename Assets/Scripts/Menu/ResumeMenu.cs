using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _victDefText;
    [SerializeField] private TMP_Text _textInfo;
    [SerializeField] private Image _backgroundVictory;
    [SerializeField] private Image _backgroundDefeat;

    private void Start()
    {
        //Player prefs info
        var victText = "DEFEAT";
        var info = "Coins: 69" + "Enemies Killed: 96";

        _victDefText.text = victText;
        _textInfo.text = info;

        _backgroundDefeat.color = new Color(255f, 107f, 107f, 230f);
        //_backgroundVictory.color = new Color(192f, 241f, 192f, 255f);
    }
    
    
    public void ReturnMainMenu()
    {
        Debug.Log("Vamos al menu principal");
        SceneManager.LoadScene(PaperConstants.SCENE_MAIN_MENU);
    }

    public void OtroButton()
    {
        Debug.Log("RANDOM");
    }
}
