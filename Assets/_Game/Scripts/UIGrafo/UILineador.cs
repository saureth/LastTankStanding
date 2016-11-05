using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UILineador : MonoBehaviour 
{
	public GameObject		prefabLinea;
	public List<GameObject>	misLineas;
	public bool visible		= false;
	public static bool 	mantener = false;
	bool antMantener;

	void Awake () 
	{
		misLineas = new List<GameObject>();
	}
	
	void OnMouseEnter() {
		if (!visible && !mantener) Visibilizar();
    }
	
	void OnMouseExit() {
		if (visible && !mantener) Invisibilizar();
    }
	
	public void CrearLineas (List<Vector2> _indices, List<GameObject> _attrs, List<Etiqueta> _etiquetas) 
	{
		for(int j=0; j<_indices.Count; j++)
		{
			int posIndex = Mathf.FloorToInt(_indices[j].y);
			GameObject nLinea = Instantiate(prefabLinea) as GameObject;
			nLinea.transform.localPosition = (transform.localPosition + _attrs[posIndex].transform.localPosition)/2;
			nLinea.GetComponent<TextoUI>().texto.text = _etiquetas[ Mathf.FloorToInt(_indices[j].x)].nombre;
			nLinea.GetComponent<UILineas>().SetCuantitativa(_etiquetas[ Mathf.FloorToInt(_indices[j].x)].tipo == Tipo.cantidad);
			nLinea.GetComponent<UILineas>().nodo1 = transform;
			nLinea.GetComponent<UILineas>().nodo2 = _attrs[posIndex].transform;

			misLineas.Add(nLinea);
		}
		Invisibilizar();
		RevizarVision();
	}

	public void Invisibilizar()
	{
		for(int i=0; i<misLineas.Count;i++)
		{
			misLineas[i].SetActive(false);
		}
		visible = false;
	}
	public void Visibilizar()
	{
		for(int i=0; i<misLineas.Count;i++)
		{
			misLineas[i].SetActive(true);
		}
		visible = true;
	}

	void RevizarVision()
	{
		if(mantener && !visible)
		{
			Visibilizar();
		}else if(!mantener && (mantener != antMantener) && visible)
		{
			Invisibilizar();
		}
		antMantener = mantener;
		Invoke("RevizarVision", 1);
	}

	public void MantenerONo(bool _mant)
	{
		mantener = _mant;
	}
}
