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
    
    //private Color _defeatColor = new Color(255f, 107f, 107f);
    private Color _victoryColor = new Color(192, 241, 192);

    private void Start()
    {
        var victText = "VICTORY";
        
        if (PlayerPrefs.GetInt(PaperConstants.PLAYER_PREFS_RESUME_GAME) == 0)
        {
            victText = "DEFEAT";

            _backgroundVictory.enabled = false;
            //_backgroundDefeat.color = _defeatColor;
            //_backgroundDefeat.color = new Color(255f, 107f, 107f);

        }
        else
        {
            //_backgroundVictory.color = _victoryColor;
            //_backgroundVictory.color = new Color(192f, 241f, 192f);
            _backgroundDefeat.enabled = false;
            //StartCoroutine(nameof(MuestraImagen));

        }
        
        var info = "Coins: " + PlayerPrefs.GetInt(PaperConstants.PLAYER_PREFS_TOTAL_COINS) + 
                   "\nEnemies Killed: " + PlayerPrefs.GetInt(PaperConstants.PLAYER_PREFS_ENEMIES_KILLED);

        _victDefText.text = victText;
        _textInfo.text = info;

        //_backgroundDefeat.color = new Color(255f, 107f, 107f, 230f);
        //_backgroundVictory.color = new Color(192f, 241f, 192f, 255f);
    }
    
    
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(PaperConstants.SCENE_MAIN_MENU);
    }

    private IEnumerator MuestraImagen()
    {
        yield return new WaitForSeconds(2f);
        
        _backgroundVictory.color = _victoryColor;
        
        Debug.Log(_backgroundVictory.color);
        yield return null;
    }
    
}
