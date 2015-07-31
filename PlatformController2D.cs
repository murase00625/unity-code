using UnityEngine;
using System.Collections;

public class PlatformController2D : MonoBehaviour {

    private bool facingRight = true;
    private bool jump = false;
    private bool grounded = false;
    private bool gamestarted = false;
    private Transform groundCheckLeft, groundCheckRight;

    public float timeToRestart = 3f;
    public float currentRunSpeed = 0f;
    public float jumpForce = 500f;
    public float runForce = 350f;
    public float maxRunSpeed = 5f;
    public ParticleSystem ouch;
    // Super Mario style jumping: Toggle this to true to allow redirection mid-jump.
    public bool changeDirectionsMidJump = false;

    public DisplayText dt;

    // Use this for initialization
    void Start () {
        gamestarted = true;
        groundCheckLeft = transform.Find("groundCheckLeft");
        groundCheckRight = transform.Find("groundCheckRight");
        if (ouch != null) ouch.Stop();
    }

    // Update is called once per frame, and is appropriate for things that don't require a ton of precision.
    void Update () {
        if (gamestarted) {
            float h = Input.GetAxis("Horizontal");

            if ((h > 0 && !facingRight) || (h < 0 && facingRight)) {
                Flip();
            }

            RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckLeft.position, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Jumpable"));
            RaycastHit2D hitRight = Physics2D.Raycast(groundCheckRight.position, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Jumpable"));
            grounded = hitLeft.collider != null || hitRight.collider != null;

            if (Input.GetKeyDown("space") && jump == false) {
                if (grounded == true) {
                    jump = true;
                }
            }

        }
    }


    // FixedUpdate is called much more quickly than Update, and is appropriate for realtime physics calculations.
    void FixedUpdate() {
        if (gamestarted) {
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

    void Flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void ToggleFreeze() {
        gamestarted = !gamestarted;
    }

}