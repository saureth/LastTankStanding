using UnityEngine;
using System.Collections;

public class Agente : MonoBehaviour {
	public int vida;
	NavMeshAgent miAgente;

	// Use this for initialization
	void Start () {
		miAgente = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void mover(Vector3 point){
		miAgente.SetDestination (point);
	}

	public void quitarVida(int damage){
		this.vida = damage;
		if (vida <= 0) {
			Destroy (gameObject);
		}
	}
	public int getVida(){
		return this.vida;
	}
}
