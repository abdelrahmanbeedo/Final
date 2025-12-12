using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    // Multipliers
    float accelerationMultiplier = 3f;
    float breaksMultiplier = 15f;
    float steeringMultiplier = 5f;

    // Input
    Vector2 input = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Physics update
    private void FixedUpdate()
    {
        // Apply Acceleration
        if (input.y > 0)
            Accelerate();
        else
            rb.linearDamping = 0.2f;

        // Apply Brakes
        if (input.y < 0)
            Brake();

        // Apply Steering
        Steer();
    }

    void Accelerate()
    {
        rb.linearDamping = 0f;
        rb.AddForce(rb.transform.forward * accelerationMultiplier * input.y);
    }

    void Brake()
    {
        // Don't brake unless we are going forward
        if (rb.linearVelocity.z <= 0)
            return;

        rb.AddForce(rb.transform.forward * breaksMultiplier * input.y);
    }

    void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            rb.AddForce(rb.transform.right * steeringMultiplier * input.x);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }
}
