using UnityEngine;
using System.Collections;

public class BulletCharge : MonoBehaviour {

	public int amount; 

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Tank")) {
			other.gameObject.GetComponent<PlayerShoot> ().bullets += amount;
			other.gameObject.GetComponent<PlayerShoot> ().bullets = Mathf.Clamp (other.gameObject.GetComponent<PlayerShoot> ().bullets, 0, other.gameObject.GetComponent<PlayerShoot> ().maxBullets);
			this.gameObject.SetActive (false);
		}
	}
}
