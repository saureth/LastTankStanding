using UnityEngine;
using System.Collections;

public class ApuntadorMovilScript : MonoBehaviour {

	public Transform der;
	public Transform izq;
	public float velocidad;
	public float distMinDevolverse;
	bool direccion = true;
	bool start;

	void Awake () {
		//transform.position = izq.position;
	}

	void Start () {
		//transform.position = izq.position;
		StartCoroutine(WaitForStart());
	}
	
	void Update () {
		if (start) {
			if (direccion) {
				transform.position += (der.position - transform.position).normalized * velocidad * Time.deltaTime;
				if (Vector3.Distance (transform.position, der.position) < distMinDevolverse) {
					direccion = !direccion;
				}
			} else {
				transform.position += (izq.position - transform.position).normalized * velocidad * Time.deltaTime;
				if (Vector3.Distance (transform.position, izq.position) < distMinDevolverse) {
					direccion = !direccion;
				}
			}
		}
	}

	IEnumerator WaitForStart () {
		yield return new WaitForSeconds (0.4f);
		start = true;
	}
}
