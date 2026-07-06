using UnityEngine;
using UnityEngine.Rendering;

public class Stairs_dev_tje : MonoBehaviour
{
    [SerializeField] int _floor = 10;
    [SerializeField] bool _incline = false;
    [SerializeField] bool _stairsFaceRight = true;
    // [SerializeField] Vector2 direction = new Vector2(0f, 1f);
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Movement>();
        if (player)
        {
            player.ChangeFloor(_floor);
            if (_incline) 
                player.IsOnStairs(true, _stairsFaceRight);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Movement>();
        if (player && _incline)
        {
            player.IsOnStairs(false, _stairsFaceRight);
        }
    }
}
