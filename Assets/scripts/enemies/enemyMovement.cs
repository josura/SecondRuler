using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyMovement : MonoBehaviour {
    GameObject target;
    [SerializeField] Transform targetTransform;
    NavMeshAgent agent;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        target = GameObject.Find("player1");
    }

    
    // Update is called once per frame
    void Update () {
        if (!pauseManager.isGamePaused)
        {
            targetTransform = target.transform;
            float distanza = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetTransform.position.x, targetTransform.position.z));
            if (distanza <= 1)
                anim.speed = distanza / 10;
            else anim.speed = 1f;
            if (distanza <= 1f)
            {
                anim.Play("enemy1", -1, 0);
            }
            if (distanza > 4)
            {
                agent.destination = targetTransform.position;
            }
        }
    }
}
