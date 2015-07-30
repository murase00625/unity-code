using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject obj = collider.gameObject;
		if (obj.tag == "Player") {
			obj.SendMessage("collect");
			Destroy(this.gameObject);
		}
	}

}
