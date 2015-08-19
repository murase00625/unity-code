using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public GameObject pointA, pointB;
	public float speed;

	private Vector2 toStart, toEnd, current;
	private GameObject target;

	// Use this for initialization
	void Start () {
		this.transform.position = pointA.transform.position;
		this.toStart = new Vector2(pointA.transform.position.x-pointB.transform.position.x,
									pointA.transform.position.y-pointB.transform.position.y);
		this.toStart.Normalize();
		this.toStart *= speed / 100f;
		this.toEnd = -toStart;

		this.current = toEnd;
		this.target = pointB;

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(this.transform.position, this.target.transform.position) < 0.1f) {
			this.current = (this.current == toEnd) ? toStart : toEnd;
			this.target = (this.target == pointB) ? pointA : pointB;
		}
	}


	void FixedUpdate() {
		this.transform.position = new Vector2(this.transform.position.x + current.x,
											this.transform.position.y + current.y);
	}
}
