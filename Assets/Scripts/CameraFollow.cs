using UnityEngine;

/*
 * Goes on the MainCamera Object
 * follow the player
 */

public class CameraFollow : MonoBehaviour
{
    public Transform player; // create a reference to the player movement
    public float offset; 

    void LateUpdate()
    {
        Vector3 temp = transform.position; //store current camera's position

        temp.x = player.position.x; //set camera's position equal to player's x position
        temp.x += offset; //offset the camera 

        transform.position = temp; //set back cam's temp position to cam's current position
    }
}
