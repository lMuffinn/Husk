using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{

    public GameObject textBox;
    public bool showText = false;
    public GameObject text;
    string boxText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textBox.SetActive(showText);
        text.GetComponent<TextMeshProUGUI>().text = boxText;
    }
}
