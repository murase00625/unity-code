using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public int damage = 1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            collider.gameObject.SendMessage("inflictDamage", damage);
        }
    }

}