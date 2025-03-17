using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource Audio;
    public AudioClip bulletSound;
    public AudioClip MonsterDieSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void DieSound()
    {
        Audio.PlayOneShot(MonsterDieSound);
    }
    public void BulletShootSound()
    {
        Audio.PlayOneShot(bulletSound);
    }

}
