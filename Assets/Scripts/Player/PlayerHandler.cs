using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody rb;

    [SerializeField]
    private float acceleration = 10f;

    [SerializeField]
    private float brakeForce = 5f;

    [SerializeField]
    private float steeringMultiplier = 5f;

    private Vector2 input = Vector2.zero;

    private void FixedUpdate()
    {
        // ACCELERATE
        if (input.y > 0)
        {
            Accelerate();
        }
        // BRAKE / SLOW DOWN
        else if (input.y < 0)
        {
            Brake();
        }
        else
        {
            rb.drag = 1f; // natural slow down
        }

        // STEERING
        Steer();
    }

    private void Accelerate()
    {
        rb.drag = 0f;
        rb.AddForce(transform.forward * acceleration * input.y, ForceMode.Acceleration);
    }

    private void Brake()
{
    // Check forward speed using linearVelocity.z instead of velocity.z
    if (rb.linearVelocity.z <= 0) return;

    rb.AddForce(-transform.forward * brakeForce * Mathf.Abs(input.y), ForceMode.Acceleration);
}


    private void Steer()
    {
        if (Mathf.Abs(input.x) > 0.01f)
        {
            rb.AddForce(transform.right * steeringMultiplier * input.x, ForceMode.Acceleration);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        input = inputVector.normalized;
    }
}
