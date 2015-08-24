using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public delegate void ScoreEvent(int points);

    int m_score = 0;

    void OnEnable() {
        Chicken.RaiseScore += Chicken_RaiseScore;
        Chick.RaiseScore += Chick_RaiseScore;
    }

    void OnDisable() {
        Chicken.RaiseScore -= Chicken_RaiseScore;
        Chick.RaiseScore -= Chick_RaiseScore;
    }

    void Chicken_RaiseScore(int points) {
        m_score += points;
    }

    void Chick_RaiseScore(int points) {
        m_score += points;
    }

    void Update() {
        GameObject.Find("PointsScore").GetComponent<Text>().text = m_score.ToString();
    }
}
