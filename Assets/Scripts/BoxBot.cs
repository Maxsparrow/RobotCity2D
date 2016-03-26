using UnityEngine;
using System.Collections;

public class BoxBot : MonoBehaviour {

    public float patrolDistance;
    public int speed;

    private Vector3 startingPosition;
    private int xDirection = 1; // Always patrols to the right initially

    void Start()
    {
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        if (xDirection == 1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (xDirection == -1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }


    private void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed * xDirection, Camera.main.transform);

        if (transform.position.x < startingPosition.x)
        {
            xDirection = 1;
        }
        else if (transform.position.x > startingPosition.x + patrolDistance)
        {
            xDirection = -1;
        }
    }
}
