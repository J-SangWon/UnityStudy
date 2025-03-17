using UnityEngine;

public class Homming : MonoBehaviour
{
    public GameObject target;
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        //플레이어 태그로 찾기
        target = GameObject.FindGameObjectWithTag("Player");
        //A - B  A바라보는 벡터     플레이어 - 미사일 
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        dirNo = dir.normalized;
    }

    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
