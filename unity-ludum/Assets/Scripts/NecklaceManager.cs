using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecklaceManager : MonoBehaviour {

	public delegate void NecklaceEvent(int picks);
	public delegate void LevelCompleted();
	public static event LevelCompleted OnLevelCompleted; 
	public GameObject picoVacioPrincipio;
	public GameObject picoVacioMedio1;
	public GameObject picoVacioMedio2;
	public GameObject picoLlenoPrincipio;
	public GameObject picoLlenoMedio1;
	public GameObject picoLlenoMedio2;

	private int picksNumber;
	private int currentPick;
	private GameObject[] peaksNecklace;

	// Use this for initialization
	void Start () {
		picksNumber = GameObject.FindObjectsOfType<Chick>().Length;
		currentPick = 0;
		peaksNecklace = new GameObject[picksNumber];
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
		if (currentPick >= picksNumber && OnLevelCompleted != null) {
			OnLevelCompleted();
		} 
	}

	void AddPicksToNecklace(int picks) {
		for (int i = 0; i < picks; i++) {
			GameObject fullPeak;
			if (currentPick == 0) {
				fullPeak = GameObject.Instantiate(picoLlenoPrincipio) as GameObject;
			} else {
				if (currentPick%2 == 0) {
					fullPeak = GameObject.Instantiate(picoLlenoMedio2) as GameObject;
				} else {
					fullPeak = GameObject.Instantiate(picoLlenoMedio1) as GameObject;
				}
			}
			peaksNecklace[currentPick].GetComponent<Image>().sprite = fullPeak.GetComponent<Image>().sprite;
			currentPick ++;
		}
	}

	void PrepareNecklace() {
		for (int i = 0; i < picksNumber; i++) {
			GameObject emptyPeak;
			if (i == 0) {
				emptyPeak = GameObject.Instantiate(picoVacioPrincipio) as GameObject;
			} else {
				if (i%2 == 0) {
					emptyPeak = GameObject.Instantiate(picoVacioMedio2) as GameObject;
				} else {
					emptyPeak = GameObject.Instantiate(picoVacioMedio1) as GameObject;
				}
			}
			emptyPeak.transform.SetParent(transform);
			peaksNecklace[i] = emptyPeak;
		}
	}
}
