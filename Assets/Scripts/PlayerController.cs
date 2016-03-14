using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpSpeed;
	public float speed;

	private bool isJumping = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	private void Move() {
		transform.Translate (Input.GetAxis ("Horizontal") * Vector3.right * Time.deltaTime * speed, Camera.main.transform);
		if (Input.GetButton ("Jump") && !isJumping) {
			isJumping = true;
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
		}
		if (GetComponent<Rigidbody2D> ().velocity.y == 0) {
			isJumping = false;
		}
	}
}
