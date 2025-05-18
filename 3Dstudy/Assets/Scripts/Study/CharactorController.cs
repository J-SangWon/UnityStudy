using UnityEngine;

public class CharactorController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 720f;
    [SerializeField] float groundCheck = 0.15f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 groundCheckOffset;
    bool isGrounded;
    float ySpeed;

    Quaternion targetRotation;

    CameraController cameraController;
    CharacterController characterController;
    Animator anim;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GroundCheck();
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        anim.SetFloat("moveAmount", moveAmount);

        var moveInput = new Vector3(h, 0, v).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;
        var Velocity = moveDir * moveSpeed;

        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
        Velocity.y = ySpeed;

        characterController.Move(Velocity * Time.deltaTime);
        
        if(moveAmount > 0)
        {
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheck, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckOffset), groundCheck);
    }
}
