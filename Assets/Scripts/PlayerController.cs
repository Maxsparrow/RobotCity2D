using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpSpeed;
    public float speed;
	public int maxHealth;

    private bool isJumping = false;
	private int currentHealth;
	private bool stunned = false;
    
	void Start() {
		currentHealth = maxHealth;
	}

    void FixedUpdate() {
		if (!stunned) {
			Move();
		}
    }

    private void Move() {
        // Move horizontally
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * speed, Camera.main.transform);

        // Jump
        if (Input.GetButton("Jump") && !isJumping) {
            isJumping = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        }
        if (GetComponent<Rigidbody2D>().velocity.y == 0) {
            isJumping = false;
        }
        
        // Allow player to fall through platforms by pushing down
        if (Input.GetAxis("Vertical") == -1)
        {
            gameObject.layer = LayerMask.NameToLayer(Constants.LayerJumperNoCollide);
        } else
        {
            gameObject.layer = LayerMask.NameToLayer(Constants.LayerPlayerCharacter);
        }
    }

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Debug.Log ("Hit an enemy");
			TakeDamage(col.gameObject.GetComponent<Enemy>().damage);
		}
	}

	private void TakeDamage(int damage) {
		currentHealth -= damage;
		Debug.Log ("Took " + damage + " damage, currently have " + currentHealth + " health.");
		Knockback ();
		if (currentHealth <= 0) {
			Die ();
		}
	}

	private void Knockback() {
		StartCoroutine("WaitForKnockback");
	}

	IEnumerator WaitForKnockback() {
		Debug.Log ("Stunned");
		stunned = true;
		// TODO add knockback movement
		yield return new WaitForSeconds (3); // TODO: un hard code knock back time
		Debug.Log ("Not stunned anymore");
		stunned = false;
	}

	private void Die() {
		Debug.Log ("I'm dead");
		// Drop dead and remove Player Controller
		transform.eulerAngles = new Vector3 (0, 0, 90);
		GameObject.Destroy (this);
	}

}