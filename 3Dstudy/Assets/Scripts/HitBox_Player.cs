using TMPro;
using UnityEngine;

public class HitBox_Player : MonoBehaviour
{
    public Animator playerAnim;
    public TextMeshProUGUI message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Col_Enemy_Atk"))
        {
            message.text = "Player Damaged!";
            message.gameObject.SetActive(true);
        }

        if (gameObject.CompareTag("Defence"))
        {
            message.text = "Block!";
            message.gameObject.SetActive(true);
        }

        if (gameObject.CompareTag("Parrying"))
        {
            playerAnim.Play("ARPG_Samurai_Parrying");
            message.text = "Parrying!";
            message.gameObject.SetActive(true);
        }

    }

}
