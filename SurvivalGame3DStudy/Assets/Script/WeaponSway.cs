using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] Vector3 originPos;
    Vector3 currentPos;
    [SerializeField] Vector3 limitPos;
    [SerializeField] Vector3 fineSightLinitPos;
    [SerializeField] Vector3 smoothSway;
    [SerializeField] GunController gunController;

    void Start()
    {
        //originPos = this.transform.position;
        originPos = Vector3.zero;
    }

    void Update()
    {
        TrySway();
    }

    void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            Swaying();
        else
            BackToOriginPos();
    }
    void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");
        if (!gunController.isFineSightMode)
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limitPos.x, limitPos.x),
            Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -limitPos.y, limitPos.y),
            originPos.z);
        }
        else
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -fineSightLinitPos.x, fineSightLinitPos.x),
            Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -fineSightLinitPos.y, fineSightLinitPos.y),
            originPos.z);
        }

        transform.localPosition = currentPos;
    }
    void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }

}
