using UnityEngine;

public class RotorController : MonoBehaviour
{
    [Header("Rotors")]
    public Transform mainRotor; // Drag Main Rotor object here
    public Transform tailRotor; // Drag Tail Rotor object here

    [Header("Settings")]
    public float maxSpeed = 1000f;
    public float acceleration = 500f; // How fast it speeds up
    public float deceleration = 200f; // How fast it slows down

    private float currentSpeed = 0f;

    void Update()
    {
        // 1. Calculate Speed based on Input
        if (Input.GetMouseButton(0))
        {
            // Speed up towards Max Speed
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Slow down towards 0 (Free spin)
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // Clamp speed so it doesn't go below 0 or above Max
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // 2. Apply Rotation
        if (mainRotor != null)
        {
            // Rotate around the Y axis (up) for main rotor
            mainRotor.Rotate(0, currentSpeed * Time.deltaTime, 0);
        }

        if (tailRotor != null)
        {
            // Rotate around the X axis (side) for tail rotor usually
            // Adjust to 'Rotate(0, 0, currentSpeed...)' or 'Rotate(0, currentSpeed...)' 
            // depending on how your 3D model was built.
            tailRotor.Rotate(0, 0, currentSpeed * Time.deltaTime);
        }
    }
}