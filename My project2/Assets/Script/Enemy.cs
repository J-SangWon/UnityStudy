using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float MoveSpeed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        float distanceY = MoveSpeed * Time.deltaTime;
        transform.Translate(0, -distanceY, 0);
        
    }

    //화면 밖으로 나간경우
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
