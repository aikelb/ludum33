using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecklaceManager : MonoBehaviour {

	public delegate void NecklaceEvent(int picks);

	private int picksNumber;
	private int currentPick;

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
	}

	// Update is called once per frame
	void Update () {
	}

	void PrepareNecklace() {
	}

	void AddPicksToNecklace(int picks) {
		for (int i = 0; i < picks; i++) {
		}
	}
}
