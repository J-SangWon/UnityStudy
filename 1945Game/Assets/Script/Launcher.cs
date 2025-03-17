using UnityEngine;

public class Launcher : MonoBehaviour
{
    //public GameObject Bullet;
    //public GameObject Bullet2;
    //public GameObject Bullet3;
    //public GameObject Bullet4;
    public GameObject[] Bul;
    Player player;

   
    void Start()
    {
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    void Shoot()
    {
        if(Player.instance.ItemCount == 0) Instantiate(Bul[0], transform.position, Quaternion.identity);
        else if (Player.instance.ItemCount == 1) Instantiate(Bul[1], transform.position, Quaternion.identity);
        else if (Player.instance.ItemCount == 2) Instantiate(Bul[2], transform.position, Quaternion.identity);
        else if (Player.instance.ItemCount >= 3) Instantiate(Bul[3], transform.position, Quaternion.identity);
        SoundManager.instance.BulletShootSound();

    }

    void Update()
    {
        
    }
}
