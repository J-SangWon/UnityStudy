using UnityEngine;

public class Player 
{
    public string Name;
    public int score;

    public Player(string Name, int score)
    {
        this.Name = Name;
        this.score = score;
    }
    public void PlayerInfo()
    {
        Debug.Log($"{Name}, {score}");
    }
}
public class ClassExample : MonoBehaviour
{
    void Start()
    {
        Player player = new Player("Me", 100);
        player.PlayerInfo();
    }

    void Update()
    {
        
    }
}
