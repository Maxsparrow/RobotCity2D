using UnityEngine;
using System.Collections;

public class HeliDrone : MonoBehaviour {

    public int speed;
    public float maxXPosition;
    public float minXPosition;
    public float bobStrength;
    public float bobSpeed;

    private int xDirection = 1;
    private float startingY;

    // Use this for initialization
    void Start () {
        startingY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        if (xDirection == 1)
		{
			transform.rotation = new Quaternion(0, 180, 0, 0);
        } else if (xDirection == -1)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
        }
	}

    void Move()
    {
        // Calculate new Y position
        float newY = startingY + (Mathf.Sin(Time.time * bobSpeed) * bobStrength);

        transform.Translate(Vector3.right * Time.deltaTime * speed * xDirection, Camera.main.transform);
        // Bob up and down
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (transform.position.x < minXPosition)
        {
            xDirection = 1;
        }
        else if (transform.position.x > maxXPosition)
        {
            xDirection = -1;
        }
    }
}
