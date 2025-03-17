using System.Collections;
using TMPro;
using UnityEngine;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    float lerpTime = 0.1f;
    //텍스트 컴포넌트
    TextMeshProUGUI textBossWarning;
    void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    //OnEnable메서드 : 오브젝트 활성화 될떄 호출
    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }
    
    //색상 전환 코루틴
    IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerpLoop(Color.white, Color.red));
            yield return StartCoroutine(ColorLerpLoop(Color.red, Color.white));
        }
    }

    IEnumerator ColorLerpLoop(Color StartColor, Color EndColor)
    {
        float CurrentTime = 0.0f;
        float Percent = 0.0f;

        while (Percent < 1)
        {
            CurrentTime += Time.deltaTime;
            Percent = CurrentTime / lerpTime;
            textBossWarning.color = Color.Lerp(StartColor, EndColor, Percent);
            yield return null;
        }

    }


}
