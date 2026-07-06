using UnityEngine;

public class FloorLevel_dev_tje : MonoBehaviour
{
    [SerializeField] int _floor = 10;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Movement>();
        if (player)
        {
            player.ChangeFloor(_floor);
        }
    }
}
