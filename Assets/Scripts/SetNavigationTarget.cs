using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField]
    private Camera topDownCamera;
    [SerializeField]
    private GameObject navTargetObject;

    private NavMeshPath path; // Current calculated path
    private LineRenderer line; // LineRenderer to display path

    private void Start()
    {
        path = new NavMeshPath();
        line = GetComponent<LineRenderer>();
        line.enabled = false; // Start with the line disabled
    }

    public void UpdateTargetPosition(Vector3 targetPosition)
    {
        // Update target cube's position
        navTargetObject.transform.position = targetPosition;

        // Calculate and display the path
        NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);

        if (path.corners.Length > 0)
        {
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            line.enabled = true; // Enable the line

            // Move and adjust top-down camera
            if (topDownCamera != null)
            {
                topDownCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y + 10, targetPosition.z - 10);
                topDownCamera.transform.LookAt(targetPosition);
            }
        }
        else
        {
            Debug.LogError("No valid path found to the target position.");
        }
    }
}
