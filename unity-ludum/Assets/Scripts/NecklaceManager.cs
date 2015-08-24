using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecklaceManager : MonoBehaviour {

	public delegate void NecklaceEvent(int picks);
	public delegate void LevelCompleted();
	public static event LevelCompleted OnLevelCompleted; 

	public GameObject picoPrincipio;
	public GameObject picoMedio1;
	public GameObject picoMedio2;

	private int picksNumber;
	private int currentPick;

	// Use this for initialization
	void Start () {
		picksNumber = GameObject.FindObjectsOfType<Chick>().Length;
		currentPick = 0;
		PrepareNecklace();
	}

	void OnEnable() {
		Chick.RaiseNecklace += Chick_RaiseNecklace;
	}
	
	void OnDisable() {
		Chick.RaiseNecklace -= Chick_RaiseNecklace;
	}

	void Chick_RaiseNecklace(int picks) {
		AddPicksToNecklace(picks);
		currentPick += picks;
		if (currentPick >= picksNumber && OnLevelCompleted != null) {
			OnLevelCompleted();
		} 
	}

	void AddPicksToNecklace(int picks) {

	}

	void PrepareNecklace() {
		for (int i = 0; i < picksNumber; i++) {
			GameObject p = GameObject.Instantiate(picoPrincipio) as GameObject;
			p.transform.SetParent(transform);
		}
	}
}
