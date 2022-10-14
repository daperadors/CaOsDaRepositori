using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class TreeController : MonoBehaviour
{
    public CharacterController cc;
    private bool collisionTree = false;
    [SerializeField]
    private Material broteArbol;
    [SerializeField]
    private Material maderaArbol;
    
    private void OnMouseDown()
    {
        Vector3 pJugador = cc.GetComponent<Transform>().position;
        Vector3Int casilla = GetComponent<Tilemap>().WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        casilla.z = 0;
        if (collisionTree)
        {
            GetComponent<Tilemap>().SetTile(casilla, null);
            cc.inventory.addInventory(broteArbol);
            cc.inventory.addInventory(maderaArbol);
            collisionTree = false;

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag== "Character")
        {
            collisionTree = true;
            
        }
            
        
    }
}
