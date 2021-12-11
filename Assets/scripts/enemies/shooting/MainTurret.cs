using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour {

	public enum TurretType {bullet=1, rocket=5, healing=10}
	public TurretType myTurret;
	public int health;
	public float timeToSpawn;
    [SerializeField] int costToBuy, costToUpgrade;
    [SerializeField] GameObject turretUpgrade;
	public void SendInformation<T>(T param)
	{
		//GuiManager.instance.ShowInformation (param);
        
	}

	void OnMouseOver()
	{
		Debug.Log (this.name);
		if (Input.GetMouseButtonDown (0))
			SendInformation (this);
	}

    public int GetCostToBuy()
    {
        return costToBuy;
    }

    public int GetCostToUpgrade()
    {
        return costToUpgrade;
    }

    public void UpgradeTurret()
    {
        
        GameObject aux=Instantiate(turretUpgrade, transform.position, transform.rotation);
        aux.GetComponent<MainTurret>().SendInformation(aux.GetComponent<MainTurret>());
        //PlayerTurretManager.instance.AddTurret(aux);
        //PlayerTurretManager.instance.RemoveTurret(gameObject);
        Destroy(gameObject);
    }

  
}
