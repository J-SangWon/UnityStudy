using UnityEngine;

public class MBullet : MonoBehaviour
{
    public float Speed = 4f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            

            //플레이어 지우기

        }
    }

}
