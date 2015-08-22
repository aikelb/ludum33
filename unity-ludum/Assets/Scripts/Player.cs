using UnityEngine;
using System.Collections;

public class Player : MovingEntity {
	
	public static event FXManager.FxEvent OnDeath;

	private Animator animatorPlayer;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
		animatorPlayer = GetComponent<Animator>();
	}
	
	void Update () {
		ReadInput();
		Move();
	}
	
	void ReadInput () {
		if (Input.GetKey(KeyCode.W)) {
			desiredOrientation += Vector3.up;
			animatorPlayer.Play("Up");
		}
		if (Input.GetKey(KeyCode.S)) {
			desiredOrientation += Vector3.down;
			animatorPlayer.Play("Down");
		}
		if (Input.GetKey(KeyCode.A)) {
			desiredOrientation += Vector3.left;
			animatorPlayer.Play("Left");
		}
		if (Input.GetKey(KeyCode.D)) {
			desiredOrientation += Vector3.right;
			animatorPlayer.Play("Right");
		}
		if (Input.GetKey(KeyCode.Space)) {
			animatorPlayer.Play("Attack");
		}
		desiredOrientation.Normalize();
	}
	
	void OnDestroy () {
		if (OnDeath != null)
			OnDeath(transform.position);
	}
	
}
