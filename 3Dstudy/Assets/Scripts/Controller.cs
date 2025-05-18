using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Camera")]
    public Transform camAxis_Central; // 카메라 축
    public Transform cam; // 카메라
    public float camSpeed; // 카메라 회전 속도
    float mouseX; // 마우스 X축 이동
    float mouseY; // 마우스 Y축 이동
    float wheel; // 마우스 휠

    [Header("Player")]
    public Transform playerAxis; // 플레이어 축
    public Transform player; // 플레이어
    public float playerSpeed; // 플레이어 이동 속도
    public Vector3 movement;
    public Animator anim;

    void Start()
    {
        wheel = -5;
        mouseY = 3;

        anim = player.GetComponent<Animator>();
    }

    void CamMove()
    {
        mouseX += Input.GetAxis("Mouse X"); // 마우스 X축 이동
        mouseY += Input.GetAxis("Mouse Y") * -1; // 마우스 Y축 이동

        if (mouseY > 10)
            mouseY = 10;
        if (mouseY < -5)
            mouseY = -5;

        camAxis_Central.rotation = Quaternion.Euler(
            new Vector3(camAxis_Central.rotation.x + mouseY, camAxis_Central.rotation.y + mouseX, 0)
                * camSpeed
        ); // 카메라 회전
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10; // 마우스 휠 이동
        if (wheel >= -5)
            wheel = -5;
        if (wheel <= -20)
            wheel = -20;

        cam.localPosition = new Vector3(0, 0, wheel); // 카메라 위치
    }

    void PlayerMove()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 플레이어 이동

        if (movement != Vector3.zero)
        {
            playerAxis.rotation = Quaternion.Euler(
                new Vector3(0, camAxis_Central.rotation.y + mouseX, 0) * camSpeed
            ); // 플레이어 회전

            playerAxis.Translate(movement * playerSpeed * Time.deltaTime); // 플레이어 이동

            player.localRotation = Quaternion.Slerp(
                player.localRotation,
                Quaternion.LookRotation(movement),
                5 * Time.deltaTime
            ); // 플레이어 회전

            anim.SetBool("Walk", true); // 플레이어 이동()
        }
        else
            anim.SetBool("Walk", false); // 플레이어 정지()

        camAxis_Central.position = new Vector3(
            player.position.x,
            player.position.y + 3,
            player.position.z
        ); // 카메라 위치
    }

    void Update()
    {
        CamMove(); // 카메라 회전
        Zoom(); // 카메라 위치 조정
        PlayerMove(); // 플레이어 이동
    }
}
