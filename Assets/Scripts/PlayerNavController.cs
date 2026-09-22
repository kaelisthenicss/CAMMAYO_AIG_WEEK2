using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerNavController : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private LayerMask movementSurfaceLayers = ~0;

    private NavMeshAgent navigationAgent;
    private Camera sceneCamera;

    private void Awake()
    {
        navigationAgent = GetComponent<NavMeshAgent>();
        sceneCamera = Camera.main;
    }

    private void Update()
    {
        HandleRightClickMovement();
    }

    private void HandleRightClickMovement()
    {
        if (Mouse.current == null || !Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray mouseRay = sceneCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit surfaceHit, Mathf.Infinity, movementSurfaceLayers))
        {
            navigationAgent.SetDestination(surfaceHit.point);
        }
    }
}