using UnityEngine;

public class VariableExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"PlayerName : {playerName}");
        Debug.Log($"PlayerScore : {PlayerScore}");
        Debug.Log($"speed : {speed}");
        Debug.Log($"isGameOver : {isGameOver}");
    }

    public string playerName = "Hero";
    public int PlayerScore = 0;
    public float speed = 5.5f;
    public bool isGameOver = false;



}
