using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DesicionesEmocionales : MonoBehaviour {
	[Header("Ira")]
	public Gradient 	gIra;
	[Range(0,1)]
	public float		iIra;
	[Range(0,1)]
	public float		eIra;
	public Vector3		importanciaIra;
	public UnityEvent	cIra;

	[Header("Tristeza")]
	public Gradient 	gTristeza;
	[Range(0,1)]
	public float		iTristeza;
	[Range(0,1)]
	public float		eTristeza;
	public Vector3		importanciaTristeza;
	public UnityEvent	cTristeza;

	[Header("Miedo")]
	public Gradient		gMiedo;
	[Range(0,1)]
	public float		iMiedo;
	[Range(0,1)]
	public float		eMiedo;
	public Vector3		importanciaMiedo;
	public UnityEvent	cMiedo;

	[Header("Alegria")]
	public Gradient		gAlegria;
	[Range(0,1)]
	public float		iAlegria;
	[Range(0,1)]
	public float		eAlegria;
	public Vector3		importanciaAlegria;
	public UnityEvent	cAlegria;

	[Header("Estado anímico")]
	public float 		ansiedad			= 1;
	public float 		autorrealizacion	= 0;
	public float 		duda				= 0;

	private float dudaNatural = 0;

	void Start () {
		ControlAnsiedad();
		dudaNatural = duda;
	}

	public void ControlAnsiedad()
	{
		ansiedad += 0.01f;
		if(ansiedad > 1) ansiedad = 1;
		Invoke("ControlAnsiedad",1);
	}

	public void CambiarAnsiedad(float _ansiedad)
	{
		ansiedad = _ansiedad;
	}

	public void Dudar(float _duda)
	{
		duda = dudaNatural + _duda;
		ControlDuda();
	}

	public void ControlDuda()
	{
		duda -= 0.1f;
		if (duda<dudaNatural){
			duda = dudaNatural;
		}else{
			Invoke("ControlDuda",0.3f);
		}
	}

	public void CambiarAutorrealizacion(float _autorrealizacion)
	{
		autorrealizacion = _autorrealizacion;
	}

	public void TomarDesicion () 
	{
		float p1 = eIra		*	(ansiedad*importanciaIra.x + autorrealizacion*importanciaIra.y + duda*importanciaIra.z) + iIra;
		float p2 = eTristeza*	(ansiedad*importanciaTristeza.x + autorrealizacion*importanciaTristeza.y + duda*importanciaTristeza.z) + iTristeza;
		float p3 = eMiedo	*	(ansiedad*importanciaMiedo.x + autorrealizacion*importanciaMiedo.y + duda*importanciaMiedo.z) + iMiedo;
		float p4 = eAlegria	*	(ansiedad*importanciaAlegria.x + autorrealizacion*importanciaAlegria.y + duda*importanciaAlegria.z) + iAlegria;

		if (p1 > p2 && p1 > p3 & p1 > p4)
		{
			cIra.Invoke();
		}else if (p2 > p1 && p2 > p3 & p2 > p4)
		{
			cTristeza.Invoke();
		}else if (p3 > p2 && p1 < p3 & p3 > p4)
		{
			cMiedo.Invoke();
		}else if (p4 > p2 && p4 > p3 & p1 < p4)
		{
			cAlegria.Invoke();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = gIra.Evaluate(iIra);
		Gizmos.DrawCube(transform.position+(Vector3.left*(-0.37f))+(Vector3.up*eIra/2)+(Vector3.up*0.5f), new Vector3(0.25f,eIra,0.25f));
		Gizmos.color = gTristeza.Evaluate(iTristeza);
		Gizmos.DrawCube(transform.position+(Vector3.left*(-0.125f))+(Vector3.up*eTristeza/2)+(Vector3.up*0.5f), new Vector3(0.25f,eTristeza,0.25f));
		Gizmos.color = gMiedo.Evaluate(iMiedo);
		Gizmos.DrawCube(transform.position+(Vector3.left*(0.125f))+(Vector3.up*eMiedo/2)+(Vector3.up*0.5f), new Vector3(0.25f,eMiedo,0.25f));
		Gizmos.color = gAlegria.Evaluate(iAlegria);
		Gizmos.DrawCube(transform.position+(Vector3.left*(0.37f))+(Vector3.up*eAlegria/2)+(Vector3.up*0.5f), new Vector3(0.25f,eAlegria,0.25f));
	}
}
