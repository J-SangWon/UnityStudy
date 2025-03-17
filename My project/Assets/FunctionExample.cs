using UnityEngine;

public class FunctionExample : MonoBehaviour
{
    public void SayHello()
    {
        Debug.Log("Hello");
    }
    int AddNumber(int a, int b)
    {
        return a + b;
    }
    void Start()
    {
        SayHello();
        Debug.Log(AddNumber(3, 5));
    }

    void Update()
    {
        
    }
}
