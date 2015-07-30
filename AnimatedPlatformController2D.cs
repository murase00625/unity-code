using UnityEngine;
using System.Collections;

public class AnimatedPlatformController2D : MonoBehaviour {
    private bool facingRight = true;
    private bool isJumping = false;
    private bool gamestarted = false;

    // Flip this to true when the animation state machine is ready
    private bool animated = false;

    private Transform groundCheckLeft, groundCheckRight;
    private bool grounded = false;
    private Animator anim;

    public float runForce = 350f;
    public float jumpForce = 500f;
    public float maxRunSpeed = 5f;
    public float currentRunSpeed;
    public float timeToRestart = 3f;
    public bool changeDirectionsMidJump = false;
    public ParticleSystem ouch;

    void Awake() {
        gamestarted = true;
        groundCheckLeft = transform.Find("groundCheckLeft");
        groundCheckRight = transform.Find("groundCheckRight");
        if (animated) anim = GetComponent<Animator>();
        if (ouch != null) ouch.Stop();
    }

    // Jumping requires all platforms and "jumpable" objects to be in a layer called Jumpable,
    // and two empty objects at the character's "feet" called "groundCheckLeft" and "groundCheckRight".
    // Jumpable must be an object layer in Unity, NOT a sprite sorting layer!
    void Update() {
        currentRunSpeed = Mathf.Abs(rigidbody2D.velocity.x);
        if (gamestarted) {
            float h = Input.GetAxis("Horizontal");

            if ((h > 0 && !facingRight) || (h < 0 && facingRight)) {
                Flip();
            }
            RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckLeft.position, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Jumpable"));
            RaycastHit2D hitRight = Physics2D.Raycast(groundCheckRight.position, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Jumpable"));
            grounded = hitLeft.collider != null || hitRight.collider != null;

            if (Input.GetButtonDown("Jump") && grounded) {
                isJumping = true;
            }
        }
    }

    void FixedUpdate() {
        if (gamestarted) {
            float h = Input.GetAxis("Horizontal");

            if (animated) anim.SetFloat("Speed", Mathf.Abs(h));
            float speed = Mathf.Abs(rigidbody2D.velocity.x);

            if (speed < maxRunSpeed) {
                float asymptoticForce = runForce * (1 - speed/maxRunSpeed);
                if (changeDirectionsMidJump || grounded)
                	rigidbody2D.AddForce(Vector2.right * h * asymptoticForce);
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
        if (ouch != null) ouch.Play();
        StartCoroutine("waitForRestart");
    }

    void EndPlay() {
        gamestarted = false;
        StartCoroutine("waitForRestart");      
    }

    IEnumerator waitForRestart() {
        yield return new WaitForSeconds(timeToRestart);
        Application.LoadLevel(0);
    }

}