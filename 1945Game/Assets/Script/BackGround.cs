using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float newOffsetY = material.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);
        material.mainTextureOffset = newOffset;


    }
}
