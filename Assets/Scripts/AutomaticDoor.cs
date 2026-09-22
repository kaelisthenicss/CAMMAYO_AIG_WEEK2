using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AutomaticDoor : MonoBehaviour
{
    [Header("Door Timing")]
    [SerializeField] private float closedDuration = 3f;
    [SerializeField] private float openDuration = 3f;

    [Header("Door Movement")]
    [SerializeField] private float openHeight = 3.5f;
    [SerializeField] private float movementSpeed = 2.5f;

    private NavMeshObstacle doorObstacle;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Awake()
    {
        doorObstacle = GetComponent<NavMeshObstacle>();

        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    private void Start()
    {
        StartCoroutine(DoorCycle());
    }

    private IEnumerator DoorCycle()
    {
        while (true)
        {

            doorObstacle.enabled = true;
            yield return new WaitForSeconds(closedDuration);

            doorObstacle.enabled = false;
            yield return MoveDoor(openPosition);

            yield return new WaitForSeconds(openDuration);

            yield return MoveDoor(closedPosition);
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;
    }
}