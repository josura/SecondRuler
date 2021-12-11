using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour {
    private GameObject target, turret;
    private Quaternion rotation;
    [SerializeField] List<GameObject> targets = new List<GameObject>();
    void Start()
    {
        turret = gameObject;
        rotation = turret.transform.rotation;
    }

    void Update()
    {

        if (target != null)
        {
            turret.transform.LookAt(target.transform.position);
        }
    }

    void LateUpdate()
    {
        if ((target != null) && (!target.activeInHierarchy))
        {
            RemoveTarget(target);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //AddTarget
        //SetTarget
        if (other.gameObject.layer == 8) {
            //target = other.gameObject;
            targets.Add(other.gameObject);
            if (target == null) {
                SetTarget();
            }

        }

    }

    //	void OnCollisionEnter(Collision coll)
    //	{
    //		Debug.Log (coll.gameObject);
    //	}

    void SetTarget()
    {
        target = targets[0];
        transform.parent.GetComponent<Animator>().enabled = false;
        transform.parent.GetComponent<ShootingTurret>().StartCoroutine("Shoot");
    }

	void OnTriggerExit(Collider other)
	{
		//RemoveTarget
		//CheckList
		if (other.gameObject.layer == 8) {

            //Deactivate ();
            RemoveTarget(other.gameObject);
			//Implementare check su lista
		}

	}

    void RemoveTarget(GameObject obj)
    {
        targets.Remove(obj);
        CheckList();
    }

	public void CheckList()
	{
		if (targets.Count == 0)
			Deactivate ();
		else
			target = targets [0];
	}

	public void Deactivate()
	{
		Debug.Log ("Deactivate");
		target = null;
		GetComponentInParent<ShootingTurret> ().StopAllCoroutines();
		turret.transform.rotation = rotation;
		GetComponentInParent<Animator> ().enabled = true;
	}
}
