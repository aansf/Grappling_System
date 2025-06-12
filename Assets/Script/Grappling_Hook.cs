using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Grappling Settings")]
    public LayerMask Grapple_Surface;
    public float maxDistance = 50f;
    public float springForce = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;

    [Header("References")]
    public Transform cameraTransform;
    public Transform hookOrigin; // where rope starts (e.g., hand or gun barrel)
    public LineRenderer lineRenderer;

    private SpringJoint joint;
    private Vector3 grapplePoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            StopGrapple();
        }

        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance, Grapple_Surface))
        {
            grapplePoint = hit.point;

            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = springForce;
            joint.damper = damper;
            joint.massScale = massScale;

            lineRenderer.positionCount = 2;
        }
    }

    void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, hookOrigin.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
