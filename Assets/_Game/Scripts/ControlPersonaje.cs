using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]

public class ControlPersonaje : MonoBehaviour {
	NavMeshAgent miAgente;
	public float vida;
	public DesicionesEmocionales misDesiciones;
	public Vector3	rangoPosiciones;
	public Color	colorGizmo;
	public Transform canyon;
	public GameObject proyectil;
	public Vector3 target;
	public float velocidadMaxima;
	private float velocidadAnterior;

	void Awake () 
	{
		miAgente = GetComponent<NavMeshAgent>();
		miAgente.SetDestination (transform.position);
		target = transform.position;
		velocidadAnterior = miAgente.speed;
	}
	
	public void ActualizarPosicion () 
	{
		//miAgente.SetDestination(new Vector3(Random.Range(-rangoPosiciones.x, rangoPosiciones.x), 0, Random.Range(-rangoPosiciones.z, rangoPosiciones.z)));
		setPosition(new Vector3(Random.Range(-rangoPosiciones.x, rangoPosiciones.x), 0, Random.Range(-rangoPosiciones.z, rangoPosiciones.z)));
	}

	public void setPosition(Vector3 pos){
		miAgente.SetDestination (pos);
		target=pos;
	}

	public void generarDamage(int damage){
		misDesiciones.TomarDesicion ();
		vida -= damage;
		if (vida <= 0) {
			Destroy (gameObject);
		}
	}

	public void disparar(){
		GameObject.Instantiate (proyectil, canyon.position,transform.rotation);
	}

	public void patrullar(){
		//setPosition(new Vector3(Random.Range(-rangoPosiciones.x, rangoPosiciones.x), 0, Random.Range(-rangoPosiciones.z, rangoPosiciones.z)));
		IniciarPatrullaje ();
	}

	public void DetenerPatrullaje(){
		CancelInvoke ("IniciarPatrullaje");
		setPosition (transform.position);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = colorGizmo;
		Gizmos.DrawCube(Vector3.zero, rangoPosiciones*2);
	}

	private void IniciarPatrullaje(){
		Debug.Log ("execute");
		Debug.Log ((transform.position - target).magnitude);
		if ((transform.position-target).magnitude<1f) {
			setPosition(new Vector3(Random.Range(-rangoPosiciones.x, rangoPosiciones.x), 0, Random.Range(-rangoPosiciones.z, rangoPosiciones.z)));
		}
		Invoke ("IniciarPatrullaje", 0.1f);
	}

	public void Correr(){
		Invoke ("DetenerCorrer", 3f);
		miAgente.speed = miAgente.speed + 10f;
		miAgente.speed = Mathf.Clamp (miAgente.speed, 0f, velocidadMaxima);
	}

	public void DetenerCorrer(){
		miAgente.speed = velocidadAnterior;
	}

	public void buscar(string propiedades){

	}
}

