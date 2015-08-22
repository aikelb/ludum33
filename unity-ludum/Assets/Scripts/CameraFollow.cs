using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject m_objetive;
	
	// Update is called once per frame
	void Update () {
        transform.position = m_objetive.transform.position;
	}
}
