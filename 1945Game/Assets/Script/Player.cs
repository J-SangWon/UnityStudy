using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance = null;
    public float moveSpeed = 5f;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    Animator ani; //애니메이터 가져올 변수

    public Transform pos = null;
    //아이템 먹기
    public int ItemCount = 0;
    [SerializeField]
    private GameObject PowerUp; //private 일떄 인스펙터에서 사용하는 방법

    //레이저

    //아이템 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemCount++;

            if (ItemCount >= 3) ItemCount = 3;
            else
            {
                GameObject go = Instantiate(PowerUp, transform.position, Quaternion.identity);
                Destroy(go, 1);
            }


                Destroy(collision.gameObject);
        }

    }
    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        // 화면의 경계를 설정
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);

        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //방향키에 따른 움직임
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        //GetAxis 값 범위 (-1 ~ 1)
        //Left
        if(Input.GetAxis("Horizontal") <= -0.5f)
            ani.SetBool("Left", true);
        else ani.SetBool("Left", false);
        //Right
        if (Input.GetAxis("Horizontal") >= 0.5f)
            ani.SetBool("Right", true);
        else ani.SetBool("Right", false);
        //Up
        if (Input.GetAxis("Vertical") >= 0.5f)
            ani.SetBool("Up", true);
        else ani.SetBool("Up", false);

        // 경계를 벗어나지 않도록 위치 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }

    }

    
}
