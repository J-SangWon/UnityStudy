using UnityEngine;

public class NamedEnemy : MonoBehaviour
{
    public int NamedHP = 2;
    public float MoveSpeed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        float distanceY = MoveSpeed * Time.deltaTime;
        transform.Translate(0, -distanceY, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
