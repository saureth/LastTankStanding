using UnityEngine;
using System.Collections;

public class Hubicador : MonoBehaviour {
	public float velocidad;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		//Debug.Log("En Trigger");
		transform.Translate((-other.transform.position + transform.position) * velocidad * Time.deltaTime);
	}
}
