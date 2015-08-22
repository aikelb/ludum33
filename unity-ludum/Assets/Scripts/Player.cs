using UnityEngine;
using System.Collections;

public class Player : MovingEntity {

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
	}
	
	void Update () {
		ReadInput();
		Move();
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
