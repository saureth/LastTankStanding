using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(DesicionesEmocionales))]

public class Motor : MonoBehaviour {
	public Vector2	rango;
	public Vector3 	posObjetivo;
	public bool		patrullando 	= true;
	public float 	velocidadInicial;
	DesicionesEmocionales misDesiciones;
	public float dudaGenerada;

	public GameObject enVision;

	void Start () 
	{
		velocidadInicial = GetComponent<NavMeshAgent>().speed;
		misDesiciones = GetComponent<DesicionesEmocionales>();
		Patrullar();
	}

	void Update () 
	{
		if(patrullando)
		{
			if ((transform.position-(posObjetivo + Vector3.up*transform.position.y)).magnitude <= 0.1f)
			{
				posObjetivo = new Vector3(Random.Range(-rango.x,rango.x),0,Random.Range(-rango.y,rango.y));
				GetComponent<NavMeshAgent>().SetDestination(posObjetivo);
			}
		}
	}

	void OnTriggerEnter(Collider otro)
	{
		enVision = otro.gameObject;
		if (otro.tag == "obstaculo")
		{
			misDesiciones.Dudar(dudaGenerada);
			misDesiciones.TomarDesicion();
		}else if (otro.tag == "premio")
		{
			misDesiciones.CambiarAnsiedad(0);
			Despatrullar();
			Invoke("Patrullar",2);
		}else
		{
			misDesiciones.Dudar(1);
		}
	}

	void OnTriggerExit(Collider otro)
	{
		enVision = null;
	}

	public void Patrullar()
	{
		patrullando = true;
		posObjetivo = new Vector3(Random.Range(-rango.x,rango.x),0,Random.Range(-rango.y,rango.y));
		GetComponent<NavMeshAgent>().SetDestination(posObjetivo);
	}

	public void Despatrullar()
	{
		patrullando = false;
		GetComponent<NavMeshAgent>().SetDestination(transform.position);
	}


	public void Atacar()
	{
		Despatrullar();
		Destroy(enVision,1);
		Invoke("Patrullar",2);
	}

	public void Mejorar()
	{
		Despatrullar();
		enVision.transform.localScale = new Vector3(1,enVision.transform.localScale.y+0.3f,1);
		Invoke("Patrullar",2);
	}

	public void Huir()
	{
		GetComponent<NavMeshAgent>().speed = velocidadInicial * 3;
		Patrullar();
		Invoke("RestaurarVelocidad",3);
	}

	void RestaurarVelocidad()
	{
		GetComponent<NavMeshAgent>().speed = velocidadInicial;
	}

	public void Empeorar()
	{
		Despatrullar();
		enVision.transform.localScale = new Vector3(1,enVision.transform.localScale.y-0.3f,1);
		Invoke("Patrullar",2);
	}

}
