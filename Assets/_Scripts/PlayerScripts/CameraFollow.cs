using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;  // Reference to the player
    public float smoothSpeed = 0.125f;  // Smoothness of camera movement
    public Vector3 offset = new Vector3(0, 2, -10);  // Default offset from the player's position

    // Minimum and maximum bounds to limit the camera's movement
    public Vector2 minBounds = new Vector2(-10, 0);  // Default minimum bounds
    public Vector2 maxBounds = new Vector2(10, 0);   // Default maximum bounds

    private float initialYPos;  // Store the initial camera Y position

    void Start()
    {
        // Initialize the Y position of the camera to prevent focusing during jumps
        initialYPos = transform.position.y;

        // If no player is assigned in the Inspector, try to find the player by tag
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        // Desired position is the player's X position + offset but keeps the initial Y
        Vector3 desiredPosition = new Vector3(playerTransform.position.x + offset.x, initialYPos + offset.y, offset.z);

        // Clamp the camera's position within the specified bounds for X movement
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
