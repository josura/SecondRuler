using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IKIllable,IDamageable<float> {
    protected float health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Image lifebar;
    [SerializeField] protected int minCoinsDrop,maxCoinsDrop;
    protected string enemyType;
    protected int enemyScore;
    // Use this for initialization
    protected void OnEnable()
    {
        health = maxHealth;
        lifebar.fillAmount = 1f;
    }

    protected virtual GameObject dropCoins(Transform trans)
    {
        trans.position.Set(trans.position.x, 0, trans.position.z);
        return CoinManager.Instance.createBronzeCoins(trans);
    }

    public void Kill()
    {
        //TODO animazione morte
        //find a way to correct null reference if object is set inactive if hit by a bullet (another bullet will collide even if the object is set inactive for a short period of time)
        //CoinManager.Instance.addcoins(10);  // coin drop is utilized instead
        dropCoins(transform);
        Random.Range(minCoinsDrop, maxCoinsDrop);
        SoundManager.Instance.PlayEnemyDestroy();
        GUIManager.Instance.addScore(enemyScore);
        this.gameObject.SetActive(false);
    }

    public bool Damage(float damage)
    {
        SoundManager.Instance.PlayEnemyClip(enemyType);
        health -= damage;
        float fill = Mathf.Clamp01( health / (float)maxHealth);
        lifebar.fillAmount = fill;
        if (health <= 0)
        {
            Kill();
        }
        return health <= 0;
    }



    public void Spawn(Transform spawnPoint)
    {
        transform.Spawn(spawnPoint);
    }
}
