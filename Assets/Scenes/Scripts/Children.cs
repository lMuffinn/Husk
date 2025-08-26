using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Children : MonoBehaviour
{
    public GameObject button;
    public string itemName;
    public void AddItem()
    {
        GameObject item;
        item = Instantiate(button,transform);
        item.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
    }
}
