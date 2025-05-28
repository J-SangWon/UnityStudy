using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Rigidbody bulletRb;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float destroyTime = 3f;
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0)
        {
            destroyTime = 3f;
        }
        BulletMove();
    }

    void BulletMove()
    {
        bulletRb.linearVelocity = transform.forward * bulletSpeed;
    }

    void DestroyBullet()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        destroyTime = 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
           other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;
        }

        DestroyBullet();
    }

}
