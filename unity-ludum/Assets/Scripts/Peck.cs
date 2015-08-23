using UnityEngine;
using System.Collections;

public class Peck : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.tag == "ChickenBody") {
            other.SendMessageUpwards("ReceiveDamage", 1, SendMessageOptions.RequireReceiver);
        }
    }
}
