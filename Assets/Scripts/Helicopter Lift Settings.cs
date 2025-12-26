using UnityEngine;

public class HelicopterLiftSettings : MonoBehaviour
{
    [Header("Physics Components")]
    public Rigidbody heliRigidbody; // The main body
    public HingeJoint rotorHinge;   // The rotor with the motor
    
    [Header("Lift Settings")]
    public float liftForce = 20f;      // Upward strength
    public float rotorSpeed = 2000f;   // Visual rotation speed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Visuals: Control the rotor motor
        JointMotor motor = rotorHinge.motor;

        if (Input.GetMouseButton(0)) // Left Click Held
        {
            motor.targetVelocity = rotorSpeed;
            motor.force = 1000f;
            rotorHinge.useMotor = true;
        }
        else // Released
        {
            rotorHinge.useMotor = false;
        }

        rotorHinge.motor = motor;
    }

    void FixedUpdate()
    {
        // Physics: Apply lift only while button is held
        if (Input.GetMouseButton(0))
        {
            // ForceMode.Acceleration ignores mass, making it easier to tune
            heliRigidbody.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
        }
    }
}
