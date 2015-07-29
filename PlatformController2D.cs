using UnityEngine;
using System.Collections;
 
public class CharacterController2D : MonoBehaviour {
 
        private bool facingRight = true;
        private bool jump = false;
        private bool grounded = false;
        private bool gamestarted = false;
 
        public float currentRunSpeed = 0f;
        public float jumpForce = 500f;
        public float runForce = 350f;
        public float maxJump = 2f;
        public float maxRunSpeed = 5f;
        public ParticleSystem ouch;
        // Super Mario style jumping: Toggle this to true
        public bool changeDirectionsMidJump = false;
 
        // Use this for initialization
        void Start () {
                gamestarted = true;
                // ouch.Stop();
        }
       
        // Update is called once per frame, and is appropriate for things that don't require a ton of precision.
        void Update () {
                if (gamestarted) {
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
        

        // FixedUpdate is called much more quickly than Update, and is appropriate for realtime physics calculations.
        void FixedUpdate() {
                if (jump == true) {
                        if (Mathf.Abs(rigidbody2D.velocity.y) < maxJump)
                                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
 
                        if (Mathf.Abs(rigidbody2D.velocity.y) >= maxJump)
                                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxJump);
                        jump = false;
                }
 
                float h = Input.GetAxis("Horizontal");
 
                if (h * rigidbody2D.velocity.x < maxRunSpeed) {
                        if (changeDirectionsMidJump || grounded) {
                                rigidbody2D.AddForce(Vector2.right * h * runForce);
                        }
                }
 
                if (Mathf.Abs(rigidbody2D.velocity.x) > maxRunSpeed) {
                        rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxRunSpeed, rigidbody2D.velocity.y);
                }
                currentRunSpeed = rigidbody2D.velocity.x;
 
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
 
        void OnCollisionEnter2D (Collision2D hit) {
                grounded = true;
        }
 
}