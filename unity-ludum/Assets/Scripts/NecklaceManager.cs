using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecklaceManager : MonoBehaviour {

	public delegate void NecklaceEvent(int picks);
	public Sprite[] necklaceSprites;
	
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
		AddPicksToNecklace (picks);
	}

	// Update is called once per frame
	void Update () {
	}

	void PrepareNecklace() {
		for (int i = 0; i < picksNumber; i++) {
			Image neckaceImage = gameObject.AddComponent<Image>();
			if (i == 0) {
				neckaceImage.sprite = necklaceSprites[0];
			} else {
				if (i%2 == 0) {
					neckaceImage.sprite = necklaceSprites[1];
				} else {
					neckaceImage.sprite = necklaceSprites[2];
				}
			}
		}
	}

	void AddPicksToNecklace(int picks) {
		for (int i = 0; i < picks; i++) {
		}
	}
}
