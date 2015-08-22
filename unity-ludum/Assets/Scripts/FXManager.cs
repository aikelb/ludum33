using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	public delegate void FxEvent(Vector3 position);

    void OnEnable() {
        Chick.OnDeath += Chick_OnDeath;
    }

    void OnDisable() {
        Chick.OnDeath -= Chick_OnDeath;
    }

    void Chick_OnDeath(Vector3 position) {

    }

}
