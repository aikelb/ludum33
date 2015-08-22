using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3 desiredOrientation;
	Vector3 currentVelocity;
	public float movementSpeed;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
	}
	
	void Update () {
		ReadInput();
		currentVelocity = currentVelocity * 0.8f + 0.2f * (desiredOrientation * movementSpeed * Time.deltaTime);
		transform.Translate(currentVelocity);
		desiredOrientation = Vector3.zero;
	}
	
	void ReadInput () {
		if (Input.GetKey(KeyCode.W))
			desiredOrientation += Vector3.up;
		if (Input.GetKey(KeyCode.S))
			desiredOrientation += Vector3.down;
		if (Input.GetKey(KeyCode.A))
			desiredOrientation += Vector3.left;
		if (Input.GetKey(KeyCode.D))
			desiredOrientation += Vector3.right;
		desiredOrientation.Normalize();
	}
	
}
