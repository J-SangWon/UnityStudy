using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{

    float currentFireRate;
    bool isReload;
    bool isFineSightMode;

    [SerializeField] Gun currentGun;
    AudioSource audioSource;
    [SerializeField] Vector3 originPos;

    RaycastHit hitInfo;
    [SerializeField] Camera cam;
    [SerializeField] GameObject hitEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originPos = transform.localPosition;
    }

    void Update()
    {
        GunFireRateCalc();

        if (!isReload)
            Fire();
        if (Input.GetKeyDown(KeyCode.R))
            TryReload();
        if (Input.GetButtonDown("Fire2") && !isReload)
            FineSight();
    }

    private void TryReload()
    {
        if (!isReload
            && currentGun.currentBulletCount == currentGun.reloadBulletCount)
            CancleFineSight();
        StartCoroutine(Reload());
    }

    void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;

    }

    void Fire()
    {
        if (Input.GetButton("Fire1")
            && currentFireRate <= 0
            && currentGun.currentBulletCount > 0
            && !isReload)
        {
            currentFireRate = currentGun.fireRate;
            PlaySE(currentGun.fire_Sound);
            currentGun.muzzleFlash.Play();
            currentGun.currentBulletCount--;
            //StopCoroutine(nameof(RetroAcitonCouroutine));
            Hit();
            StopAllCoroutines();
            StartCoroutine(RetroAcitonCouroutine());

        }
        if (currentGun.currentBulletCount == 0)
        {
            TryReload();
        }
    }

    void Hit()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, currentGun.range))
        {
            GameObject clone = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            Destroy(clone, 1.5f);
        }
    }
    IEnumerator Reload()
    {
        CancleFineSight();
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);
            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

        }
        else
        {
            Debug.Log("총알 없음");
        }
        isReload = false;
        yield return null;
    }
    IEnumerator RetroAcitonCouroutine()
    {

        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSigthOriginPos.y, currentGun.fineSigthOriginPos.z);
        if (!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;

            while (currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }

        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSigthOriginPos;

            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            while (currentGun.transform.localPosition != currentGun.fineSigthOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSigthOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    #region 정조준
    void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);

        StopAllCoroutines();

        if (isFineSightMode)
        {
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StartCoroutine(FineSightDeActivateCoroutine());
        }


    }

    public void CancleFineSight()
    {
        isFineSightMode = false;
    }

    IEnumerator FineSightActivateCoroutine()
    {
        while (currentGun.transform.localPosition != currentGun.fineSigthOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSigthOriginPos, 0.2f);
            yield return null;
        }
    }
    IEnumerator FineSightDeActivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }
    #endregion

    void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }
}
