using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    Transform tTransform;
    public LayerMask Character;
    Vector3 pos;
    public Vector2 size = new Vector2(1, 2);
    Rect rect;
    public int floor = 0;
    // Start is called before the first frame update
    void Start()
    {
        tTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = tTransform.position;
        Collider2D[] col = Physics2D.OverlapAreaAll(new Vector2(pos.x - size.x / 2, pos.y - size.y / 2), new Vector2(pos.x + size.x / 2, pos.y + size.y / 2), Character);
        for (int i = 0; i < col.Length; i++)
        {
            //Debug.Log(i + ": " + col[i].gameObject.name);
            col[i].gameObject.GetComponent<Floor>().floor = floor;
        }
    }
    private void OnDrawGizmos()
    {
        rect.width = size.x;
        rect.height = size.y;
        rect.x = GetComponent<Transform>().position.x - size.x / 2;
        rect.y = GetComponent<Transform>().position.y - size.y / 2;
        Gizmos.color = Color.gray;
        DrawRect(rect);
    }
    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
