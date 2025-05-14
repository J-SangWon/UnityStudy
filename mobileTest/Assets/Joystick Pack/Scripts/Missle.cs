using UnityEngine;

public class Missle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 5f);

    }

    private void OnDestroy()
    {
        // Destroy the missile after 2 seconds
        Destroy(gameObject, 2f);
    }
}
