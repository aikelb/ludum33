using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour {

    public static event FXManager.FxEvent OnDeath;
    public static event ScoreManager.ScoreEvent RaiseScore;
    
    void Awake () {
        GetComponent<AudioSource>().PlayDelayed(Random.value);
    }

    void OnDestroy() {
        SendMessageUpwards("SetChickensAngry", SendMessageOptions.RequireReceiver);
        if (RaiseScore != null) {
            RaiseScore(100);
        }
        if (OnDeath != null) {
            OnDeath(transform.position);
        }
    }
}
