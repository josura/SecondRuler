using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lostManager : MonoBehaviour {

    [SerializeField] GameObject endGUI;

    private static lostManager _instance;

    public static bool isGameEnded;
    [SerializeField] bool esterno;
    public static lostManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes
        pauseManager.isGamePaused = false;
        isGameEnded = false;
        
        endGUI.SetActive(false);
    }

    public void endGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        pauseManager.isGamePaused = true;
        isGameEnded = true;
        endGUI.SetActive(true);
        scoreManager.Instance.displayScoreboard();
    }
    private void resume()
    {
        endGUI.SetActive(false);
        pauseManager.isGamePaused = false;
        isGameEnded = false;
        Time.timeScale = 1;
    }
    public void resumeAfterLost()
    {
        PlayerManager.Instance.resetPos();
        PlayerManager.Instance.resetHealth();
        EnemyManager.Instance.resetAll();
        resume();
    }

    public void loadSavePoint()
    {
        if (!SaveSystem.newPlayer)
        {
            SaveSystem.load();
            resume();
        }
    }
}
