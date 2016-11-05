using UnityEngine;
using System.Collections;
[System.Serializable]

public enum	Tipo
{
	cualidad	= 0,
	cantidad	= 1
}
[System.Serializable]

public enum	Filtro
{
	Visual		= 0,
	Sensorial	= 1,
	Quinestesico= 2,
	Auditivo	= 3
}

[System.Serializable]
public class Etiqueta
{
	public string 	nombre;
	public Tipo		tipo;
}

[System.Serializable]
public class Caracteristica 
{
	public string 	etiqueta;
	public Tipo 	tipo;
	public string 	cualidad;
	public float	cantidad;
	// este sería para saber qué tan seguro está de que es esa característica
	// la verdad no estoy seguro de que este debiera ser un atributo publico.
	[Range(0.0f,1.0f)]
	public float	seguridad = 1;
	// filtro para saber si puede ser detectado por algún sensor específico
	public Filtro filtro;
}
