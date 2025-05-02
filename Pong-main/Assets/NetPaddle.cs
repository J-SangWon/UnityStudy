using UnityEngine;
using Photon.Pun;

public class NetPaddle : MonoBehaviourPun
{
    public float speed = 10f;

    void Update()
    {

        //내가 소유한 오브젝트인지 확인하는 조건문이다.
        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, move, 0);
        }
    }
}
