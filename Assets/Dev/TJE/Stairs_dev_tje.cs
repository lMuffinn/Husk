using UnityEngine;
using UnityEngine.Rendering;

public class Stairs_dev_tje : MonoBehaviour
{
    [SerializeField] int _floor = 10;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayer = collision.CompareTag("Player");
        if (isPlayer)
        {
            collision.GetComponent<Movement>().ChangeFloor(_floor);
        }
    }
}
