using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour,IMoveable {
    [SerializeField] float speed=10;
    [SerializeField] float lifetime = 10f;
    WaitForSeconds lifeTimeSec;
    float currentTime;

    private void OnEnable()
    {
        StartCoroutine(move());
        lifeTimeSec = new WaitForSeconds(lifetime);
        StartCoroutine(lifeTime());
    }

    public void Spawn(Transform spawnPoint)
	{
		
		transform.Spawn (spawnPoint);
	}

    public void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    IEnumerator move()
    {
        while (true)
        {
            Move();
            yield return null;
        }
    }

    IEnumerator lifeTime()
    {
        yield return lifeTimeSec;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!pauseManager.isGamePaused)
        {
            /*currentTime += Time.deltaTime;
            if (currentTime >= lifetime)
            {
                this.gameObject.SetActive(false);
            }*/
        }
    }
}
