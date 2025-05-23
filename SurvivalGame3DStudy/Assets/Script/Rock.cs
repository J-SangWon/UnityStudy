using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float destroyTime;
    [SerializeField] SphereCollider sphereCol;
    [SerializeField] GameObject go_rock;
    [SerializeField] GameObject go_debris;
    [SerializeField] GameObject go_Effect;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip effectSound;
    [SerializeField] AudioClip effectSound2;
        

    public void Mining()
    {
        hp--;
        audioSource.clip = effectSound;
        audioSource.Play();
        var clone = Instantiate(go_Effect, sphereCol.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);
        if (hp < 0)
            Destruction();
    }

    void Destruction()
    {
        audioSource.clip = effectSound2;
        audioSource.Play();

        sphereCol.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }

}
