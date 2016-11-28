using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float damage;
	public float shootSpeed;
	public float maxTimeAlive;
	Vector3 startPosition;
	public bool shooted;
	public float hitMissDistance;
	public Collider ignoreCollisionWith;
	public GameObject hitExplosion;

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
		DoSomethingOnCollision (c.contacts[0].point);
	}

	void RaycastingForMiss () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, hitMissDistance)) {
			DoSomethingOnCollision (hit.point);
		}
		if (Physics.Raycast (transform.position, -transform.forward, out hit, hitMissDistance / 2)) {
			DoSomethingOnCollision (hit.point);
		}
	}

	void DoSomethingOnCollision (Vector3 cPos) {
		GameObject g = Instantiate (hitExplosion, cPos, Quaternion.identity) as GameObject;
		g.SetActive (true);
		Destroy (gameObject);
	}


}

