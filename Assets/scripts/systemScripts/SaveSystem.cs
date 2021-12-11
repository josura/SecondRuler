using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem {

    public static bool newPlayer;
    public static string playerName;
    public static bool fullscreen;

	public static void save()
    {
        PlayerManager.Instance.saveStats();
        scoreManager.Instance.saveScores();
        //altri save
    }

    public static void load()
    {

        //PlayerManager.Instance.loadStats();
        //ShootersManager.Instance.resumeUpgrades();
        //pauseManager.Instance.loadPrices();
        //scoreManager.Instance.loadScores();
        //delegate load in PlayerManager that does the above operations
        PlayerManager.Instance.loadDel();
    }


    public static void playGame()
    {
        if (newPlayer)
        {
            PlayerManager.Instance.stats.playerName = playerName;
            scoreManager.Instance.loadScores();
        }
        else
        {
            load();
        }
    }

    public static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public static void SaveSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("MiscVolume", volume);
        PlayerPrefs.Save();
    }

    public static void SaveWidth(int width)
    {
        PlayerPrefs.SetInt("WIDTH", width);
        PlayerPrefs.Save();
    }

    public static int GetWidth()
    {
        return PlayerPrefs.GetInt("WIDTH", 1920);
    }

    public static void SaveLevelProgress(int level)
    {
        PlayerPrefs.SetInt("LEVEL", level);
        PlayerPrefs.Save();
    }

    public static void SaveHeight(int height)
    {
        PlayerPrefs.SetInt("HEIGHT", height);
        PlayerPrefs.Save();
    }

    public static int GetHeight()
    {
        return PlayerPrefs.GetInt("HEIGHT", 1280);
    }

    public static void SaveFullscreen(int fullscreen)
    {
        PlayerPrefs.SetInt("FULLSCREEN", fullscreen);
        PlayerPrefs.Save();
    }

    public static int GetFullscreen()
    {
        return PlayerPrefs.GetInt("FULLSCREEN",1);
    }
}
