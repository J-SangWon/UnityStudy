using UnityEngine;

public class ConditionExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public int health = 100;
    // Update is called once per frame
    void Update()
    {
        health -= 1;
        if(health <= 0)
        {
            Debug.Log("체력이 없음");
            health = 0;
        }
    }
}
