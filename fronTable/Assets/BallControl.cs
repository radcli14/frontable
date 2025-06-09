using UnityEngine;

public class BallControl : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private Vector3 initialPosition = new Vector3(0f, 0.2f, 1f);
    [SerializeField] private Vector3 initialVelocity = new Vector3(3f, 0.5f, 0.5f);
    [SerializeField] private float reinitializationInterval = 3f;

    [Header("Randomization")]
    [SerializeField] private float velocityNoise = 0.2f; // Amount of random noise to add to velocity

    private Rigidbody rb;
    private float timeSinceLastInitialization;
    private TrailRenderer trail;

    // Use this to track if the full scale ball has been reset, if so, this triggers clearing the tail in the mini ball
    public bool didReset = false;

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
        // Reset position relative to parent
        transform.localPosition = initialPosition;
        
        // Add random noise to initial velocity
        Vector3 noisyVelocity = initialVelocity + new Vector3(
            Random.Range(-velocityNoise, velocityNoise),
            Random.Range(-velocityNoise, velocityNoise),
            Random.Range(-velocityNoise, velocityNoise)
        );
        
        // Reset velocity with noise
        rb.linearVelocity = noisyVelocity;
        rb.angularVelocity = Vector3.zero;

        // Clear the trail
        if (trail != null)
        {
            trail.Clear();
        }

        didReset = true;
    }
}
