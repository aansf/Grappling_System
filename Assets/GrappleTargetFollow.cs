using UnityEngine;

public class GrappleTargetFollow : MonoBehaviour
{
    public Camera mainCam;
    public SpriteRenderer spriteRenderer;
    public LayerMask grappleLayer;
    public float maxDistance = 200f;
    public float displayDistance = 5f; // how far from camera the dot floats

    void Start()
    {
        if (!mainCam) mainCam = Camera.main;
    }

    void Update()
    {
        // Ray from camera forward (screen center)
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);

        // Check if we hit a valid grapple surface
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, grappleLayer))
        {
            // Show dot at fixed point in front of camera (centered)
            transform.position = mainCam.transform.position + mainCam.transform.forward * displayDistance;
            transform.rotation = Quaternion.LookRotation(mainCam.transform.forward);
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
