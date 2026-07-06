using UnityEngine;

public class Walls_dev_tje : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.CompareTag("Player");
        if (isPlayer)
        {
            SpriteRenderer spr = collision.GetComponentInChildren<SpriteRenderer>();
            Color currentColor = spr.color;
            currentColor.a = 0.5f;
            spr.color = currentColor;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        bool isPlayer = collision.CompareTag("Player");
        if (isPlayer)
        {
            SpriteRenderer spr = collision.GetComponentInChildren<SpriteRenderer>();
            Color currentColor = spr.color;
            currentColor.a = 1f;
            spr.color = currentColor;
        }
    }
}
