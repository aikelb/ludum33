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
        Debug.Log("Gallinas furiosas");
        foreach (Chicken chicken in m_chickens) {
            chicken.PlayerKillChick();
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("He entrado en la zona");
        foreach(Chicken chicken in m_chickens) {
            chicken.PlayerInsideNest();
        }
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("He salido de la zona");
        foreach (Chicken chicken in m_chickens) {
            chicken.PlayerExitNest();
        }
    }
}
