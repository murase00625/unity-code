using UnityEngine;
using System.Collections;

public class CollectableHealth : MonoBehaviour {

	public int health = 1;

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject obj = collider.gameObject;
		if (obj.tag == "Player") {
			obj.SendMessage("heal", health);
			Destroy(this.gameObject);
		}
	}
}
