using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public string nextScene;
	float waitTime = 2;
	
	void OnEnable () {
		Player.OnDeath += Player_OnDeath;
	}
	
	void OnDisable () {
		Player.OnDeath -= Player_OnDeath;
	}
	
	void Player_OnDeath (Vector3 position) {
		Invoke("Home", waitTime);
	}
	
}
