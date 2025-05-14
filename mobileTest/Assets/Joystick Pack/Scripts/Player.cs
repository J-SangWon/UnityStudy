using UnityEngine;

public class Player : MonoBehaviour
{
    public DynamicJoystick joystick;
    public float moveSpeed = 5f;
    public GameObject misslePrefab;
    public bool fire = false;

    void Start()
    {

    }

    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 move = new Vector3(x, y, 0);
        transform.Translate(move * Time.deltaTime * moveSpeed);
        if (fire)
        {
            FireMissle();
        }
    }

    public void FireMissle()
    {
        GameObject missle = Instantiate(misslePrefab, transform.position, Quaternion.identity);
    }

    public void FireEnter()
    {
        fire = true;
    }
    public void FireExit()
    {
        fire = false;
    }

}
