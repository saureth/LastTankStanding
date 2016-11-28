using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	string playerName;
	public float damage;
	public float shootSpeed;
	public float maxTimeAlive;
	Vector3 startPosition;
	public bool shooted;
	public float hitMissDistance;
	public Collider ignoreCollisionWith;
	public GameObject hitExplosion;

	public void setPlayerName(string pn){
		this.playerName = pn;
	}

	void Start () {
		startPosition = transform.position;
		Physics.IgnoreCollision (GetComponent<Collider>(), ignoreCollisionWith);
		Destroy (gameObject, maxTimeAlive);
	}
	
	void Update () {
		RaycastingForMiss ();
	}

	void FixedUpdate () {
		if (shooted) {
			GetComponent<Rigidbody> ().AddForce (transform.forward * shootSpeed);
			shooted = false;
		}
	}

	void OnCollisionEnter (Collision c) {
		DoSomethingOnCollision (c.contacts[0].point, c.gameObject);
	}

	void RaycastingForMiss () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, hitMissDistance)) {
			DoSomethingOnCollision (hit.point, hit.collider.gameObject);
		}
		if (Physics.Raycast (transform.position, -transform.forward, out hit, hitMissDistance / 2)) {
			DoSomethingOnCollision (hit.point, hit.collider.gameObject);
		}
	}

	void DoSomethingOnCollision (Vector3 cPos, GameObject c) {
		GameObject g = Instantiate (hitExplosion, cPos, Quaternion.identity) as GameObject;
		g.SetActive (true);
		if (c.gameObject.CompareTag("Tank")) {		
			
			//Dealths the damage and calculates if the other tank has been destroyed
			bool tankDestroyed = c.gameObject.GetComponent<LifeBarCtrl> ().TakeDamage (damage);

			//Calculates the statistics:
			GameObject statCtrl = GameObject.FindWithTag ("StatCtrl");
			GameStatistics stat = new GameStatistics();  
			int iterator=-1;

			for (int i = 0; i < statCtrl.GetComponent<StatCtrl>().playerStat.Length; i++) {
				
				if (statCtrl.GetComponent<StatCtrl>().playerStat[i].getPlayerName().Equals(playerName)) {
					stat = statCtrl.GetComponent<StatCtrl> ().playerStat [i];
					iterator = i;

					float tabs = stat.getTotalAmountOfBulletShooted();
					tabs += 1;
					stat.setTotalAmountOfBulletShooted (tabs);

					int tdd = stat.getTotalDamageDealt();
					tdd += 1;
					stat.setTotalDamageDealt(tdd);

					statCtrl = GameObject.FindWithTag ("StatCtrl");
					Debug.Log (iterator);
					statCtrl.GetComponent<StatCtrl> ().playerStat [iterator].DebugMyStatistics();

					if (tankDestroyed) {
						int kills = stat.getAmountOfKills ();
						kills++;
						stat.setAmountOfKills (kills);

						GameObject statCtrlOther = GameObject.FindWithTag ("StatCtrl");
						GameStatistics statOther = new GameStatistics ();  
						int iterator2 = -1;
						for (int j = 0; j < statCtrlOther.GetComponent<StatCtrl> ().playerStat.Length; j++) {
							if (statCtrlOther.GetComponent<StatCtrl> ().playerStat [j].getPlayerName ().Equals (c.name)) {
								statOther = statCtrl.GetComponent<StatCtrl> ().playerStat [j];
								iterator2 = j;
								j = statCtrlOther.GetComponent<StatCtrl> ().playerStat.Length;
							}
						}
						int deaths = statOther.getAmountOfDeaths ();
						deaths++;
						statOther.setAmountOfDeaths (deaths);
						Debug.Log ("A PLAYER HAS KILLED ANOTHER");
						Debug.Log ("STATS IF THE FALLEN ONE: "); 
						statCtrlOther.GetComponent<StatCtrl> ().playerIsAlive [iterator2] = false;
						statCtrlOther.GetComponent<StatCtrl> ().playerStat [iterator2].DebugMyStatistics();
					}
					i = statCtrl.GetComponent<StatCtrl> ().playerStat.Length;
				}
			}
		}
		Destroy (gameObject);
	}	
}
