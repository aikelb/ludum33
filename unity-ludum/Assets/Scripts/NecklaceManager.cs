using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecklaceManager : MonoBehaviour {

	public delegate void NecklaceEvent(int picks);
	public delegate void LevelCompleted();
	public static event LevelCompleted OnLevelCompleted; 

	private int picksNumber;
	private int currentPick;
	
	public GameObject pickPrefab;

	// Use this for initialization
	void Start () {
		picksNumber = GameObject.FindObjectsOfType<Chick>().Length;
		currentPick = 0;
	}

	void OnEnable() {
		Chick.RaiseNecklace += Chick_RaiseNecklace;
	}
	
	void OnDisable() {
		Chick.RaiseNecklace -= Chick_RaiseNecklace;
	}

	void Chick_RaiseNecklace(int picks) {
		AddPicksToNecklace (picks);
		currentPick += picks;
		if (currentPick >= picksNumber && OnLevelCompleted != null) {
			OnLevelCompleted();
		} 
	}

	void AddPicksToNecklace(int picks) {
		for (int i = 0; i < picks; i++) {
			GameObject p = GameObject.Instantiate(pickPrefab) as GameObject;
			p.transform.SetParent(transform);
		}
	}
}
