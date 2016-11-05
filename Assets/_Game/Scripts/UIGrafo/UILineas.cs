using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILineas : MonoBehaviour {
	public LineRenderer	miLinea;
	public Transform	nodo1;
	public Transform	nodo2;
	public Sprite		imagenCuantitativa;
	public bool			cuantitativa = false;
	public Image 		miImagen;
	void Awake () 
	{
		miLinea = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		miLinea.SetPosition(0, nodo1.position);
		miLinea.SetPosition(1, nodo2.position);	
		transform.position = nodo2.position + (nodo1.position - nodo2.position)/2;
	}

	public void SetCuantitativa(bool _cuantitativa)
	{
		cuantitativa = _cuantitativa;
		if (cuantitativa) 
			miImagen.sprite = imagenCuantitativa;
	}
}
