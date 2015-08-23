using UnityEngine;
using System.Collections;

public class Player : MovingEntity {
	
	public static event FXManager.FxEvent OnDeath;

	private Animator playerAnimator;

	private float xAxis;
	private float yAxis;

	private enum PlayerDirection {
		isOnX,
		isOnY
	}

	private enum PlayerViewDirection {
		isViewUp,
		isViewDown
	}

	private PlayerDirection direction;
	private PlayerViewDirection viewDirection;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
		playerAnimator = GetComponent<Animator>();
	}
	
	void Update () {
		ReadInput();
		Move();
		Attack();
	}
	
	void ReadInput () {

		xAxis = Input.GetAxis("Horizontal");
		yAxis = Input.GetAxis("Vertical");

		//Movimiento en X
		if (xAxis != 0 && yAxis == 0) {
			direction = PlayerDirection.isOnX;
			playerAnimator.SetBool ("isMoveSide", true);
			if (xAxis > 0) {
				desiredOrientation += Vector3.right;
				this.transform.localScale = new Vector3(1, 1, 1);
			} else {
				desiredOrientation += Vector3.left;
				this.transform.localScale = new Vector3(-1, 1, 1);
			}
		} else {
			playerAnimator.SetBool ("isMoveSide", false);
		}
		//Moviemiento en Y
		if (yAxis != 0 && xAxis == 0) {
			direction = PlayerDirection.isOnY;
			this.transform.localScale = new Vector3 (1, 1, 1);
			if (yAxis > 0) {
				desiredOrientation += Vector3.up;
				playerAnimator.SetBool ("isMoveUp", true);
				viewDirection = PlayerViewDirection.isViewUp;
			} else {
				desiredOrientation += Vector3.down;
				playerAnimator.SetBool ("isMoveDown", true);
				viewDirection = PlayerViewDirection.isViewDown;
			}
		} else {
			playerAnimator.SetBool ("isMoveUp", false);
			playerAnimator.SetBool ("isMoveDown", false);
		}
		//Movimiento en diagonal.
		if (xAxis != 0 && yAxis != 0) {
			desiredOrientation += new Vector3(xAxis, yAxis, 0);
			if (direction == PlayerDirection.isOnX) {
				playerAnimator.SetBool ("isMoveSide", true);
				if (xAxis > 0) {
					this.transform.localScale = new Vector3(1, 1, 1);
				} else {
					this.transform.localScale = new Vector3(-1, 1, 1);
				}
			} else {
				if (yAxis > 0) {
					playerAnimator.SetBool ("isMoveUp", true);
					viewDirection = PlayerViewDirection.isViewUp;
				} else {
					playerAnimator.SetBool ("isMoveDown", true);
					viewDirection = PlayerViewDirection.isViewDown;
				}
			}
		}

		desiredOrientation.Normalize();
	}

	void Attack() {
		//Atacar a los lados.
		if (direction == PlayerDirection.isOnX && playerAnimator.GetBool("isAttackSide") == false) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				playerAnimator.SetBool("isAttackSide", true);
				Invoke("StopAttack",0.4f);
			}
		}
		//Atacar hacia arriba o hacia abajo.
		if (direction == PlayerDirection.isOnY) {
			if (viewDirection == PlayerViewDirection.isViewUp && playerAnimator.GetBool("isAttackUp") == false) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					playerAnimator.SetBool("isAttackUp", true);
					Invoke("StopAttack",0.4f);
				}
			}
			if (viewDirection == PlayerViewDirection.isViewDown && playerAnimator.GetBool("isAttackDown") == false) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					playerAnimator.SetBool("isAttackDown", true);
					Invoke("StopAttack",0.4f);
				}
			}
		}
	}

	void StopAttack() {
		playerAnimator.SetBool("isAttackSide", false);
		playerAnimator.SetBool("isAttackUp", false);
		playerAnimator.SetBool("isAttackDown", false);
	}

	void OnDestroy () {
		if (OnDeath != null)
			OnDeath(transform.position);
	}
	
}
