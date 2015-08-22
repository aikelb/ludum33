using UnityEngine;
using System.Collections;

public class Chicken : MovingEntity {

	public static event FXManager.FxEvent OnDeath;
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
	
	public float attackDistance = 1f;
	
	[Header("Wandering Setings")]
	public float desireOrientationChance = 0.1f;
	public float idleChance = 0.5f;
	
	[Header("Chase Setings")]
	public float chaseMaxDistance = 2f;
	public float chaseSpeedModifier = 1.5f;
	
	[Header("Angry Setings")]
	public float angrySpeedModifier = 2.5f;
	
	void OnValidate () {
		player = FindObjectOfType<Player>().transform;
	}

	void Awake () {
		animatorChicken = GetComponent<Animator>();
	}
	
	void Start () {
		StartCoroutine(SMC());
	}

	IEnumerator SMC () {
		while (true) {
			Debug.Log("Starting state: " + state);
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
			animatorChicken.Play ("Up");
		} else if (Random.value < 0.75f) {
			myDesiredOrientation += Vector3.down;
			animatorChicken.Play ("Down");
		}

		if (Random.value < 0.5f) {
			myDesiredOrientation += Vector3.left;
			animatorChicken.Play ("Left");
		} else if (Random.value < 0.75f) {
			myDesiredOrientation += Vector3.right;
			animatorChicken.Play ("Right");
		}

		myDesiredOrientation.Normalize();
	}
	
	IEnumerator Chasing () {
		while (state == states.Chasing) {
			Vector3 towardsPlayer = player.position - transform.position;
			
			if (!alert && towardsPlayer.magnitude > chaseMaxDistance)
				state = states.Idle;
			
			myDesiredOrientation = towardsPlayer.normalized;
			desiredOrientation = myDesiredOrientation;
			Move(chaseSpeedModifier);
			
			yield return new WaitForFixedUpdate();
		}
	}
	
	IEnumerator Angry () {
		while (state == states.Angry) {
			Vector3 towardsPlayer = player.position - transform.position;
			
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
        Debug.Log("PlayerKillChick");
    }
	
    public void PlayerInsideNest() {
        Debug.Log("PlayerInsideNest");
    }

    public void PlayerExitNest() {
        Debug.Log("PlayerExitNest");
    }
	
}
