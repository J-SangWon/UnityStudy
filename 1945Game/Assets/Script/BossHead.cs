using UnityEngine;

public class BossHead : MonoBehaviour
{
    [SerializeField]
    GameObject BossBullet;

    //애니메이션에서 함수 사용
    public void RightDownLanunch()
    {
        GameObject go = Instantiate(BossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(1, -1));
    }
    public void LeftDownLanunch()
    {
        GameObject go = Instantiate(BossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(-1, -1));
    }
    public void DownLanunch()
    {
        GameObject go = Instantiate(BossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(0, -1));
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
