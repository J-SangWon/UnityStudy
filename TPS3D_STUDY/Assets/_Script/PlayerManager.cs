using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    StarterAssetsInputs input;
    ThirdPersonController controller;
    Animator anim;
    Enemy enemy;

    [Header("Aim)")]
    [SerializeField] CinemachineCamera aimCam;
    [SerializeField] GameObject aimImage;
    [SerializeField] GameObject aimObject;
    [SerializeField] float aimObjectDistance = 20f;
    [SerializeField] LayerMask targetLayer;

    [Header("IK")]
    [SerializeField] Rig handRig;
    [SerializeField] Rig aimRig;
    


    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AimCheck();

    }

    private void AimCheck()
    {
        if (input.reload)
        {
            ReloadProcess();
        }
        if (controller.isReload) return;

        if (input.aim)
        {
            AimControl(true);
            anim.SetLayerWeight(1, 1);

            Vector3 targetPosition = Vector3.zero;
            Transform camTransform = Camera.main.transform;
            RaycastHit hit;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, targetLayer))
            {
                //Debug.Log("Name -> " + hit.transform.gameObject.name);
                targetPosition = hit.point;
                aimObject.transform.position = hit.point;

                enemy = hit.collider.gameObject.GetComponent<Enemy>();
            }
            else
            {
                targetPosition = camTransform.position + camTransform.forward;
                aimObject.transform.position = camTransform.position + camTransform.forward * aimObjectDistance;
            }
            Vector3 targetAim = targetPosition;
            targetAim.y = transform.position.y;
            Vector3 aimDir = (targetAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 50f);
            SetRigWeight(1f);

            if (input.shoot)
            {
                if(GameManager.instance.currentBullet <= 0)
                {
                    ReloadProcess();
                    return;
                }
                anim.SetBool("Shoot", true);
                GameManager.instance.Shooting(targetPosition, enemy);
            }
            else
            {
                anim.SetBool("Shoot", false);
            }

        }
        else
        {
            AimControl(false);
            anim.SetLayerWeight(1, 0);
            anim.SetBool("Shoot", false);
            SetRigWeight(0f);
        }



    }

    private void ReloadProcess()
    {
        input.reload = false;
        if (controller.isReload)
        {
            return;
        }
        AimControl(false);
        SetRigWeight(0f);
        anim.SetLayerWeight(1, 1);
        anim.SetTrigger("Reload");
        controller.isReload = true;
    }

    private void AimControl(bool isCheck)
    {
        aimCam.gameObject.SetActive(isCheck);
        aimImage.SetActive(isCheck);
        controller.isAimMove = isCheck;
    }

    public void Reload()
    {
        controller.isReload = false;
        SetRigWeight(1f);
        anim.SetLayerWeight(1, 0);

    }

    void SetRigWeight(float weight)
    {
        aimRig.weight = weight;
        handRig.weight = weight;
    }

    void ReloadWeaponClip()
    {
        GameManager.instance.ReloadClip();
    }



}
