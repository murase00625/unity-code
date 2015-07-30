﻿using UnityEngine;
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

	private int lives;
	private int hitPoints;

	void Start() {
		string content = generateContent(initialLives, initialHitPoints);
		updateContent(content);
		lives = initialLives;
		hitPoints = initialHitPoints;
		if (ouch != null) ouch.Stop();
	}

	string generateContent(int l, int h) {
		string content = "Lives: " + l;
		if (displayHits) content += ", HP: " + h;
		return content;
	}

	void updateContent(string content) {
		object[] args = new object[2];
		args[0] = responseTag;
		args[1] = content;
		player.SendMessage("setContentAsArray", args);
	}

	void inflictDamage(int x) {
		hitPoints -= x;
		if (hitPoints <= 0) {
			hitPoints = 0;
			if (lives <= 0) {
				player.SendMessage("StopPlayer");
			} else {
				lives--;
				hitPoints = initialHitPoints;
				StartCoroutine("waitForRespawn");
				
			}
		}
		string content = generateContent(lives, hitPoints);
		updateContent(content);
	}

	IEnumerator waitForRespawn() {
        player.SendMessage("ToggleFreeze");
        if (ouch != null) ouch.Play();
		yield return new WaitForSeconds(respawnCooldown);
        Vector3 playerPosition = spawnPoint.transform.position;
		player.transform.position = playerPosition;
		if (ouch != null) ouch.Stop();
		player.SendMessage("ToggleFreeze");
    }

    void StopPlayer() {
        player.SendMessage("ToggleFreeze");
        string message = "You lost! Waiting " + timeToRestart + " seconds to restart.";
        updateContent(message);
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
