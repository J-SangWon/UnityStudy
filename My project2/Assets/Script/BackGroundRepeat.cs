using UnityEngine;

public class BackGroundRepeat : MonoBehaviour
{
    //스크롤할 속도 지정
    public float scrollSpeed = 0.3f;
    //쿼드의 머터리얼 데이터 받아올 객체 선언
    private Material thisMaterial;


    void Start()
    {
        //객체 생성될 경우 최초 1회 호출되는 함수
        //현재 객체의 Component들을 참조해 Renderer라는 컴포넌트의 Meterial정보
        thisMaterial = GetComponent<Renderer>().material;

    }

    void Update()
    {
        //새롭게 지정해줄 offset 객체 선언
        Vector2 NewOffset = thisMaterial.mainTextureOffset;
        //Y부분에 현재 y값에 속도에 프레임 보정을 더해줌
        NewOffset.Set(0, NewOffset.y + (scrollSpeed * Time.deltaTime));
        thisMaterial.mainTextureOffset = NewOffset;
    }
}
