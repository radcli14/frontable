using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float gravity = 1f;

    [Header("Initial Settings")]
    [SerializeField] private Vector3 initialPosition = new Vector3(0f, 0.2f, 1f);
    [SerializeField] private Vector3 initialVelocity = new Vector3(3f, 0.5f, 0.5f);
    [SerializeField] private float reinitializationInterval = 3f;

    private Rigidbody rb;
    private float timeSinceLastInitialization;
    private TrailRenderer trail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("BallControl requires a Rigidbody component!");
            enabled = false;
            return;
        }
        
        // Set world gravity to specified scale
        Physics.gravity = new Vector3(0f, -gravity, 0f);
        
        // Get reference to trail renderer
        trail = GetComponent<TrailRenderer>();
        
        InitializeBall();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastInitialization += Time.deltaTime;
        
        if (timeSinceLastInitialization >= reinitializationInterval)
        {
            InitializeBall();
            timeSinceLastInitialization = 0f;
        }
    }

    private void InitializeBall()
    {
        // Reset position
        transform.position = initialPosition;
        
        // Reset velocity
        rb.linearVelocity = initialVelocity;
        rb.angularVelocity = Vector3.zero;

        // Clear the trail
        if (trail != null)
        {
            trail.Clear();
        }
    }
}
