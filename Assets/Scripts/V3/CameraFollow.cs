using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Targeting")]
    public Transform target; // Drag 'Player Character' here
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Lag Settings")]
    public float smoothTime = 0.3f;
    
    // This variable is used internally by SmoothDamp to track velocity
    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Define where the camera WANT to be
        Vector3 destination = target.position + offset;

        // 2. Smoothly move from current position to destination
        // SmoothDamp is perfect for cameras because it handles the "lag" naturally
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            destination, 
            ref currentVelocity, 
            smoothTime
        );
    }
}