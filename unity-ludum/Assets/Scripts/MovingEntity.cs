using UnityEngine;

public class MovingEntity : MonoBehaviour {
	
	protected Vector3 desiredOrientation;
	protected Vector3 currentVelocity;
	public float movementSpeed = 1;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
	}
	
	protected void Move (float speedModifier) {
		currentVelocity = currentVelocity * 0.8f + 
			0.2f * (desiredOrientation * movementSpeed  * speedModifier * Time.deltaTime);
			
		transform.Translate(currentVelocity);
		desiredOrientation = Vector3.zero;
	}
	
	protected void Move () {
		Move(1f);
	}

    /*
    void OnTriggerStay(Collider other) {        
        if (other.tag == "Wall") {
            transform.position -= currentVelocity.normalized * Time.deltaTime * 8;
        }
    }
     */

    //Funciones para intentar controlar las colisiones, no son 100% fiables, habría que mejorarlas.
    void OnTriggerStay(Collider other) {
        if (gameObject.name != "Chicken") {
            if (other.tag == "Wall" || other.tag == "ChickenBody") {
                transform.position -= currentVelocity.normalized * Time.deltaTime * 8;
            }
        }
    }

    public void OnTriggerStayChild(Collider other) {
        if (other.tag == "Wall" || other.tag == "Player") {
            transform.position -= currentVelocity.normalized * Time.deltaTime * 8;
        }
    }
}