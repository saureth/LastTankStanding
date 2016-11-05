using UnityEngine;
using System.Collections;

public class Atributos : MonoBehaviour {
	public Caracteristica[]	caracteristicas;

	void Start(){
		Caracteristica[] _caracteristicas = new Caracteristica[caracteristicas.Length + 1];
		Caracteristica car = new Caracteristica ();
		car.etiqueta = "Posicion";
		car.tipo = Tipo.cualidad;
		car.cualidad = transform.position.ToString();
		car.seguridad = 1;
		_caracteristicas [0] = car;

		for (int i = 1; i < _caracteristicas.Length; i++) {
			_caracteristicas [i] = caracteristicas [i - 1];
		}
		caracteristicas = _caracteristicas;
	}
}
