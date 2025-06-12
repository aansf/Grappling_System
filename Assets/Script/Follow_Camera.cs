using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2f, -6f); // Back and slightly above
    public float followSpeed = 10f;

    [Header("Mouse Orbit")]
    public float rotateSpeed = 100f;
    public float pitchMin = -35f;
    public float pitchMax = 60f;

    private float yaw = 0f;
    private float pitch = 15f;

    void LateUpdate()
    {
        if (!target) return;

        // Rotate camera rig with mouse
        yaw += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Apply rotation to the camera rig
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Desired position is behind the player based on offset
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f); // Adjust look height
    }
}
