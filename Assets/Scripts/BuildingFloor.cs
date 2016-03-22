using UnityEngine;

public class BuildingFloor : MonoBehaviour {
    
    void OnTriggerEnter2D (Collider2D jumper)
    {
        Physics2D.IgnoreCollision(jumper, transform.parent.GetComponent<BoxCollider2D>());
        Debug.Log("Ignoring platform collisions");
    }

    void OnTriggerExit2D (Collider2D jumper)
    {
        // Possible this is needed - it was in original script http://forum.unity3d.com/threads/layers-collision-and-one-way-platforms-a-question.71790/
        // But right now doesn't seem like it
        //jumper.gameObject.layer = LayerMask.NameToLayer(Constants.LayerPlayerCharacter);

        Debug.Log("No longer ignoring platform collisions");
        Physics2D.IgnoreCollision(jumper, transform.parent.GetComponent<BoxCollider2D>(), false);
    }
}
