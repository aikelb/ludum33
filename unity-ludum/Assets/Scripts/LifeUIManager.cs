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
        m_lifeUi.sprite = life_array[m_cont];
    }
}
