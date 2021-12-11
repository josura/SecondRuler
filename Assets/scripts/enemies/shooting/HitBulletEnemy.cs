using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBulletEnemy : MonoBehaviour {

	[SerializeField] int damage;

	public void SetBulletType(int damageAmount)
	{
		damage = damageAmount;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8) {
			Debug.Log ("Colpito");
            //other.GetComponent<Player> ().Damage (damage);
            PlayerManager.Instance.Damage(damage);
			transform.gameObject.SetActive (false);

		}
	}

	void OnBecameInvisible()
	{
		Debug.Log ("Invisibile");
		gameObject.SetActive (false);
	}
}
