using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    public int m_life;

    void ReceiveDamage(int damage) {
        m_life -= damage;
        if (m_life <= 0) {
            Destroy(gameObject);
        }
    }
}
