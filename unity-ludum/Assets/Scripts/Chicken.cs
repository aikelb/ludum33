using UnityEngine;
using System.Collections;

public class Chicken : MovingEntity {

	public static event FXManager.FxEvent OnDeath;
    public static event ScoreManager.ScoreEvent RaiseScore;

	public static event FXManager.FxEvent OnAttack;
	
	private Animator animatorChicken;

	enum states {
		Idle,
		Wandering,
		Chasing,
		Angry
	}
	
	states state;
	
	[SerializeField]
	Transform player;
	bool alert = false;
	Vector3 myDesiredOrientation = Vector3.zero;
	
	public float attackDistance = 2.5f;
	bool attacking = false;
	float lastAttack = 0;
	
	float desireOrientationChance = 0.05f;
	float idleChance = 0.005f;
	
	float chaseMaxDistance = 9f;
	float chaseSpeedModifier = 1.5f;
	
	float angrySpeedModifier = 2.5f;
	
	void OnValidate () {
		Player p = FindObjectOfType<Player>();
		if (p != null)
			player = p.transform;
	}

	void Awake () {
		animatorChicken = GetComponent<Animator>();
	}
	
	void Start () {
		StartCoroutine(SMC());
	}

	IEnumerator SMC () {
		while (true) {
			yield return StartCoroutine(state.ToString());
		}
	}
	
	IEnumerator Idle () {
		float elapsedTime = 0;
		float waitTime = Random.Range(0.5f, 2f);
		while (state == states.Idle && elapsedTime < waitTime) {
			elapsedTime += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		state = states.Wandering;
	}
	
	IEnumerator Wandering () {
		DesireOrientation();
		while (state == states.Wandering) {
			if (Random.value < desireOrientationChance)
				DesireOrientation();	
			
			if (myDesiredOrientation == Vector3.zero) {
				state = states.Idle;
			} else if (Random.value < idleChance)
				state = states.Idle;
				
			desiredOrientation = myDesiredOrientation;
			Move();
			yield return new WaitForFixedUpdate();
		}
	}
	
	void DesireOrientation () {
		if (Random.value < 0.5f) {
			myDesiredOrientation += Vector3.up;
		} else if (Random.value < 0.75f) {
			myDesiredOrientation += Vector3.down;
		}

		if (Random.value < 0.5f) {
			myDesiredOrientation += Vector3.left;
			this.transform.localScale = new Vector3(-1, 1, 1);
		} else if (Random.value < 0.75f) {
			myDesiredOrientation += Vector3.right;
			this.transform.localScale = new Vector3(1, 1, 1);
		}
		myDesiredOrientation.Normalize();
	}
	
	void LateUpdate () {
		if (myDesiredOrientation.x != 0) {
			animatorChicken.SetBool ("isMoveSide", true);
			animatorChicken.SetBool("isAttackSide", attacking);
			
			if (myDesiredOrientation.x > 0)
				transform.localScale = new Vector3(1, 1, 1);
			else
				transform.localScale = new Vector3(-1, 1, 1);
		}
		if ((Mathf.Abs(myDesiredOrientation.y) > 0.5f) && (Mathf.Abs(myDesiredOrientation.y) > Mathf.Abs(myDesiredOrientation.x))) {
			animatorChicken.SetBool ("isMoveSide", false);
			animatorChicken.SetBool("isAttackSide", false);
			if (myDesiredOrientation.y != 0) {
				if (myDesiredOrientation.y > 0) {
					animatorChicken.SetBool ("isMoveUp", true);
					animatorChicken.SetBool ("isMoveDown", false);
					animatorChicken.SetBool ("isAttackUp", attacking);
				} else {
					animatorChicken.SetBool ("isMoveUp", false);
					animatorChicken.SetBool ("isMoveDown", true);
					animatorChicken.SetBool ("isAttackDown", attacking);
				}
			}
		} else {
			animatorChicken.SetBool ("isMoveUp", false);
			animatorChicken.SetBool ("isMoveDown", false);
			if (myDesiredOrientation.x == 0)
				animatorChicken.SetBool ("isAttackSide", false);
			animatorChicken.SetBool ("isAttackUp", false);
			animatorChicken.SetBool ("isAttackDown", false);
		}
		
		if (state == states.Idle) {
			animatorChicken.SetBool ("isMoveSide", false);
			animatorChicken.SetBool ("isMoveUp", false);
			animatorChicken.SetBool ("isMoveDown", false);
			animatorChicken.SetBool ("isAttackSide", false);
			animatorChicken.SetBool ("isAttackUp", false);
			animatorChicken.SetBool ("isAttackDown", false);
		}
		
		if (attacking && (Time.time - lastAttack) > 0.45f) {
			if (OnAttack != null)
				OnAttack(transform.position);
			lastAttack = Time.time;
			RaycastHit hit;
			if (Physics.Raycast(transform.position, myDesiredOrientation.normalized, out hit, 1.4f)) {
				if (hit.collider.tag == "Player")
					hit.transform.SendMessage("ReceiveDamage", 1);
			}
		}
	}
	
	IEnumerator Chasing () {
		while (state == states.Chasing) {
			if (player == null)
				yield break;
			
			Vector3 towardsPlayer = player.position - transform.position;
			
			if (!alert && towardsPlayer.magnitude > chaseMaxDistance)
				state = states.Idle;
			
			attacking = (towardsPlayer.magnitude < attackDistance);
			
			myDesiredOrientation = towardsPlayer.normalized;
			desiredOrientation = myDesiredOrientation;
			Move(chaseSpeedModifier);
			
			yield return new WaitForFixedUpdate();
		}
	}
	
	IEnumerator Angry () {
		while (state == states.Angry) {
			Vector3 towardsPlayer = player.position - transform.position;
			if (player == null)
				yield break;
			attacking = (towardsPlayer.magnitude < attackDistance);
			
			myDesiredOrientation = towardsPlayer.normalized;
			desiredOrientation = myDesiredOrientation;
			Move(angrySpeedModifier);
			
			yield return new WaitForFixedUpdate();
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if (state != states.Angry) {
				state = states.Chasing;
			}
		}
	}

    public void PlayerKillChick() {
        state = states.Angry;
    }
	
    public void PlayerInsideNest() {
        if (state != states.Angry) {
			state = states.Chasing;
			alert = true;
		}
    }

    public void PlayerExitNest() {
        alert = false;
    }
	
	void OnDestroy () {
        if (RaiseScore != null)
            RaiseScore(50);
		if (OnDeath != null)
			OnDeath(transform.position);
	}
	
	void OnEnable () {
		Player.OnDeath += Player_OnDeath;
	}
	
	void OnDisable () {
		Player.OnDeath -= Player_OnDeath;
	}
	
	void Player_OnDeath (Vector3 position) {
		state = states.Idle;
		StopCoroutine(SMC());
		attacking = false;
	}
	
}
