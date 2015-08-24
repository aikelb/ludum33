using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeUIManager : MonoBehaviour {

    public delegate void LifePlayerEvent();
    public Sprite[] life_array;
    private int m_cont = 0;
    private Image m_lifeUi;

    void Start() {
        m_lifeUi = GameObject.Find("LifeUI").GetComponent<Image>();
        m_lifeUi.sprite = life_array[m_cont];
    }

    void OnEnable() {
        Life.DecreseLifeUI += Life_DecreseLifeUI;
    }

    void OnDisable() {
        Life.DecreseLifeUI -= Life_DecreseLifeUI;
    }

    void Life_DecreseLifeUI() {
        m_cont++;
        if (m_cont <= life_array.Length) {
            m_lifeUi.sprite = life_array[m_cont];
        }
        else {
            Debug.LogError("Intentas acceder a una imagen de vida que no existe, comprueba vida de player", this);
        }
    }
}
