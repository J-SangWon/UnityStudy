using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    void Start()
    {

    }

    void Update()
    {
        MoveControl();



    }
    
    void MoveControl()
    {
        float distanceX = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        transform.Translate(distanceX, 0, 0);
    }

}
