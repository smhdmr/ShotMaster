using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GameOverPanelSet(bool isOpen = true)
    {
        gameOverPanel.SetActive(isOpen);
    }

    public void OnClickRestart()
    {
        GameOverPanelSet(false);
        LevelManager.Instance.RestartGame();         
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ClearPoints()
    {

    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickJoinUs()
    {
        Application.OpenURL("https://discord.gg/zazeUdYbzX");
    }
}
