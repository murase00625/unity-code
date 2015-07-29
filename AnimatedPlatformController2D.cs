using UnityEngine;
using System.Collections;

public class AnimatedPlatformController2D : MonoBehaviour {
	private bool facingRight = true;
	private bool isJumping = false;
	
	// Flip this to true when the animation state machine is ready
	private bool animated = false;

	private Transform groundCheck;
	private bool grounded = false;
	private Animator anim;

	public float runForce = 350f;
	public float jumpForce = 500f;
	public float maxRunSpeed = 5f;
	public ParticleSystem ouch;

	void Awake() {
		groundCheck = transform.Find("groundCheck");
		if (animated) anim = GetComponent<Animator>();
		// ouch.Stop();
	}

	// Jumping requires all platforms and "jumpable" objects to be in a layer called Jumpable,
	// and an empty object at the character's "feet" called "groundCheck".
	// This is an object layer in Unity, NOT a sprite sorting layer!
	void Update() {
		float h = Input.GetAxis("Horizontal");

		if ((h > 0 && !facingRight) || (h < 0 && facingRight)) {
			Flip();
		}

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Jumpable"));

		if (Input.GetButtonDown("Jump") && grounded) {
			isJumping = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		if (animated) anim.SetFloat("Speed", Mathf.Abs(h));

		if (h * rigidbody2D.velocity.x < maxRunSpeed) {
			rigidbody2D.AddForce(Vector2.right * h * runForce);
		}

		if (isJumping) {
			if (animated) anim.SetTrigger("Jump");

			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			isJumping = false;
		}

		if (animated) {
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

			if (stateInfo.IsName("Base Layer.jump")) {
				if (grounded) {
					anim.SetTrigger("Touchground");
				}
			}
		}
	}

	void Flip() {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void StopPlayer() {
		gamestarted = false;
		print("Ouch! The player has faceplanted.");

		if (transform.localScale.x < 0) Flip();
		// ouch.Play();
		StartCoroutine("waitForRestart");
    }

    void EndPlay() {
    	gamestarted = false;
    	StartCoroutine("waitForRestart");      
    }

    IEnumerator waitForRestart() {
    	yield return new WaitForSeconds(5);
    	Application.LoadLevel(0);
    }

}