using UnityEngine;
using System.Collections;

public class Player : MovingEntity {
	
	public static event FXManager.FxEvent OnDeath;

	private Animator animatorPlayer;

	private float xAxis;
	private float yAxis;

	private enum playerDirection {
		isOnX,
		isOnY
	}

	private playerDirection direction;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
		animatorPlayer = GetComponent<Animator>();
	}
	
	void Update () {
		ReadInput();
		Move();
	}
	
	void ReadInput () {

		xAxis = Input.GetAxis("Horizontal");
		yAxis = Input.GetAxis("Vertical");

		//Movimiento en X
		if (xAxis != 0 && yAxis == 0) {
			direction = playerDirection.isOnX;
			animatorPlayer.SetBool ("isMoveSide", true);
			if (xAxis > 0) {
				desiredOrientation += Vector3.right;
				this.transform.localScale = new Vector3(1, 1, 1);
			} else {
				desiredOrientation += Vector3.left;
				this.transform.localScale = new Vector3(-1, 1, 1);
			}
		} else {
			animatorPlayer.SetBool ("isMoveSide", false);
		}
		//Moviemiento en Y
		if (yAxis != 0 && xAxis == 0) {
			direction = playerDirection.isOnY;
			this.transform.localScale = new Vector3 (1, 1, 1);
			if (yAxis > 0) {
				desiredOrientation += Vector3.up;
				animatorPlayer.SetBool ("isMoveUp", true);
			} else {
				desiredOrientation += Vector3.down;
				animatorPlayer.SetBool ("isMoveDown", true);
			}
		} else {
			animatorPlayer.SetBool ("isMoveUp", false);
			animatorPlayer.SetBool ("isMoveDown", false);
		}
		//Movimiento en diagonal.
		if (xAxis != 0 && yAxis != 0) {
			desiredOrientation += new Vector3(xAxis, yAxis, 0);
			if (direction == playerDirection.isOnX) {
				animatorPlayer.SetBool ("isMoveSide", true);
				if (xAxis > 0) {
					this.transform.localScale = new Vector3(1, 1, 1);
				} else {
					this.transform.localScale = new Vector3(-1, 1, 1);
				}
			} else {
				if (yAxis > 0) {
					animatorPlayer.SetBool ("isMoveUp", true);
				} else {
					animatorPlayer.SetBool ("isMoveDown", true);
				}
			}
		}

		desiredOrientation.Normalize();
	}
	
	void OnDestroy () {
		if (OnDeath != null)
			OnDeath(transform.position);
	}
	
}
