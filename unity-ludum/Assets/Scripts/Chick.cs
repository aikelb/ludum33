using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour {

    public static event FXManager.FxEvent OnDeath;

    void ReceiveDamage() {
        //GetComponent<Animator>().Play("Death");
        SendMessageUpwards("SetChickensAngry", SendMessageOptions.RequireReceiver);
        if (OnDeath != null) {
            OnDeath(transform.position);
        }
        Destroy(this);
    }
}
