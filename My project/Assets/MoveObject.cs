using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f; //속도 조절
    public float jumpForce = 5.0f; // 점프 힘

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // 좌우 이동
        float moveY = Input.GetAxis("Vertical"); // 위아래 이동
        float moveZ = Input.GetAxis("Vertical"); // 앞뒤 이동
        Vector3 move = new Vector3(moveX, 0, moveZ);
        //방향*속도*Time.deltaTime
        // position 과 trantlate 둘다 동일하게 이동함
        //transform.position = move * speed * Time.deltaTime;
        transform.Translate(move * speed * Time.deltaTime);

        // Space 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
