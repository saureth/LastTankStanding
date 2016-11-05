using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum cosas {
	algo = 1,
	algo2 = 2
}

public class VistaScript : MonoBehaviour {

	public Transform[] apuntadoresMoviles;
	Memoria miMemoria;
	public DesicionesEmocionales misDesiciones;

	void Start () {

		miMemoria = GetComponent<Memoria>();
	}
	

	void Update () {
		RaycasteoMovil ();
	}

	void RaycasteoMovil () {
		RaycastHit hit;
		for (int i = 0; i < apuntadoresMoviles.Length; i++) {
			
			Debug.DrawRay (transform.position, apuntadoresMoviles[i].position - transform.position, Color.magenta);

			if (Physics.Raycast (transform.position, (apuntadoresMoviles[i].position - transform.position).normalized, out hit, Vector3.Distance(apuntadoresMoviles[i].position, transform.position))) {

				Atributos att = hit.collider.GetComponent<Atributos>();
				if (att != null)
				{
					miMemoria.CrearIdea(hit.collider.name, att.caracteristicas);
					misDesiciones.TomarDesicion ();

				}
			}
		}
	}

}
