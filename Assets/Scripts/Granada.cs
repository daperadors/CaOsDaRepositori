using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{

    public bool eliminarzombie;
    public bool activar;
    public GameObject prefabExplosion;
    /*public delegate void MinaElimina();
    public event MinaElimina OnMinaElimina;*/

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mina());
    }

    // Update is called once per frame
    void Update()
    {
        if (activar) {
            if (Input.GetKeyDown(KeyCode.C))
            {
                eliminarzombie = true;
                StartCoroutine(Eliminar());
            }
        }
       

    }

    IEnumerator Mina() {
        yield return new WaitForSeconds(3);
        activar = true;
    }

    IEnumerator Eliminar() {
        GameObject explosion = Instantiate(prefabExplosion, new Vector3(transform.position.x,transform.position.y,-5), transform.rotation) as GameObject;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        Destroy(explosion);
        eliminarzombie = false;
        activar = false;
    }
    


    private void OnTriggerStay2D(Collider2D c)
    {
        if(eliminarzombie == true)
        {
            if(c.tag.Equals("Enemigo"))
             {
                Destroy(c.gameObject);
             }
        }
        
        
    }


}
