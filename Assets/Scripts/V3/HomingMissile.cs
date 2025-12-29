using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("References")]
    public Transform target; // Drag 'Player Character' here
    private Rigidbody rb;

    [Header("Physics Settings")]
    public float engineForce = 15f;
    public float torqueForce = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // 1. CONSTANT THRUST
        // Since the Red Arrow (X) is the nose, we apply force to the right
        rb.AddRelativeForce(Vector3.right * engineForce);

        // 2. HOMING MATH
        // Find the direction from missile to helicopter
        Vector2 directionToTarget = (Vector2)target.position - (Vector2)transform.position;
        
        // Calculate the angle the missile is currently facing
        float currentAngle = transform.eulerAngles.z;
        
        // Calculate the angle needed to look at the target
        // We add an offset because the X-axis is the nose (not the usual Y-up)
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Find the shortest path to rotate to that angle
        float angleError = Mathf.DeltaAngle(currentAngle, targetAngle);

        // 3. STEERING (TORQUE)
        // Instead of snapping rotation, we push the nose toward the target
        // This creates inertia; if the missile is fast, it will overshoot the turn
        rb.AddRelativeTorque(Vector3.forward * angleError * torqueForce * Time.fixedDeltaTime);
    }

    // Optional: Detect hit
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Missile Hit the Helicopter!");
            // Add explosion logic here
            Destroy(gameObject);
        }
    }
}