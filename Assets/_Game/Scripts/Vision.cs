using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public Memoria miMemoria;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider otro) 
	{
		Atributos att = otro.GetComponent<Atributos>();
		if (att != null)
		{
			miMemoria.CrearIdea(otro.name, att.caracteristicas);

		}

	}
}
