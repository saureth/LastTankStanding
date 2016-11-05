using UnityEngine;
using System.Collections;

public class Enemigo : MonoBehaviour {
	public Transform canon;
	public GameObject proyectile;
	public float tiempoDisparo;
	private float time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			//canon.transform.LookAt (other.transform.position);
			Invoke ("Fire", 1f);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			canon.transform.LookAt (other.transform.position);
		}     
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			CancelInvoke ("Fire");
		}
	}

	private void Fire(){
		Instantiate (proyectile, canon.position, canon.rotation);
		Invoke ("Fire", 1f);
	}
}
