using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
    
    public static event FXManager.FxEvent OnDamage;

    public int m_life;

    void ReceiveDamage(int damage) {
        if (OnDamage != null)
            OnDamage(transform.position);
        
        m_life -= damage;
        if (m_life <= 0) {
            Destroy(gameObject);
        }
    }
}
