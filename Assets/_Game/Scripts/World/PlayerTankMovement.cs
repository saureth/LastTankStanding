using UnityEngine;
using System.Collections;

public class PlayerTankMovement : MonoBehaviour {

	[Tooltip("If is not player 2, is player 1")]
	public bool player2;
	string playerName;
	float speed;
	public float maxForwardSpeed;
	public float acceleration;
	public float deceleration;
	public float rotationSpeed;
	public Animator cameraAnim;

	void Start () {
		if (player2)
			playerName = "p2_";
		else
			playerName = "p1_";
	}
	
	void Update () {
		Movement ();
		cameraAnim.SetFloat ("Velocity", GetComponent<Rigidbody>().velocity.magnitude);
	}

	void FixedUpdate () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

	public void Movement () {

		if (Input.GetAxis (playerName + "Vertical") > 0.2f) {
			speed = Mathf.Lerp (speed, maxForwardSpeed, Time.deltaTime * acceleration);
		} else if (Input.GetAxis (playerName + "Vertical") < -0.2f) {
			speed = Mathf.Lerp (speed, -maxForwardSpeed, Time.deltaTime * acceleration);
		} else {
			speed = Mathf.Lerp (speed, 0, Time.deltaTime * deceleration);
		}

		// Rotation w/o accel or physics
		transform.Rotate (Vector3.up, Input.GetAxis(playerName + "Horizontal") * rotationSpeed * Time.deltaTime);
	}
}
