using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Driving Settings")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float braking = 25f;
    [SerializeField] private float maxSpeed = 12f;     // Much slower & controlled
    [SerializeField] private float turnRotationSpeed = 80f; // Degrees per second
    [SerializeField] private float turnStability = 5f; // Pushes car toward forward direction

    private Vector2 input;

    private void FixedUpdate()
    {
        HandleAcceleration();
        HandleSteering();
        LimitSpeed();
    }

    // ---------------------------------------------
    // ACCELERATION (NO REVERSE)
    // ---------------------------------------------
    void HandleAcceleration()
    {
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        // ACCELERATE (forward only)
        if (input.y > 0)
        {
            if (forwardSpeed < maxSpeed)
                rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            rb.linearDamping = 0.2f;
        }
        else
        {
            // BRAKE but DO NOT go backwards
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                rb.AddForce(-rb.linearVelocity.normalized * braking, ForceMode.Acceleration);
            }

            rb.linearDamping = 0.5f;
        }
    }

    // ---------------------------------------------
    // STEERING (NO SIDE SLIDE)
    // ---------------------------------------------
    void HandleSteering()
    {
        if (Mathf.Abs(input.x) > 0.05f)
        {
            // Rotate car using MoveRotation (stable turning)
            float turn = input.x * turnRotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }

        // Add a force that pushes velocity TO the forward direction
        // This removes the left-right sliding
        Vector3 forwardVel = transform.forward * Vector3.Dot(rb.linearVelocity, transform.forward);
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, forwardVel, Time.fixedDeltaTime * turnStability);
    }

    // ---------------------------------------------
    // SPEED LIMIT
    // ---------------------------------------------
    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    // ---------------------------------------------
    // INPUT
    // ---------------------------------------------
    public void SetInput(Vector2 inputVector)
    {
        input = inputVector; // No normalize (keeps analog control)
    }
}
