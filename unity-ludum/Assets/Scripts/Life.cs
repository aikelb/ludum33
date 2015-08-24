using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    public static event LifeUIManager.LifePlayerEvent DecreseLifeUI;

    public int m_life;

    void ReceiveDamage(int damage) {
        m_life -= damage;
        if ((gameObject.tag == "Player") && (m_life > 0)) {
            if (DecreseLifeUI != null) {
                DecreseLifeUI();
            }
        }
        if (m_life <= 0) {
            Destroy(gameObject);
        }
    }
}
