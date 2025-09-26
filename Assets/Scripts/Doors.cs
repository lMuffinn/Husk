using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float doorRadius;
    public LayerMask doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D door = Physics2D.OverlapCircle(transform.position, doorRadius, doors);
        if (Physics2D.OverlapCircle(transform.position, doorRadius, doors) && Input.GetKeyDown(KeyCode.E))
        {
            door.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = !door.gameObject.GetComponentInChildren<SpriteRenderer>().enabled;
            door.gameObject.GetComponentInChildren<Collider2D>().isTrigger = !door.gameObject.GetComponentInChildren<Collider2D>().isTrigger;
        }
    }
}
