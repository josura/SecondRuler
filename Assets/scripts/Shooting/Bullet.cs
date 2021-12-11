using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    float currentTime;
    private void OnEnable()
    {
        currentTime = 0;
        StartCoroutine(lifeTime());
    }

    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }

    public void Spawn(Transform spawnPoint)
    {
        transform.Spawn(spawnPoint);
    }
    
}