using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{

    //public string name;
    public TextManager textManager;
    public string[] text;
    public Collider2D objectRange;
    public Collision2D collision;
    public LayerMask player;
    public float textSpeed = .1f;
    bool typing = false;
    int current = 0;
    GameObject canvas;
    bool skip = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas = textManager.canvas;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectRange.IsTouchingLayers(player) && Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(true);
            if (!typing)
            {
                if(current == text.Length)
                {
                    canvas.SetActive(false);
                    current--;
                }
                else
                {
                    typing = true;
                    StartCoroutine(SlowTextFill(current));
                    current++;
                }
            }
            else skip = true;
        }
    }

    public IEnumerator SlowTextFill(int current) 
    {
        string unfinished = "";
        for (int i = 0; i<text[current].Length;i++)
        {
            if (skip)
            {
                i = text[current].Length;
                skip = false;
                textManager.text.text = text[current];
            }
            else
            {
                unfinished += text[current][i];
                textManager.text.text = unfinished;
                yield return new WaitForSeconds(textSpeed); 
            }
        }
        typing = false;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("exited");
        if (col.gameObject.name == "Player")
        {
            current--;
            canvas.SetActive(false);
        }
    }
}
