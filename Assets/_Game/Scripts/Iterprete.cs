using UnityEngine;
using System.Collections;

public class Iterprete : MonoBehaviour {
	public TextAsset 	objetivo;
	public string[] 	lineas;
	public ControlPersonaje	miControl;

	[SerializeField]
	private int apuntador	= -1;

	void Start () 
	{
		lineas = objetivo.text.Replace(";","=").Split('\n');
		Debug.Log(transform.position);
	}

	public void SiguienteDesicion () 
	{
		apuntador ++;
		Interpretar();
	}

	public void Interpretar()
	{
		string[] lineaActual = lineas[apuntador].Split('=');
		string accion = lineaActual[0];

		switch (accion.ToLower()) {
		case "buscar":
			miControl.Buscar(lineaActual[1]);
			break;
		case "disparar":
			miControl.disparar();
			break;
		case "mirar":
			string[] casos = lineaActual[2].Split(',');
			int i = int.Parse(casos[0]);
			int j = int.Parse(casos[1]);
			if(miControl.MirarSiEsta(lineaActual[1]))
			{
				apuntador = i -2;
				SiguienteDesicion ();
			}else{
				apuntador = j -2;
				SiguienteDesicion ();
			}
			break;
		case "imprimir":
			print(lineaActual[1]);
			break;
		default:
			print("NoEntiendo");
			break;
		}
	}

}
