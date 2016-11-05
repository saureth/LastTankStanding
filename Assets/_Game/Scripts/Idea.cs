using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Idea
{
	public string			nombre;
	public List<Vector2>	indices;

	public static Idea CrearIdea(string _nombre)
	{
		Idea nIdea 			= new Idea();
		nIdea.nombre 		= _nombre;
		nIdea.indices		= new List<Vector2>();
		return nIdea;
	}

	public void AñadirIndice(int _etiqueta, int _valor)
	{
		float nEtiqueta 	= _etiqueta+0.0f;
		float nValor		= _valor+0.0f;
		Vector2 nIndices 	= new Vector2(nEtiqueta,nValor);
		this.indices.Add(nIndices);
	}

	public int GetIndexValor(int etiqueta){
		int valor=-1;
		if (etiqueta >=0) {
			foreach (Vector2 item in indices) {
				if (item.x == etiqueta) {
					valor = (int)item.y;
					break;
				}
			}
		}
		return valor;
	}

}
