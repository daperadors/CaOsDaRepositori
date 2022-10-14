using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearSoldado : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ElementASpawnejar;
    public void CreateSoldier()
    {
        GameObject spawned = Instantiate(m_ElementASpawnejar);
        spawned.transform.position = this.transform.position;
        print("spawned");
    }
}
