using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject m_objetive;
    public Vector3 m_distObjective;
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = m_objetive.transform.position + m_distObjective;
	}
}
