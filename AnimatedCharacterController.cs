using UnityEngine;
using System.Collections;

public class AnimatedCharacterController : MonoBehaviour {
	private bool facingRight = true;
	private bool isJumping = false;

	private Transform groundCheck;
	private bool grounded = false;
	private Animator anim;

	public float runForce = 350f;
	public float jumpForce = 500f;
	public float maxRunSpeed = 5f;

	void Awake() {
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}

	void Update() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown("Jump") && grounded) {
			isJumping = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(h));

		if (h * rigidbody2D.velocity.x < maxRunSpeed) {
			rigidbody2D.AddForce(Vector2.right * h * runForce);
		}

		if ((h > 0 && !facingRight) || (h < 0 && facingRight) {
			Flip();
		}

		if (isJumping) {
			anim.SetTrigger("Jump");

			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName("Base Layer.jump")) {
			if (grounded) {
				anim.SetTrigger("Touchground");
			}
		}
	}

	void Flip() {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}