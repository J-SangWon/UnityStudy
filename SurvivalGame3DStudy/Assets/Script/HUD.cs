using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] GunController gunController;
    Gun currentGun;
    [SerializeField] GameObject go_BulletHUD;
    [SerializeField] Text[] text_Bullet;

    void Update()
    {
        CheckBullet();
    }

    void CheckBullet()
    {
        currentGun = gunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.currentBulletCount.ToString();
        text_Bullet[2].text = currentGun.reloadBulletCount.ToString();


    }

}
