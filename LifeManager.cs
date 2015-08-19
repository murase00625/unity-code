using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public string responseTag = "life";
	public int initialLives;
	public int initialHitPoints;
	public bool displayHits;
	public GameObject player;
	public GameObject spawnPoint;
	public int respawnCooldown;
	public int timeToRestart;
	public ParticleSystem ouch;

	private bool losingLife = false;
	private int lives;
	private int hitPoints;

	void Start() {
		string content = generateContent(initialLives, initialHitPoints);
		updateContent(content, responseTag);
		lives = initialLives;
		hitPoints = initialHitPoints;
		if (ouch != null) ouch.Stop();
	}

	string generateContent(int l, int h) {
		string content = "Lives: " + l;
		if (displayHits) content += ", HP: " + h;
		return content;
	}

	void updateContent(string content, string tag) {
		object[] args = new object[2];
		args[0] = tag;
		args[1] = content;
		player.SendMessage("setContentAsArray", args);
	}

	void inflictDamage(int x) {
		
		if (!losingLife) {
			hitPoints -= x;
			if (hitPoints <= 0) {
				hitPoints = 0;
				if (lives <= 0) {
					player.SendMessage("StopPlayer");
				} else {
					losingLife = true;
					StartCoroutine("waitForRespawn");		
				}
			}
		}
		string content = generateContent(lives, hitPoints);
		updateContent(content, responseTag);
	}

	void heal(int x) {
		hitPoints += x;
		string content = generateContent(lives, hitPoints);
		updateContent(content, responseTag);
	}

	IEnumerator waitForRespawn() {
		if (ouch != null) ouch.Play();
		if (player.transform.localScale.x < 0) player.SendMessage("Flip");
        player.SendMessage("ToggleFreeze");
        player.rigidbody2D.velocity = new Vector2(0f, player.rigidbody2D.velocity.y);

		yield return new WaitForSeconds(respawnCooldown);
		lives--;
		hitPoints = initialHitPoints;
		string content = generateContent(lives, hitPoints);
		updateContent(content, responseTag);
					
        if (ouch != null) {
			ouch.Stop();
			ouch.Clear();
		}
        Vector3 playerPosition = spawnPoint.transform.position;
		player.transform.position = playerPosition;
		losingLife = false;
		player.SendMessage("ToggleFreeze");
    }

    void StopPlayer() {
        player.SendMessage("ToggleFreeze");
        string message = "You lost! Waiting " + timeToRestart + " seconds to restart.";
        updateContent(message, responseTag);
		if (player.transform.localScale.x < 0) player.SendMessage("Flip");

        if (ouch != null) ouch.Play();
        StartCoroutine("waitForRestart");
    }

    void EndPlay() {
        player.SendMessage("ToggleFreeze");
        StartCoroutine("waitForRestart");      
    }

    IEnumerator waitForRestart() {
        yield return new WaitForSeconds(timeToRestart);
        Application.LoadLevel(0);
    }


}
