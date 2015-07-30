using UnityEngine;
using System.Collections;

public class CollectorBehavior : MonoBehaviour {

	private int score = 0;
	public DisplayText dt;

	void collect() {
		score++;
		print("Score: " + score);
		if (dt != null) dt.setContent("scoreboard", "Score: " + score);
	}
}
