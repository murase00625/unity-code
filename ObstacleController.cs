using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	void OnTriggerEnter(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			collider.gameObject.SendMessage("StopPlayer");
		}
	}

}