
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private Vector3 offset; // The initial distance from the camera to the player

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial offset
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // Update the camera's position to the player's position plus the offset
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
