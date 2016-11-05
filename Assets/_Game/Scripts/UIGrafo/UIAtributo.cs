using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAtributo : MonoBehaviour {
	public Memoria			memoriaAgente;
	public GameObject		prefabIdea;
	public GameObject		prefabAttr;
	public List<GameObject>	ideas;
	public List<GameObject>	attrs;
	public List<GameObject>	lineas;

	public Transform		posIniIdea;
	public Transform		posIniAttr;
	public int				moverIdea		= -30;
	public int				moverAtributo	= -15;

	
	void Start () 
	{
		ideas = new List<GameObject>();
		attrs = new List<GameObject>();
		lineas = new List<GameObject>();
		ActualizarLista ();
	}
	
	public void ActualizarLista () 
	{
		for(int i = attrs.Count; i<memoriaAgente.valores.Count;i++)
		{
			GameObject nAttr = Instantiate(prefabAttr, posIniAttr.position, Quaternion.identity) as GameObject;
			nAttr.GetComponent<TextoUI>().texto.text = memoriaAgente.valores[i];
			attrs.Add(nAttr);
			posIniAttr.Translate(Vector3.up * (moverAtributo));
		}

		for(int i = ideas.Count; i<memoriaAgente.ideas.Count;i++)
		{
			GameObject nIdeaUI = Instantiate(prefabIdea, posIniIdea.position, Quaternion.identity) as GameObject;
			nIdeaUI.GetComponent<TextoUI>().texto.text = memoriaAgente.ideas[i].nombre;
			ideas.Add(nIdeaUI);
			nIdeaUI.GetComponent<UILineador>().CrearLineas(memoriaAgente.ideas[i].indices, attrs, memoriaAgente.etiquetas);
			posIniIdea.Translate(Vector3.up * (moverIdea));
		}


		Invoke("ActualizarLista", 2);
	}
}
