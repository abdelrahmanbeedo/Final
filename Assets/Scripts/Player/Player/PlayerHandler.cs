using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Driving Settings")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float braking = 25f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float turnRotationSpeed = 80f;
    [SerializeField] private float turnStability = 5f;

    private Vector2 input;
    private bool isDead = false;

    private void FixedUpdate()
    {
        if (isDead) return;

        HandleAcceleration();
        HandleSteering();
        LimitSpeed();
    }

    // ---------------- ACCELERATION ----------------
    void HandleAcceleration()
    {
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        if (input.y > 0)
        {
            if (forwardSpeed < maxSpeed)
                rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            rb.linearDamping = 0.2f;
        }
        else
        {
            if (rb.linearVelocity.magnitude > 0.1f)
                rb.AddForce(-rb.linearVelocity.normalized * braking, ForceMode.Acceleration);

            rb.linearDamping = 0.5f;
        }
    }

    // ---------------- STEERING ----------------
    void HandleSteering()
    {
        if (Mathf.Abs(input.x) > 0.05f)
        {
            float turn = input.x * turnRotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }

        Vector3 forwardVel =
            transform.forward * Vector3.Dot(rb.linearVelocity, transform.forward);

        rb.linearVelocity =
            Vector3.Lerp(rb.linearVelocity, forwardVel, Time.fixedDeltaTime * turnStability);
    }

    // ---------------- SPEED LIMIT ----------------
    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    // ---------------- INPUT ----------------
    public void SetInput(Vector2 inputVector)
    {
        input = inputVector;
    }

    // ---------------- COLLISION ----------------
    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        GameManager.Instance.GameOver();
    }
}
