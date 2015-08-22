using UnityEngine;

public class MovingEntity : MonoBehaviour {
	
	protected Vector3 desiredOrientation;
	protected Vector3 currentVelocity;
	public float movementSpeed;

	void Awake () {
		desiredOrientation = currentVelocity = Vector3.zero;
	}
	
	protected void Move () {
		currentVelocity = currentVelocity * 0.8f + 0.2f * (desiredOrientation * movementSpeed * Time.deltaTime);
		transform.Translate(currentVelocity);
		desiredOrientation = Vector3.zero;
	}
	
}