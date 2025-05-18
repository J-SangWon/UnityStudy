using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5f;

    [SerializeField] float minVerticalAngle = -45f;
    [SerializeField] float maxVerticalAngle = 45f;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertY;
    [SerializeField] bool invertX;

    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        invertXVal = invertX ? -1 : 1;
        invertYVal = invertY ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed * invertYVal;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * rotationSpeed * invertXVal;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = target.position + new Vector3(framingOffset.x, framingOffset.y, 0);
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
