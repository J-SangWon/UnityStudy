using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance;
    public Text scoreText; //점수 표시하는 Text객체를 에디터에서 받아옴
    public Text StartText;
    public int score = 0;
    public int EnemyCount = 0;
    private void Awake()
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
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        int i = 3;
        while(i > 0)
        {
            StartText.text = i.ToString();
            yield return new WaitForSeconds(1);
            i--;
            if(i == 0)
            {
                StartText.gameObject.SetActive(false);
            }

        }
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score : " + score;
        EnemyCount++;
    }

}
