using UnityEngine;
using System.Collections;
[System.Serializable]
public class Atomo 
{
	public string	dato;
	public Atomo[]	ligasAnteriores;
	public Atomo[]	ligasSiguientes;

	public void ConectarSiguiente(Atomo _siguiente)
	{
		for(int i=0; i< ligasSiguientes.Length;i++)
		{
			if (ligasSiguientes[i].dato == _siguiente.dato)
			{
				return;
			}
		}

		Atomo[] nLista = new Atomo[ligasSiguientes.Length + 1];
		for(int i=0; i< ligasSiguientes.Length;i++)
		{
			nLista[i] = ligasSiguientes[i];
		}
		nLista[ligasSiguientes.Length + 1] = _siguiente;
		_siguiente.ConectarAnterior(this as Atomo);
	}

	public void ConectarAnterior(Atomo _anterior)
	{
		for(int i=0; i< ligasAnteriores.Length;i++)
		{
			if (ligasAnteriores[i].dato == _anterior.dato)
			{
				return;
			}
		}

		Atomo[] nLista = new Atomo[ligasAnteriores.Length + 1];
		for(int i=0; i< ligasAnteriores.Length;i++)
		{
			nLista[i] = ligasAnteriores[i];
		}
		nLista[ligasSiguientes.Length + 1] = _anterior;
		_anterior.ConectarSiguiente(this as Atomo);
	}
}
