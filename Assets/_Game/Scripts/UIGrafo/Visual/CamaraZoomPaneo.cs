using UnityEngine;
using System.Collections;

public class CamaraZoomPaneo : MonoBehaviour {

	public float 	cameraDistanceMax = 20f;
	public float 	cameraDistanceMin = 5f;
	public float 	cameraDistance = 10f;
	public float 	scrollSpeed = 0.5f;
	public Camera 	miCamara;

	public Vector3 	bPos;
	public float	velocidadMovimiento;
	void Start()
	{
		cameraDistance = miCamara.orthographicSize;
	}

	 
	void Update()
	{
	    cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
	    cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
		miCamara.orthographicSize = cameraDistance;
	    
	    if (Input.GetMouseButtonDown(2))
	    {
	    	bPos = Input.mousePosition;
	    }
	    if (Input.GetMouseButton(2))
	    {
	    	transform.Translate((Input.mousePosition - bPos) * -velocidadMovimiento);
	    	bPos = Input.mousePosition;
	    }

	}
}
