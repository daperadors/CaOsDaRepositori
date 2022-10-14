using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Controller : MonoBehaviour
{
    public GameObject CasaMenu;

    private void Awake()
    {
        SetHouseMenuActive(false);
    }


    public void OnHouseOpened()
    {
        SetHouseMenuActive(true);
    }

    public void OnHouseClosed()
    {
        SetHouseMenuActive(false);
    }

    private void SetHouseMenuActive(bool state)
    {
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(state);

    }
}
