using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shadow : MonoBehaviour
{
    private GameObject player;
    public float TwSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Lerp 부드러운 이동에 사용함
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * TwSpeed);

    }
}
//부드러운 이동 (Smooth Movement)
//public Transform target;
//public float speed = 2.0f;

//void Update()
//{
//    transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
//}
//→ Time.deltaTime* speed를 사용하면 t 값이 점점 증가하면서 점진적으로 이동해.

//카메라 천천히 따라가기
//public Transform player;
//public float followSpeed = 5f;

//void LateUpdate()
//{
//    transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * followSpeed);
//}
//→ t 값을 Time.deltaTime * speed로 조정하면, 너무 빠르게 이동하지 않고 부드럽게 따라가.