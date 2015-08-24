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

    void OnTriggerStay(Collider other) {
        if (other.transform == transform)
            return;
        if (other.transform.parent == transform)
            return; 
        
        Vector3 towardsTarget = other.transform.position - transform.position;
        if (other.tag == "Wall") {
            transform.position -= currentVelocity.normalized * Time.deltaTime * 18;
        } else if ((other.tag == "ChickenBody" || other.tag == "Player") && towardsTarget.magnitude < 1.8f) {
            transform.position -= currentVelocity.normalized * Time.deltaTime * 8;
        }
    }
    
}