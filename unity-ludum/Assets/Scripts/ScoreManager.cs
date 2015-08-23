using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public delegate void ScoreEvent(int points);

    int m_score = 0;

    void OnEnable() {
        FXManager.RaiseScore += FXManager_RaiseScore;
    }

    void OnDisable() {
        FXManager.RaiseScore += FXManager_RaiseScore;
    }

    void FXManager_RaiseScore(int points) {
        m_score += points;
    }

    void Update() {
        GameObject.Find("PointsScore").GetComponent<Text>().text = m_score.ToString();
    }
}
