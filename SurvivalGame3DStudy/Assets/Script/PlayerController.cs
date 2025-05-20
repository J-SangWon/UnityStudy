using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Setting")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float defaultMoveSpeed;
    [SerializeField] float jumpForce = 5f;
    bool isRun;
    bool isCrouch;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float crounchPosY;
    [SerializeField] float defaultPosY;
    [SerializeField] float applyPosY;

    [Header("Camera Setting")]
    [SerializeField] float lookSensitivity;
    [SerializeField] float cameraRotationLimit;
    [SerializeField] float currentCameraRotationX;
    [SerializeField] Camera playerCamera;

    [Header("Ground Check")]
    [SerializeField] bool isGround = true;
    [SerializeField] float groundCheck = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 groundCheckOffset;

    [Header("Component")]
    Rigidbody rb;
    CapsuleCollider capCol;
    [SerializeField] GunController GC;

    public bool cameraInverteX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        GC = FindAnyObjectByType<GunController>();

        //이동
        defaultMoveSpeed = moveSpeed;

        //카메라
        defaultPosY = playerCamera.transform.localPosition.y;
        applyPosY = defaultPosY;
    }

    void Update()
    {
        GroundCheck();
        Jump();
        Crouch();
        CrouchCamera();
        Run();
        Move();
        CameraRotation();
        CharactoerRotation();
    }

    private void Move()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;

        Vector3 vel = (moveHorizontal + moveVertical).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + vel);

    }

    void CameraRotation()
    {
        float rotateX = Input.GetAxisRaw("Mouse Y") * lookSensitivity;

        if (cameraInverteX)
        {
            rotateX = -rotateX;
        }
        currentCameraRotationX -= rotateX; //반전 해서 적용
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(currentCameraRotationX, 0f, 0f);

    }
    void CharactoerRotation()
    {
        float rotateY = Input.GetAxisRaw("Mouse X");
        Vector3 charactorRotationY = new Vector3(0, rotateY, 0) * lookSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(charactorRotationY));
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
            moveSpeed = runSpeed;
            GC.CancleFineSight();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            moveSpeed = defaultMoveSpeed;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.linearVelocity = transform.up * jumpForce;
            isCrouch = false;
        }

    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = !isCrouch;
            if (isCrouch)
            {
                moveSpeed = crouchSpeed;
                applyPosY = crounchPosY;
            }
            else
            {
                moveSpeed = defaultMoveSpeed;
                applyPosY = defaultPosY;
            }

            

        }
    }

    private void CrouchCamera()
    {
        Vector3 currentPos = playerCamera.transform.localPosition;
        Vector3 targetPos = new Vector3(
            playerCamera.transform.localPosition.x,
            applyPosY,
            playerCamera.transform.localPosition.z);

        playerCamera.transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, 5f * Time.deltaTime);
    }


    #region Gizmos
    void GroundCheck()
    {
        isGround = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheck, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckOffset), groundCheck);
    }
    #endregion



}
