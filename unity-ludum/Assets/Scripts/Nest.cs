using UnityEngine;
using System.Collections;

public class Nest : MonoBehaviour {

    [SerializeField]
    Chicken[] m_chickens;

	// Use this for initialization
    void OnValidate() {
        m_chickens = GetComponentsInChildren<Chicken>();
    }

    void SetChickensAngry() {
        foreach (Chicken chicken in m_chickens) {
            if (chicken != null)
                chicken.PlayerKillChick();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            foreach(Chicken chicken in m_chickens) {
                if (chicken != null)
                    chicken.PlayerInsideNest();
            }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
            foreach (Chicken chicken in m_chickens) {
                if (chicken != null)
                    chicken.PlayerExitNest();
            }
    }
}
