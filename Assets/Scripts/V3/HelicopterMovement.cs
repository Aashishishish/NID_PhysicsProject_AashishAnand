using UnityEngine;

public class HelicopterMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public Transform helicopterModel; // Assign 'Helicopter Bell' here

    [Header("Settings")]
    public float liftForce = 20f;
    public float moveSpeed = 5f;
    public float tiltAmount = 3f;
    public float tiltSpeed = 5f;

    private float targetX;

    void Start()
    {
        // If you forgot to drag the RB in, this grabs it automatically
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Calculate Target X Position based on Mouse
        // We create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Create a mathematical plane at Z=0 (where your game takes place)
        Plane gameplayPlane = new Plane(Vector3.forward, Vector3.zero);
        
        float distance;
        // Find where the mouse ray hits that Z=0 plane
        if (gameplayPlane.Raycast(ray, out distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            targetX = worldPoint.x;
        }
    }

    void FixedUpdate()
    {
        // 2. Vertical Movement (Lift vs Gravity)
        if (Input.GetMouseButton(0)) // Left Click Held
        {
            // Apply upward force. Gravity handles the "down" automatically.
            rb.AddForce(Vector3.up * liftForce);
        }

        // 3. Horizontal Movement (Follow Mouse)
        // Calculate the direction towards the mouse target
        Vector3 targetPosition = new Vector3(targetX, rb.position.y, 0);
        Vector3 direction = (targetPosition - rb.position).normalized;
        float distanceToTarget = Vector3.Distance(rb.position, targetPosition);

        // We use linearVelocity for responsive physics movement
        // Note: linearVelocity replaces 'velocity' in Unity 6
        Vector3 newVelocity = rb.linearVelocity;
        
        // Move towards target, but stop jittering if we are very close
        if (distanceToTarget > 0.1f)
        {
            newVelocity.x = direction.x * moveSpeed;
        }
        else
        {
            newVelocity.x = 0;
        }
        
        rb.linearVelocity = newVelocity;

        // 4. The Tilt Mechanic (Visuals + Collider)
        // We calculate tilt based on how fast we are moving horizontally
        float targetTilt = newVelocity.x * -tiltAmount;
        
        // Smoothly rotate the child object (Helicopter Bell)
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);
        helicopterModel.localRotation = Quaternion.Lerp(helicopterModel.localRotation, targetRotation, Time.fixedDeltaTime * tiltSpeed);
    }
}