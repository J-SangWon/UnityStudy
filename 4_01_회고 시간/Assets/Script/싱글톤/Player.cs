using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 100;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            EventManager.Instance.TriggerEvent("PlayerHealthChanged", _health);
            if(_health <= 0)
            {
                EventManager.Instance.TriggerEvent("PlayerDied");
            }
        }
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
    }
}
