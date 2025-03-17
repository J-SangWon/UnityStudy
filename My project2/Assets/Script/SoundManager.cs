using System.Security.Cryptography;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //싱글톤
    public static SoundManager instance; //자기 자신을 변수로 담기
    public AudioSource myAudio; //AudioSouce 컴포넌트를 변수로 담기
    public AudioClip SoundBullet;
    public AudioClip DieSound;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (SoundManager.instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로운 인스턴스를 파괴
        }

    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SoundDie()
    {
        myAudio.PlayOneShot(DieSound); //몬스터 사망음
    }
    public void PlayBulletSound()
    {
        myAudio.PlayOneShot(SoundBullet); //미사일 발사음
    }

}
