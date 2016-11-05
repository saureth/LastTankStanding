using UnityEngine;
using System.Collections;

public class Proyectil : MonoBehaviour {
	public float vel;
	public float damage;
	public float alcance;

	private float time;
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		transform.position += transform.forward * vel * Time.deltaTime;
		if ((time * vel) >= alcance) {
			Destroy (gameObject);
		}

	}

	/*
	void OnCollisionEnter(Collider otro) {
		if (otro.CompareTag("Destruible")) {
			otro.GetComponent<DestroyObject> ().destroy ();
		}
	}
	*/

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Destruible") {
			collision.transform.GetComponent<DestroyObject> ().destroy ();
			collision.transform.GetComponent<BoxCollider> ().enabled = false;
			Destroy (gameObject);

		}
		if (collision.transform.tag == "Player") {
			//collision.transform.GetComponent<ControlPersonaje>().
			collision.transform.GetComponent<ControlPersonaje>().generarDamage((int)damage);
			Destroy (gameObject);
		}
		Destroy (gameObject);
	}
}
