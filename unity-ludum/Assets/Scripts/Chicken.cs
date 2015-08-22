using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour {

	public static event FXManager.FxEvent OnStep;

	enum states {
		Idle,
		Wandering,
		Chasing,
		Angry
	}
	
	states state;
	
	void Start () {
		StartCoroutine(SMC());
	}

	IEnumerator SMC () {
		while (true) {
			yield return StartCoroutine(state.ToString());
		}
	}
	
	IEnumerator Idle () {
		while (state == states.Idle) {
			Debug.Log("Idle");
			yield return 0;
		}
	}
	
	IEnumerator Wandering () {
		while (state == states.Wandering) {
			yield return 0;
		}
	}
	
	IEnumerator Chasing () {
		while (state == states.Chasing) {
			yield return 0;
		}
	}
	
	IEnumerator Angry () {
		while (state == states.Angry) {
			yield return 0;
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
