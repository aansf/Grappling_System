using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrapplingHook : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform hookOrigin;
    public LineRenderer lineRenderer;
    public LayerMask grappleLayer;

    [Header("Distances & Force")]
    public float maxDistance = 50f;
    public float hookSpeed = 40f;
    public float stopDistance = 3f;
    public float swingDistance = 12f;

    [Header("States")]
    private bool isGrappling = false;
    private bool isSwinging = false;
    private Vector3 grapplePoint;

    private Rigidbody rb;
    private SpringJoint joint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrappling && !isSwinging)
                TryGrapple();
            else
                StopGrapple();
        }

        if (isGrappling || isSwinging)
            DrawRope();
    }

    void TryGrapple()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, maxDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            float dist = Vector3.Distance(transform.position, grapplePoint);

            if (dist > swingDistance)
            {
                StartPull();
            }
            else
            {
                StartSwing();
            }
        }
    }

    void StartPull()
    {
        isGrappling = true;

        rb.linearVelocity = Vector3.zero;
        Vector3 dir = (grapplePoint - transform.position).normalized;
        rb.AddForce(dir * hookSpeed, ForceMode.VelocityChange);
    }

    void StartSwing()
    {
        isSwinging = true;

        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distance = Vector3.Distance(transform.position, grapplePoint);
        joint.maxDistance = distance * 0.9f;
        joint.minDistance = distance * 0.8f;

        joint.spring = 5f;
        joint.damper = 4f;
        joint.massScale = 6f;
    }

    void StopGrapple()
    {
        isGrappling = false;
        isSwinging = false;
        if (joint) Destroy(joint);
        lineRenderer.positionCount = 0;
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            float dist = Vector3.Distance(transform.position, grapplePoint);
            if (dist < stopDistance)
            {
                StartSwing();
                isGrappling = false;
            }
        }
    }

    void DrawRope()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, hookOrigin.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }
}
