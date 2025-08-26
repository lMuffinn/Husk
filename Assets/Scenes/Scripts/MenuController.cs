using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject use;
    public GameObject player;

    public void UseMenu()
    {
        GameObject menu;
        menu = Instantiate(use, transform);
    }
    public void Exit()
    {
        Destroy(transform.parent.gameObject);
    }
    public void UseItem()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        string name = transform.parent.parent.GetComponentInChildren<TextMeshProUGUI>().text;
        if (name == "Sledge Hammer")
        {
            player.GetComponent<Items>().Sledgehammer();
        }
        Debug.Log(name);
        Exit();
    }
}
