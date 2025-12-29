using UnityEngine;

public class HelicopterMovementUpdated : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public Transform helicopterModel;

    [Header("Settings")]
    public float liftForce = 20f;
    public float moveForce = 10f; // Increased value because we are using Force now, not Speed
    public float tiltAmount = 3f;
    public float tiltSpeed = 5f;

    private float targetX;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Calculate Target X (Same as before)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane gameplayPlane = new Plane(Vector3.forward, Vector3.zero);
        
        float distance;
        if (gameplayPlane.Raycast(ray, out distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            targetX = worldPoint.x;
        }
    }

    void FixedUpdate()
    {
        // 2. Vertical Movement (Lift)
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(Vector3.up * liftForce);
        }

        // 3. Horizontal Movement (The "Spring" Physics)
        // Calculate the distance between where we are and where we want to be
        float distanceToTarget = targetX - rb.position.x;

        // Apply a force proportional to that distance.
        // Far away = Big Pull. Close = Gentle Pull.
        // This naturally causes overshoot because force is still applied until you pass the target.
        Vector3 horizontalForce = new Vector3(distanceToTarget * moveForce, 0, 0);
        
        rb.AddForce(horizontalForce);

        // 4. The Tilt Mechanic
        // We still use the actual velocity to determine tilt, so it looks physically accurate
        float currentHorizontalSpeed = rb.linearVelocity.x; // 'linearVelocity' is Unity 6 syntax
        float targetTilt = currentHorizontalSpeed * -tiltAmount;
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);
        helicopterModel.localRotation = Quaternion.Lerp(helicopterModel.localRotation, targetRotation, Time.fixedDeltaTime * tiltSpeed);
    }
}