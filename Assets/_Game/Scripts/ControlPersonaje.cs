using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public Memoria conocimiento; //Conocimiento adquirido por el agente.

	private float velocidadAnterior;
	public bool siEsta = false;

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
		//Debug.Log ("execute");
		//Debug.Log ((transform.position - target).magnitude);
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

	public void Buscar(string propiedades){
		
		string[] caracteristica = propiedades.Split (':');
		Vector2 indice = new Vector2 ();
		indice.x = conocimiento.GetIndexEtiqueta (caracteristica [0]);
		indice.y = conocimiento.GetIndexValor (caracteristica [1]);
		//Debug.Log (indice.x + " " + indice.y);
		if ((indice.x >=0) && (indice.y >=0)) {
			Idea myIdea = conocimiento.GetIdeaCaracteristica (indice);
			Debug.Log (myIdea.nombre);
			if (myIdea != null) {
				int etiquetaPos = conocimiento.GetIndexEtiqueta ("Posicion");
				int indexValor = myIdea.GetIndexValor (etiquetaPos);
				string valor = conocimiento.GetValor (indexValor);

				if (valor != "") {
					valor = valor.Substring (1, valor.Length - 2);
					valor = valor.Replace(" ","");
					string[] pos = valor.Split (',');
					//Debug.Log (valor);
					Vector3 posicion = new Vector3 (float.Parse(pos[0]),float.Parse(pos[1]),float.Parse(pos[2]));
					setPosition (posicion-(posicion-transform.position).normalized*2);
				}

			}
		}
	}

	public bool MirarSiEsta(string b){
		return siEsta;
	}
}

