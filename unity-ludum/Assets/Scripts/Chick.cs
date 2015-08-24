using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour {

    public static event FXManager.FxEvent OnDeath;
    public static event ScoreManager.ScoreEvent RaiseScore;
	public static event NecklaceManager.NecklaceEvent RaiseNecklace;

    void OnDestroy() {
        SendMessageUpwards("SetChickensAngry", SendMessageOptions.RequireReceiver);
        if (RaiseScore != null) {
            RaiseScore(100);
        }
		if (RaiseNecklace != null) {
			RaiseNecklace(1);
		}
        if (OnDeath != null) {
            OnDeath(transform.position);
        }
    }
}
