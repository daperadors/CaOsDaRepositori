using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Casa : MonoBehaviour
{
    [SerializeField] private GameEvent OnHouseOpen;
    public bool CasaOpened = false;
    private void OnMouseDown()
    {
        CasaOpened = true;
        OnHouseOpen.Raise();
    }

}
                    