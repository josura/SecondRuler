using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance;
    private AudioSource musicsource, enemysource,batonSource, playerSource,playershootSourceBullet, playershootSourceLaser, enemyShooterSource;
    [SerializeField] AudioClip soundTrack, playerAttack, batonHit, laser,bulletShooter,rocketShooter,enemyShooter, lost;
    [SerializeField] AudioClip[] enemyDestroy, enemyHit;

    public float musicVolume, miscVolume;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        musicsource = gameObject.AddComponent<AudioSource>();
        musicsource.loop = true;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        miscVolume = PlayerPrefs.GetFloat("MiscVolume", 0.5f);

        musicsource.volume = musicVolume;
        musicsource.clip = soundTrack;
        musicsource.Play();
        enemysource = gameObject.AddComponent<AudioSource>();
        enemysource.volume = miscVolume;
        playerSource = gameObject.AddComponent<AudioSource>();
        playerSource.volume = miscVolume;
        batonSource = gameObject.AddComponent<AudioSource>();
        batonSource.volume = miscVolume;
        playershootSourceBullet = gameObject.AddComponent<AudioSource>();
        playershootSourceBullet.volume = miscVolume;
        playershootSourceLaser = gameObject.AddComponent<AudioSource>();
        playershootSourceLaser.volume = miscVolume;
        enemyShooterSource = gameObject.AddComponent<AudioSource>();
        enemyShooterSource.volume = miscVolume;
    }

    public void PlayPlayerAttack()
    {
        playerSource.clip = playerAttack;
        playerSource.Play();
    }
    public void PlayBatonHit()
    {
        batonSource.clip = batonHit;
        batonSource.Play();
    }
    public void PlayShootingBullet()
    {
        playershootSourceBullet.clip = bulletShooter;
        playershootSourceBullet.Play();
    }
    public void PlayShootingLaser()
    {
        playershootSourceLaser.clip = laser;
        playershootSourceLaser.Play();
    }

    public void PlayShootingRocket()
    {
        playershootSourceBullet.clip = rocketShooter;
        playershootSourceBullet.Play();
    }

    public void PlayEnemyShoot()
    {
        enemyShooterSource.clip = enemyShooter;
        enemyShooterSource.Play();
    }

    public void PlayDefeat()
    {
        playerSource.clip = lost;
        playerSource.Play();
    }

    public void PlayEnemyDestroy()
    {
        enemysource.clip = enemyDestroy[Random.Range(0, enemyDestroy.Length)];
        enemysource.Play();
    }

    public void PlayEnemyClip(string turret)
    {
        switch (turret)
        {
            case "biped":
                //Debug.Log("traccia hit biped");
                enemysource.clip = enemyHit[0];
                break;
            case "oculus":
                enemysource.clip = enemyHit[1];
                break;

        }
        enemysource.Play();
    }
}
