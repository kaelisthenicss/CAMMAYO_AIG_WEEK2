using UnityEngine;

public class MovingNavObstacle : MonoBehaviour
{
    public enum MovementAxis
    {
        X,
        Z
    }

    [Header("Movement Settings")]
    [SerializeField] private MovementAxis movementAxis = MovementAxis.X;
    [SerializeField] private float movementDistance = 2.5f;
    [SerializeField] private float movementSpeed = 2f;

    private Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float movementOffset = Mathf.PingPong(
            Time.time * movementSpeed,
            movementDistance
        );

        Vector3 movementPosition = startingPosition;

        if (movementAxis == MovementAxis.X)
        {
            movementPosition.x += movementOffset;
        }
        else if (movementAxis == MovementAxis.Z)
        {
            movementPosition.z += movementOffset;
        }

        transform.position = movementPosition;
    }
}