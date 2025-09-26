using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public float checkRadius = 6;
    public Transform eyes;
    public LayerMask player;
    public LayerMask ignore;
    public GameObject target;
    RaycastHit2D canSeePlayer;
    public bool seePlayer;
    public float objectpermanance = 2;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = objectpermanance;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, checkRadius, player))
        {
            Vector3 direction = (target.GetComponent<Transform>().position - GetComponent<Transform>().position);
            canSeePlayer = Physics2D.Raycast(GetComponent<Transform>().position, direction, checkRadius, ignore);
            Debug.DrawRay(GetComponent<Transform>().position, direction);
            if (!canSeePlayer) timer = objectpermanance;
        }
        timer -= Time.deltaTime;
        if (timer < 0) seePlayer = false;
        else if (timer >= 0) seePlayer = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(eyes.position,checkRadius);
        
    }
}
