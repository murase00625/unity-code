using UnityEngine;
using System.Collections;

public class SimplePlatformController2D : MonoBehaviour {

	private bool facingRight = true;
    private bool jump = false;
    private bool gamestarted = false;
	private bool grounded = true;
	
	public float currentRunSpeed = 0f;
    public float jumpForce = 400f;
    public float runForce = 300f;
    public float maxRunSpeed = 5f;

    // Super Mario style jumping: Toggle this to true to allow redirection mid-jump.
    public bool changeDirectionsMidJump = false;

	// Use this for initialization
	void Start () {
		gamestarted = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestarted) {
			// basic control dispatching, mostly jump and turning
			float h = Input.GetAxis("Horizontal");

            if ((h > 0 && !facingRight) || (h < 0 && facingRight)) {
                Flip();
            }

			if (Input.GetKeyDown("space") && jump == false) {
                if (grounded == true) {
                    jump = true;
                    grounded = false;
                }
            }
		}
	}

	void FixedUpdate() {
		if (gamestarted) {
			// Physics calculations go here!
			if (jump) {
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }

            float h = Input.GetAxis("Horizontal");
            float speed = Mathf.Abs(rigidbody2D.velocity.x);

            if (speed < maxRunSpeed) {
                float asymptoticForce = runForce * (1 - speed/maxRunSpeed);
                if (changeDirectionsMidJump || grounded)
                    rigidbody2D.AddForce(Vector2.right * h * asymptoticForce);
            }
            currentRunSpeed = rigidbody2D.velocity.x;

		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		grounded = true;
	}

	void Flip() {
        facingRight = !facingRight;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

}
