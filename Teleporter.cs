using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Transform destination;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player" && destination != null) {
			collider.gameObject.transform.position = destination.position;
		}
	}

}
