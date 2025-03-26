using UnityEngine;

public class Landdust : MonoBehaviour
{
    public float lifetime = 0.5f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    void Start()
    {
        
    }

    void Update()
    {

    }
}
