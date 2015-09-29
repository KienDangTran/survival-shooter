using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed = 6f; // The speed that the player will move at.
	
	private int floorMark; // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	private float camRayLength = 100f;  // The length of the ray from the camera into the scene.
	private Vector3 movement; // The vector to store the direction of the player's movement.
	private Rigidbody rigidbody; // Reference to the player's Rigidbody component.
	private Animator animator;  // Reference to the player's Animator component.

	void Awake ()
	{
		// Create a layer mask for the floor layer.	
		this.floorMark = LayerMask.GetMask ("Floor");

		// Set up references.
		this.rigidbody = this.GetComponent<Rigidbody> ();
		this.animator = this.GetComponent<Animator> ();
	}

	void FixedUpdate ()
	{
		// Store the input axes.
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		this.MovePlayer (horizontal, vertical);
		this.TurnPlayer ();
		this.AnimatePlayer (horizontal, vertical);
	}


	/**
	 * Move the player around the scene.
	 */
	private void MovePlayer (float horizontal, float vertical)
	{
		// Set the movement vector based on the axis input.
		this.movement.Set (horizontal, 0f, vertical);
		// Normalise the movement vector and make it proportional to the speed per second.
		this.movement = this.movement.normalized * this.speed * Time.deltaTime;
		// Move the player to it's current position plus the movement.
		this.rigidbody.MovePosition (this.rigidbody.position + this.movement);
	}

	/**
	 * Turn the player to face the mouse cursor.
	 */
	private void TurnPlayer ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit; // Create a RaycastHit variable to store information about what was hit by the ray.

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast (camRay, out floorHit, this.camRayLength, this.floorMark)) {
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - this.transform.position;
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newPlayerRotation = Quaternion.LookRotation (playerToMouse);
			// Set the player's rotation to this new rotation.
			this.rigidbody.MoveRotation (newPlayerRotation);
		}
	}

	/**
	 * Animate the player.
	 */
	private void AnimatePlayer (float horizontal, float vertical)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool isWalking = horizontal != 0f || vertical != 0f;
		// Tell the animator whether or not the player is walking by set "IsWalking" parameter
		this.animator.SetBool ("IsWalking", isWalking);
	}
}
