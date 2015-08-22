using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour {

    public static event FXManager.FxEvent OnDeath;

    void OnDestroy() {
        SendMessageUpwards("SetChickensAngry", SendMessageOptions.RequireReceiver);
        if (OnDeath != null) {
            OnDeath(transform.position);
        }
    }
}
