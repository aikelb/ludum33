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
	
}
