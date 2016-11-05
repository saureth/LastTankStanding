using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	public GameObject objetoDesactivar;
	public GameObject objetoActivar;

	public void destroy(){
		objetoDesactivar.SetActive (false);
		objetoActivar.SetActive (true);
	}
}
