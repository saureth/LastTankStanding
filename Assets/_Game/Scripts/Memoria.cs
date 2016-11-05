using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memoria : MonoBehaviour {
	public List<Idea>		ideas;
	public List<Etiqueta>	etiquetas;
	public List<string>		valores;
	public string			textoBase;

	public void CrearIdea(string _nombre, Caracteristica[] _caracteristicas)
	{
		for(int i = 0; i< ideas.Count; i++)
		{
			if (ideas[i].nombre == _nombre)
			{
				return;
			}
		}
		Idea nIdea = Idea.CrearIdea(_nombre);
		for(int i = 0; i<_caracteristicas.Length; i++)
		{
			Etiqueta nEtiqueta 	= new Etiqueta();
			nEtiqueta.nombre 	= _caracteristicas[i].etiqueta;
			nEtiqueta.tipo 		= _caracteristicas[i].tipo;
			int iEtiqueta = CrearEtiqueta(nEtiqueta);
			int iValor = -1;
			if (_caracteristicas[i].tipo == Tipo.cantidad)
			{
				iValor = CrearValor(_caracteristicas[i].cantidad.ToString());
			}else{
				iValor = CrearValor(_caracteristicas[i].cualidad);
			}
			nIdea.AñadirIndice(iEtiqueta, iValor);
		}
		ideas.Add(nIdea);
	}

	public int CrearEtiqueta(Etiqueta _etiqueta)
	{
		for(int i = 0; i< etiquetas.Count; i++)
		{
			if (etiquetas[i].nombre == _etiqueta.nombre)
			{
				return i;
			}
		}
		etiquetas.Add(_etiqueta);
		return etiquetas.Count-1;
	}

	public int CrearValor(string _valor)
	{
		for(int i = 0; i< valores.Count; i++)
		{
			if (valores[i] == _valor)
			{
				return i;
			}
		}
		valores.Add(_valor);
		return valores.Count-1;
	}





/////////////////////////////////////// Convertir información en texto ///////////////
	string nTexto = "";

	public void IntentarConvertir()
	{
		ConvertirDesdeTexto(textoBase);
	}

	public void ConvertirATexto()
	{
		nTexto = "";
		for(int i = 0; i<ideas.Count; i++)
		{
			nTexto = nTexto + ideas[i].nombre + ":";
			for(int j = 0; j<ideas[i].indices.Count;j++)
			{
				nTexto = nTexto + ideas[i].indices[j].x + "," + ideas[i].indices[j].y + "/";
			}
			nTexto = nTexto + "|";
		}
		nTexto = nTexto.Substring(0, nTexto.Length-1) + "%";
		for(int i = 0; i<etiquetas.Count; i++)
		{
			nTexto = nTexto + etiquetas[i].nombre + ":";
			nTexto = nTexto + etiquetas[i].tipo.ToString();
			nTexto = nTexto + "|";
		}
		nTexto = nTexto.Substring(0, nTexto.Length-1) + "%";
		for(int i = 0; i<valores.Count; i++)
		{
			nTexto = nTexto + valores[i] + ",";
		}
		nTexto = nTexto.Substring(0, nTexto.Length-1);
		print(nTexto);
	}

	public void ConvertirDesdeTexto(string txt)
	{
		string[] nDatos = txt.Split('%');
		ideas 		= new List<Idea>();
		etiquetas 	= new List<Etiqueta>();
		valores		= new List<string>();

		// Tratar de obtener las ideas
		string[] listaIdeas = nDatos[0].Split('|');
		for (int i=0;i<listaIdeas.Length;i++)
		{
			Idea nIdea = new Idea();
			List<Vector2> nIndices = new List<Vector2>();
			string[] datoIdea = listaIdeas[i].Split(':');
			string[] datosIndices = datoIdea[1].Split('/');
			for(int j=0; j<datosIndices.Length; j++)
			{
				if (datosIndices[j].Length==3) nIndices.Add(ConvertirTextoEnVector(datosIndices[j]));
			}
			nIdea.nombre = datoIdea[0];
			nIdea.indices = nIndices;
			ideas.Add(nIdea);
		}

		// Tratar de obtener las etiquetas
		string[] listaEtiquetas = nDatos[1].Split('|');
		for (int i=0;i<listaEtiquetas.Length;i++)
		{
			string[] nDetalleEtiqueta =listaEtiquetas[i].Split(':');
			Etiqueta nEtiqueta 	= new Etiqueta();
			nEtiqueta.nombre 	= nDetalleEtiqueta[0];
			nEtiqueta.tipo 		= ConvertirTextoEnTipo(nDetalleEtiqueta[1]);
			etiquetas.Add(nEtiqueta);
		}

		// Tratar de obtener las etiquetas
		string[] listaValores = nDatos[2].Split(',');
		for (int i=0;i<listaValores.Length;i++)
		{
			if(listaValores[i].Length >0) valores.Add(listaValores[i]);
		}

	}

	Vector2 ConvertirTextoEnVector(string _txt)
	{
		string[] nDats = _txt.Split(',');
		return new Vector2(float.Parse(nDats[0]),float.Parse(nDats[1]));
	}

	Tipo ConvertirTextoEnTipo(string _txt)
	{
		if (_txt == "cantidad")
		{
			return Tipo.cantidad;
		}
		return Tipo.cualidad;
	}
}
