using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // 이동 속도
    [SerializeField] float moveSpeed = 5f;
    // 회전 속도
    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheck = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    bool isGrounded;
    float ySpeed = 0f; // Y축 속도


    // 목표 회전값
    Quaternion targetRotation;

    // 카메라 컨트롤러 참조
    CameraController cameraController;
    CharacterController characterController;

    Animator anim;


    private void Awake()
    {
        // 메인 카메라에서 CameraController 컴포넌트 가져오기
        cameraController = Camera.main.GetComponent<CameraController>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 수평, 수직 입력값 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        GroundCheck();
        Debug.Log("isGround = " + isGrounded);


        // 전체 이동량 계산
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        anim.SetFloat("moveAmount", moveAmount);

        // 입력 방향 정규화
        var moveInput = (new Vector3(h, 0, v)).normalized;

        // 카메라 방향을 기준으로 이동 방향 계산
        var moveDir = cameraController.PlanarRotation * moveInput;

        var Velocity = moveDir * moveSpeed;
        Velocity.y = ySpeed; 
        characterController.Move(Velocity * Time.deltaTime);
        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else 
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        // 이동 입력이 있을 때만 처리
        if (moveAmount > 0)
        {
            // 위치 이동
            //transform.position += moveDir * moveSpeed * Time.deltaTime;
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            // 이동 방향으로 회전 목표 설정
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // 부드러운 회전 처리
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    #region Gizmos
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheck, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckOffset), groundCheck);
    }
    #endregion

}