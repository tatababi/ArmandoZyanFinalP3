
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private Vector3 offset; // The initial distance from the camera to the player

   
    void Start()
    {
        // Calculate the initial offset
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    
    void LateUpdate()
    {
      
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
