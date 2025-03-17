using System.Threading;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float BulletSpeed = 5f;
    //공격력
    public int Attack = 10;
    //이펙트
    public GameObject effect;
    //아이템 생성
    public GameObject Item;

    void Update()
    {
        transform.Translate(Vector2.up* BulletSpeed * Time.deltaTime);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Effect
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);
            //Sound
            SoundManager.instance.DieSound();
            //Delete
            Destroy(gameObject);
            collision.gameObject.GetComponent<Monster>().Damage(Attack);

        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            //Effect
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);
            //Sound
            SoundManager.instance.DieSound();
            //Delete
            Destroy(gameObject);

        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
