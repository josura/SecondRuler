using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class flyingEnemy : Enemy {

    GameObject target;
    [SerializeField] Transform targetTransform;
    NavMeshAgent agent;

    // Use this for initialization
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player1");
    }



    // Update is called once per frame
    void Update()
    {
        if (!pauseManager.isGamePaused)
        {
            targetTransform = target.transform;
            
                agent.SetDestination(targetTransform.position);
        }
    }

}
