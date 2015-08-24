using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public string nextScene;
	float waitTime = 2;
	
	void OnEnable () {
		Player.OnDeath += Player_OnDeath;
		NecklaceManager.OnLevelCompleted += OnLevelCompleted;
	}
	
	void OnDisable () {
		Player.OnDeath -= Player_OnDeath;
		NecklaceManager.OnLevelCompleted -= OnLevelCompleted;
	}
	
	void Player_OnDeath (Vector3 position) {
		Invoke("Menu", waitTime);
	}
	
	void OnLevelCompleted () {
		Invoke("NextScene", waitTime);
	}
	
	void NextScene () {
		Application.LoadLevel(nextScene);
	}
	
	void Menu () {
		Application.LoadLevel("Home");
	}
}
