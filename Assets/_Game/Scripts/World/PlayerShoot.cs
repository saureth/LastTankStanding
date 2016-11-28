using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public bool player2;
	string playerName;

	public int bullets;
	public int maxBullets;
	public float damage;
	public float shootSpeed;
	public float maxTimeAlive;
	public float timeBetweenShoots;
	public Transform startPosition;
	public GameObject bullet;
	public float hitMissDistance;
	float t;
	public GameObject muzzleFlash;
	public ParticleSystem smokeAfterShoot;
	public Animator cameraAnim;

	void Start () {
		if (player2)
			playerName = "p2_";
		else
			playerName = "p1_";	

		bullets = maxBullets;
	}
	
	void Update () {
		Shoot ();
	}

	public void Shoot () {
		t += Time.deltaTime;
		if (Input.GetButton (playerName + "Fire1") && t > timeBetweenShoots && bullets > 0) {
			bullets--;
			GameObject m = Instantiate (muzzleFlash, startPosition.position, Quaternion.identity) as GameObject;
			m.SetActive (true);
			cameraAnim.SetTrigger ("Shoot");
			smokeAfterShoot.Play ();
			GameObject g = Instantiate (bullet, startPosition.position, Quaternion.LookRotation(startPosition.forward)) as GameObject;
			g.GetComponent<BulletScript> ().setPlayerName (this.gameObject.name);
			g.GetComponent<BulletScript> ().damage = damage;
			g.GetComponent<BulletScript> ().shootSpeed = shootSpeed;
			g.GetComponent<BulletScript> ().maxTimeAlive = maxTimeAlive;
			g.GetComponent<BulletScript> ().ignoreCollisionWith = GetComponent<Collider> ();
			g.GetComponent<BulletScript> ().shooted = true;
			g.GetComponent<BulletScript> ().hitMissDistance = hitMissDistance;
			t = 0;
		}
	}
}
