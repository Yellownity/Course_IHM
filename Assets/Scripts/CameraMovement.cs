using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float offsetDistance = 30f; // distance in front of the player toward the checkpoint
    private Transform playerTransform;
    private Vector3 checkpointDirection;
    [SerializeField] private float smoothTime = 0.35f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetCheckpointPosition;
    [SerializeField] private float checkpointSmoothTime = 0.6f; // (legacy) how quickly the checkpoint reference moves
    private Vector3 checkpointVelocity = Vector3.zero;
    private Vector3 smoothedCheckpointPosition;
    [SerializeField] private float checkpointResponsiveness = 3f; // how quickly the checkpoint velocity steers toward desired velocity
    [SerializeField] private float followSpeedFactor = 1.0f; // multiplier of player speed to move checkpoint reference
    [SerializeField] private float minCheckpointMoveSpeed = 2f; // minimum speed for checkpoint reference when player is slow
    [SerializeField] private float minAheadDistance = 8f; // ensure reference stays at least this far ahead of the player on forward axis
    private Rigidbody playerRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();
        LapManager.onCheckpointEvent += HandleCheckpointReached;
        targetCheckpointPosition = playerTransform.position;
        smoothedCheckpointPosition = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the checkpoint reference using a velocity that steers toward the desired velocity.
        float playerSpeed = playerRigidbody != null ? playerRigidbody.linearVelocity.magnitude : 0f;
        // Ensure the reference moves at least as fast as the player so it doesn't get "largué"
        float desiredSpeed = Mathf.Max(playerSpeed * followSpeedFactor, playerSpeed, minCheckpointMoveSpeed);
        Vector3 toTarget = targetCheckpointPosition - smoothedCheckpointPosition;
        Vector3 desiredDir = toTarget.sqrMagnitude > 0.0001f ? toTarget.normalized : Vector3.zero;
        Vector3 desiredVelocity = desiredDir * desiredSpeed;

        // Steer current checkpointVelocity toward desiredVelocity for smooth turning (creates curved path)
        checkpointVelocity = Vector3.Lerp(checkpointVelocity, desiredVelocity, Mathf.Clamp01(checkpointResponsiveness * Time.deltaTime));

        // Prevent the checkpoint reference from moving backwards relative to player's forward axis
        Vector3 playerForward = playerTransform.forward;
        float forwardComp = Vector3.Dot(checkpointVelocity, playerForward);
        if (forwardComp < 0f)
        {
            Vector3 lateral = checkpointVelocity - playerForward * forwardComp;
            checkpointVelocity = lateral + playerForward * Mathf.Max(0f, forwardComp);
        }

        // Integrate position
        smoothedCheckpointPosition += checkpointVelocity * Time.deltaTime;

        // Ensure the smoothed reference stays at least a bit ahead of the player on the forward axis
        Vector3 vecToSmoothed = smoothedCheckpointPosition - playerTransform.position;
        float forwardDist = Vector3.Dot(vecToSmoothed, playerForward);
        if (forwardDist < minAheadDistance)
        {
            Vector3 projPlane = Vector3.ProjectOnPlane(vecToSmoothed, playerForward);
            smoothedCheckpointPosition = playerTransform.position + playerForward * minAheadDistance + projPlane;
            // remove any backward forward component on velocity
            float fcomp = Vector3.Dot(checkpointVelocity, playerForward);
            if (fcomp < 0f) checkpointVelocity -= playerForward * fcomp;
        }

        // Use the smoothed checkpoint position (updated when event fires) and smoothly move the camera
        Vector3 Direction = smoothedCheckpointPosition - playerTransform.position;
        Direction.Normalize();
        Direction.z = 0;
        Vector3 desiredPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 60, playerTransform.position.z-10) + Direction * offsetDistance;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
    }

    private void HandleCheckpointReached(object sender, GameObject checkpoint)
    {
        checkpointDirection = checkpoint.transform.position;
        // When a new checkpoint is set, update the target; camera will move smoothly because smoothedCheckpointPosition lags behind
        targetCheckpointPosition = checkpoint.transform.position;
    }

    private void OnDestroy()
    {
        LapManager.onCheckpointEvent -= HandleCheckpointReached;
    }
}
