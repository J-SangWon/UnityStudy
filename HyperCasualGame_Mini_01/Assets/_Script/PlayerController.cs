using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float minJumpForce = 15f; // 최소 점프 힘
    public float maxJumpForce = 30f; // 최대 점프 힘
    public float jumpChargeRate = 7; // 점프 힘 충전 속도

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    private float currentJumpCharge = 0f;
    private bool isChargingJump = false;
    private bool isJumping = false; // 점프 중인지 여부
    private bool isRunning = false; // 달리는 중인지 여부
    private bool isGrounded = false; // 땅에 닿았는지

    // 땅 체크를 위한 변수
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    // 캐릭터
    public float characterSize = 3f;
    public float characterDir = 1f; // 1: 오른쪽, -1: 왼쪽
    public float moveInput;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GroundCheck();
        Flip();
        Jump();
        if (!isChargingJump && !isJumping)
            Move();
    }

    private void Move()
    {
        // 좌우 이동 처리
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 애니메이션 파라미터 설정
        if (anim != null)
        {
            bool isMoving = Mathf.Abs(moveInput) > 0.1f;
            if (isMoving != isRunning)
            {
                isRunning = isMoving;
                anim.SetBool("Run", isRunning);
            }
        }
    }

    private void Flip()
    {
        // 캐릭터 방향 전환 (좌우 입력이 있을 때만)
        if (moveInput != 0)
        {
            if (moveInput > 0.01f) // 오른쪽으로 이동 입력이 있을 때 (약간의 오차 고려)
            {
                // 스프라이트가 오른쪽을 보도록 설정
                // (기본 스프라이트가 오른쪽을 보고 있다면 false, 왼쪽을 보고 있다면 true로 설정)
                sp.flipX = false;
                characterDir = 1;
            }
            else if (moveInput < -0.01f) // 왼쪽으로 이동 입력이 있을 때 (약간의 오차 고려)
            {
                // 스프라이트가 왼쪽을 보도록 설정
                // (기본 스프라이트가 오른쪽을 보고 있다면 true, 왼쪽을 보고 있다면 false로 설정)
                sp.flipX = true;
                characterDir = -1;
            }
        }

    }

    private void Jump()
    {
        // 점프 버튼 눌렀을 때 충전 시작
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            currentJumpCharge = 0f; // 충전량 초기화
        }

        // 점프 버튼 누르고 있을 때 충전
        if (Input.GetKey(KeyCode.Space) && isChargingJump)
        {
            currentJumpCharge += jumpChargeRate * Time.deltaTime;
            // 최대 점프 힘 제한
            currentJumpCharge = Mathf.Clamp(currentJumpCharge, 0, maxJumpForce);
        }

        // 점프 버튼 떼었을 때 점프!
        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            isJumping = true; // 점프 시작 시 isJumping을 true로 설정
            isChargingJump = false;
            anim.SetBool("Jump", true);
            anim.SetBool("Landing", false); // 착지 애니메이션 초기화

            // 방향과 점프 힘 계산
            float jumpPower = minJumpForce + currentJumpCharge;

            // velocity를 직접 설정하여 더 정확한 대각선 점프 구현
            rb.linearVelocity = new Vector2(characterDir * jumpPower * 0.7f, jumpPower);

            currentJumpCharge = 0f; // 점프 후 충전량 초기화
        }
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void OnLandingAnimationEnd()
    {
        anim.SetBool("Landing", false); // 착지 애니메이션 종료
    }

    private bool wasGrounded; // 이전 프레임의 접지 상태 저장

    private void GroundCheck()
    {
        // 이전 프레임의 접지 상태 저장
        wasGrounded = isGrounded;

        // 현재 프레임의 접지 상태 업데이트
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // 이전에 공중에 있다가 땅에 닿은 경우에만 착지 처리
        if (isGrounded && !wasGrounded && isJumping)
        {
            isJumping = false;
            anim.SetBool("Jump", false); // 점프 애니메이션 종료
            anim.SetBool("Landing", true); // 착지 애니메이션 시작
        }
    }

    // 땅 체크 디버그용 (씬 뷰에서 땅 체크 범위 확인)
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
