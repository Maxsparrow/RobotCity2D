using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpSpeed;
    public float speed;

    private bool isJumping = false;
    
    void FixedUpdate() {
        Move();
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

}