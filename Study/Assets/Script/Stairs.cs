using UnityEngine;

public class Stairs : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
