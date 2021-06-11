using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Instance
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject gameOverPanel;
    public TMP_Text pointText;


    public void GameOverPanelSet(bool isVisible = true)
    {
        gameOverPanel.SetActive(isVisible);
        pointText.gameObject.SetActive(!isVisible);        
    }

    public void OnClickRestart()
    {
        GameOverPanelSet(false);        
        LevelManager.Instance.RestartGame();         
    }

    public void OnClickHome()
    {
        Global.isGameStarted = false;    
        Global.isPlayable = true;
        Global.gameDir = Global.GameDirection.toLeft;
        Global.score = 0;

        SceneManager.LoadScene(0);
    }

    public void ClearPoints()
    {
        pointText.text = "0";
        Global.score = 0;
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickJoinUs()
    {
        Application.OpenURL("https://discord.gg/zazeUdYbzX");
    }

    public void SetPoint(int point)
    {
        pointText.text = point.ToString();
    }

    public void SetPointsVisibility(bool isVisible = true)
    {
        pointText.gameObject.SetActive(isVisible);
    }
}
