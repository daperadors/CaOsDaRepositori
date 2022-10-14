using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //private Dictionary<GameObject,int> Inventory = new Dictionary<GameObject, int>();
    private Material m;
    private List<Material> Inventory = new List<Material>();
    public List<GameObject> InventoryGUI = new List<GameObject>();
    public GameObject inventory;
    public GameObject selector;
    [SerializeField]
    private TMPro.TextMeshProUGUI quantityItem;
    private int id;
    private bool activeMenu;

    private void Awake()
    {
        SetMenuElementsActive(false);
    }

    public void addInventory(Material newItem)
    {
        if (Inventory.Contains(newItem))
        {
            newItem.quantity++;
        }
        else
        {
            newItem.quantity++;
            Inventory.Add(newItem);
            addItemToInventoryGui(newItem);

        }
        
    }
    public void addItemToInventoryGui(Material newItem)
    {
        for (int i =0; i < InventoryGUI.Count; i++)
        {
            if(InventoryGUI[i].GetComponent<Image>().enabled == false)
            {
                InventoryGUI[i].GetComponent<Image>().enabled = true;
                InventoryGUI[i].GetComponent<Image>().sprite = newItem.sprite;
                break;
            }

        }
    }
    public void Update()
    {
        if (!activeMenu)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) && id < InventoryGUI.Count-1)
        {
            id++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && id > 0)
        {
            id--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && id > 3)
        {
            id-=6;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && id < 18)
        {
            id += 6;
        }
        if (id>-1 && id<18)
        {
            
        selector.transform.position = InventoryGUI[id].transform.position;

        if (Inventory.Count>id)
        {
            quantityItem.text = Inventory[id].quantity.ToString();
        }
        else
        {
            quantityItem.text = "";
        }
        
        }
    }

    public void OnMenuOpened()
    {
        SetMenuElementsActive(true);
    }

    public void OnMenuClosed()
    {
        SetMenuElementsActive(false);
    }

    private void SetMenuElementsActive(bool state)
    {
        activeMenu = state;
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(state);

        GetComponent<Image>().enabled = state;
    }
}
