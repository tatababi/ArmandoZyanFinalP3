using UnityEngine;
using System.Collections; // <-- ADD THIS LINE

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    // Update is called once per frame
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero) // Simplified condition logic
            {
                // ERROR FIXED: Typo 'postition' changed to 'position'
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }
    }

    private IEnumerator Move(Vector3 targetPos) // Made private and used correct casing
    {
        isMoving = true; // Set isMoving flag

        // ERROR FIXED: Invalid chained comparison logic and missing transform.position reference
        while (Vector3.SqrMagnitude(targetPos - transform.position) > Mathf.Epsilon)
        {
            // ERROR FIXED: Incorrect use of MoveTowards parameters
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false; // Unset isMoving flag
    }
}
