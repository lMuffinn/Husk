using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Cinemachine;

public class Items : MonoBehaviour
{
    public float itemRadius = 2;
    public LayerMask itemLayer;
    Transform tr;
    public GameObject[] items;
    int itemsCurrent = 0;
    public Transform inventoryPos;
    public GameObject inventory;
    bool inventoryOn = false;
    public GameObject invCanvas;
    public LayerMask Breakable;
    public GameObject moniter;
    public GameObject littleBrother;
    public bool littleBrotherSection = false;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(littleBrother.GetComponent<Transform>().position, itemRadius, itemLayer) && littleBrotherSection)
        {
            Collider2D col = Physics2D.OverlapCircle(littleBrother.GetComponent<Transform>().position, itemRadius, itemLayer);
            InteractableObject interactable = col.GetComponent<InteractableObject>();
            Debug.Log(interactable.gameObject.name);
            items[itemsCurrent] = interactable.gameObject;
            interactable.gameObject.GetComponent<Transform>().position = inventoryPos.position;
            itemsCurrent++;
        }
        else if (Physics2D.OverlapCircle(tr.position, itemRadius, itemLayer) && Input.GetKeyDown(KeyCode.E))
        {
            Collider2D col = Physics2D.OverlapCircle(tr.position, itemRadius, itemLayer);
            InteractableObject interactable = col.GetComponent<InteractableObject>();
            Debug.Log(interactable.gameObject.name);
            if(interactable.gameObject.name == "Attic key")
            {
                GetComponent<Movement>().enabled = false;
                moniter.SetActive(true);
                littleBrother.GetComponent<LoneSection>().enabled = true;
                littleBrother.GetComponent<LoneSection>().target = littleBrother.GetComponent<Transform>();
                littleBrotherSection = true;
                //Camera.GetComponent<CinemachineVirtualCamera>().Follow = littleBrother.GetComponent<Transform>();
            }
            else
            {
                items[itemsCurrent] = interactable.gameObject;
                interactable.gameObject.GetComponent<Transform>().position = inventoryPos.position;
                itemsCurrent++;
            }
            if (!inventoryOn)
            {
                invCanvas.GetComponent<Canvas>().enabled = false;
                inventory.SetActive(true);
                inventory.GetComponent<Children>().itemName = interactable.gameObject.name;
                inventory.GetComponent<Children>().AddItem();
                inventory.SetActive(false);
                invCanvas.GetComponent<Canvas>().enabled = true;
            }
            if (inventoryOn)
            {
                inventory.GetComponent<Children>().itemName = interactable.gameObject.name;
                inventory.GetComponent<Children>().AddItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inventoryOn)
            {
                inventory.SetActive(true);
                inventoryOn = true;
            }
            else if (inventoryOn)
            {
                inventory.SetActive(false);
                inventoryOn = false;
            }
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.gray;
        if (littleBrotherSection)
        {
            Gizmos.DrawWireSphere(littleBrother.GetComponent<Transform>().position, itemRadius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position,itemRadius);
        }
    }

    public void Sledgehammer()
    {
        Collider2D col = Physics2D.OverlapCircle(tr.position, itemRadius, Breakable);
        if (Physics2D.OverlapCircle(tr.position, itemRadius, Breakable))
        {
            Destroy(col.gameObject);
        }
    }
}
