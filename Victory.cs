using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	public GameObject player;
	public ParticleSystem happy;
	public string message;
	public string messageTag;
	public int timeToNextLevel = 5;
	public int nextLevel = 0;
	public int targetScore = 10;
	CollectorBehavior cb;

	void Start() {
		cb = GetComponent<CollectorBehavior>();
		if (happy != null) {
			happy.Stop();
		}
	}

	void onVictory() {
		object[] obj = new object[2];
		obj[0] = messageTag;
		obj[1] = message;
		player.SendMessage("setContentAsArray", obj);
		player.SendMessage("ToggleFreeze");
		if (happy != null) {
			happy.Play();
		}
		StartCoroutine("waitForNextLevel", nextLevel);
	}

	IEnumerator waitForNextLevel(int levelID) {
        yield return new WaitForSeconds(timeToNextLevel);
        Application.LoadLevel(levelID);
    }

	void checkConditions() {
		// TODO: set up victory conditions!
		if (cb.getScore() == targetScore) {
			onVictory();
		}
	}
}
