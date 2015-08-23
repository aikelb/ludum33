using UnityEngine;
using System.Collections;

public class Prueba : MonoBehaviour {

    void OnTriggerStay(Collider other) {
        transform.parent.gameObject.GetComponent<MovingEntity>().OnTriggerStayChild(other);
    }
}
