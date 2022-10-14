using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldadosController : MonoBehaviour
{
    private bool m_seleccionado = false;
    List<Vector3> m_Camino;
    private int velocitat;
    private Animator m_animator;
    public GameObject Bullet;
    public float BulletSpeed;
    private float m_nextFire;
    public float FireRate = 2.0f;
    private Vector3 m_PositionEnemigo;
    private bool m_disparando;
    
    private void Awake()
    {
        m_Camino = new List<Vector3>();
        m_seleccionado = false;
}
    private void Start()
    {
        m_disparando = false;
        m_animator = GetComponent<Animator>();
        
    }

    public void EnemyDeath()
    {
        print("muere");
        m_disparando = false;
        //StopCoroutine(Disparar());
        StopAllCoroutines();
    }

    void Update()
    {
        if (!m_seleccionado)
            return;
        
        if (Input.GetMouseButtonDown(1))
        {
            
            Vector3 destino = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print(destino);
            m_Camino.Clear();
            GameManager.Instance.FindPath(transform.position, destino, out m_Camino);
            m_seleccionado = false;
            StartCoroutine(Mover());
            //m_moviments = 1;
            if (destino.y > transform.position.y)
            {
                m_animator.Play("WalkArriba");
            }
            if (destino.y < transform.position.y)
            {
                m_animator.Play("WalkBajo");
            }

        }
    }
    private IEnumerator Mover()
    {

        int nodoDestino = 1;
        while(nodoDestino < m_Camino.Count)
        {
            //Mantener transform.position.z del soldado para que no vaya a otra z al moverse.
            Vector2 direccion = m_Camino[nodoDestino] - transform.position;
            direccion.Normalize();
            Vector3 direccion3D = new Vector3(direccion.x, direccion.y, transform.position.z);

            while (Vector2.Distance(transform.position, m_Camino[nodoDestino]) > 0.1f)
            {
                GetComponent<Rigidbody2D>().MovePosition(transform.position + direccion3D * 1 * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            nodoDestino++;
        }
        m_animator.Play("StayBajo");
    }
    private void OnMouseDown()
    {
         m_seleccionado = true;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        
        if (c.tag.Equals("Enemigo"))
        {
            print(m_disparando);
            if (m_disparando==false)
            {
                StartCoroutine(Disparar());
                print("Disparando");
                m_disparando = true;
            }
            
            


        }
    }
    private void OnTriggerStay2D(Collider2D c)
    {
        if(c.gameObject.tag=="Enemigo")
            m_PositionEnemigo = c.transform.position;
            

    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag.Equals("Enemigo"))
        {
            m_disparando=false;
            StopCoroutine(Disparar());
            

            

        }
    }

    private IEnumerator Disparar()
    {
       
        while (true)
        {
            GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
            m_bullet.GetComponent<Rigidbody2D>().velocity = ((m_PositionEnemigo - transform.position).normalized * BulletSpeed);
            yield return new WaitForSeconds(1);
        }
        


    }
}
