using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 20;
    public float MonsterSpeed = 1f;
    public float delay = 1f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    //아이템 가져오기
    public GameObject Item = null;


    void Start()
    {
        Invoke("CreateBullet", delay);

        
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);
        //재귀 호출
        Invoke("CreateBullet", delay);
    }
    void Update()
    {
        transform.Translate(Vector3.down * MonsterSpeed * Time.deltaTime);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        HP -= attack;
        if (HP <= 0) 
        { 
            ItemDrop();
            //Destroy(gameObject);
            PoolManager.Instance.Return(gameObject);
        }
    }

    public void ItemDrop()
    {
        //아이템 생성
        Instantiate(Item, transform.position, Quaternion.identity);
    }



}
