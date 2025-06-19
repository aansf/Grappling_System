using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // ───────── Public Tunables ─────────
    [Header("Layers & Distances")]
    public LayerMask grappleLayer;
    public float maxDistance = 50f;

    [Header("Spring Settings (Pulling)")]
    public float springForce = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;

    [Header("Visuals")]
    public Transform cameraTransform;
    public Transform hookOrigin;      // e.g. hand or gun barrel
    public LineRenderer lineRenderer;

    [Header("Auto-detach")]
    public float arriveDistance = 2f; // How close = reached

    [Header("Rope Length Settings")]
    [Tooltip("Set the rope length used when swinging (after reaching the grapple point).")]
    public float ropeLength = 10f;

    // ───────── Internals ─────────
    private enum State { Idle, Hooked, Pulling, Swinging }
    private State state = State.Idle;

    private SpringJoint joint;
    private Vector3 grapplePoint;

    // ───────── Unity Loop ─────────
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (state)
            {
                case State.Idle:
                    TryAttach();         // Step 1: Attach
                    break;

                case State.Hooked:
                    StartPull();         // Step 2: Pull
                    break;

                case State.Pulling:
                case State.Swinging:
                    Detach();            // Step 3: Release any time
                    break;
            }
        }

        if (state != State.Idle) DrawRope();
        if (state == State.Pulling) CheckArrival();
    }


    // ───────── Phase 1: Attach ─────────
    void TryAttach()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, maxDistance, grappleLayer))
        {
            float hitDistance = Vector3.Distance(transform.position, hit.point);

            // ✅ New check: only attach if within rope length
            if (hitDistance > ropeLength)
            {
                Debug.Log("Too far to grapple — rope too short!");
                return;
            }

            grapplePoint = hit.point;
            state = State.Hooked;

            lineRenderer.positionCount = 2;
            DrawRope();
        }
    }


    // ───────── Phase 2: Pull ─────────
    void StartPull()
    {
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float dist = Vector3.Distance(transform.position, grapplePoint);
        joint.maxDistance = dist * 0.8f;   // little slack
        joint.minDistance = 0f;            // reel all the way in

        joint.spring = springForce;
        joint.damper = damper;
        joint.massScale = massScale;

        state = State.Pulling;
    }

    // ───────── Phase 3: Detach ─────────
    void Detach()
    {
        if (joint) Destroy(joint);
        lineRenderer.positionCount = 0;
        state = State.Idle;
    }

    // ───────── Helpers ─────────
    void DrawRope()
    {
        lineRenderer.SetPosition(0, hookOrigin.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    void CheckArrival()
    {
        float distance = Vector3.Distance(transform.position, grapplePoint);
        if (distance < arriveDistance)
        {
            // Lock rope length for pendulum swing
            joint.maxDistance = ropeLength;
            joint.minDistance = ropeLength * 0.95f;
            state = State.Swinging;

            Debug.Log("Reached grapple point – now swinging.");
        }
    }


}
