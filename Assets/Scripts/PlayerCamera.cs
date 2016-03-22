using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public Transform target;

	// Update is called once per frame
	void Update () {
        Vector3 newPosition = new Vector3();
        newPosition.x = target.position.x;
        newPosition.y = target.position.y;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
	}
}
