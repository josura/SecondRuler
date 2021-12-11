using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour {

    [SerializeField] Slider musicSlider, soundSlider;
    [SerializeField] Toggle fullScreen;
    [SerializeField] InputField newPlayer;
    [SerializeField] InputField width, height;

    [SerializeField] GameObject optionsScreen, menuScreen, newgameScreen;

    [SerializeField] Text up, down, left, right, bullet, rocket;

    [SerializeField] GameObject[] tutorialImages;
    int currentTutorial;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        width.text = SaveSystem.GetWidth().ToString();
        height.text = SaveSystem.GetHeight().ToString();
        Screen.SetResolution(SaveSystem.GetWidth(), SaveSystem.GetHeight(), System.Convert.ToBoolean(SaveSystem.GetFullscreen()));

        currentTutorial = 0;

        soundSlider.value = PlayerPrefs.GetFloat("MiscVolume", 0.5f); ;
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f); ;
    }

    private void LateUpdate()
    {
        up.text = KeyBindManager.Instance.keyBindings["UP"].ToString();
        down.text = KeyBindManager.Instance.keyBindings["DOWN"].ToString();
        left.text = KeyBindManager.Instance.keyBindings["LEFT"].ToString();
        right.text = KeyBindManager.Instance.keyBindings["RIGHT"].ToString();
        bullet.text = KeyBindManager.Instance.keyBindings["FIREBULLET"].ToString();
        rocket.text = KeyBindManager.Instance.keyBindings["FIREROCKET"].ToString();
    }


    public void playNewGame(int i)
    {
        //SceneManager.LoadScene(i);
        SaveSystem.newPlayer = true;
        SaveSystem.playerName = newPlayer.text;
        AsyncOperation p = SceneManager.LoadSceneAsync(i);


    }

    public void SaveSoundVolume()
    {
        SaveSystem.SaveSoundVolume(soundSlider.value);
    }

    public void SaveMusicVolume()
    {
        SaveSystem.SaveMusicVolume(musicSlider.value);
    }

    public void SetResolution()
    {
        SaveSystem.SaveWidth(System.Convert.ToInt32(width.text));
        SaveSystem.SaveHeight(System.Convert.ToInt32(height.text));
        SaveSystem.SaveFullscreen(System.Convert.ToInt32(fullScreen.isOn));
    }

    public void resumeGame(int i)
    {
        if (File.Exists(Application.persistentDataPath + "/playerStats.stats"))
        {
            SaveSystem.newPlayer = false;
            AsyncOperation p = SceneManager.LoadSceneAsync(i);
        }
        else
        {
            menuScreen.SetActive(false);
            newgameScreen.SetActive(true);
        }
        
    }

    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    

    public void NextTutorial()
    {
        tutorialImages[currentTutorial].SetActive(false);
        if (currentTutorial < tutorialImages.Length - 1)
        {
            currentTutorial++;
            tutorialImages[currentTutorial].SetActive(true);
        }
        else if (currentTutorial == tutorialImages.Length - 1)
        {
            currentTutorial = 0;
            tutorialImages[currentTutorial].SetActive(true);
        }
    }

    public void keyBindOnClick(string key)
    {
        KeyBindManager.Instance.keyBindOnClick(key);
    }

}
