using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoControlador : MonoBehaviour
{
    Vector2 EPos;
    bool perseguirCh;
    [SerializeField] public CharacterController cc;
    public int V;
    private int x;
    private int y;
    private bool activeInv = false;
    private Animator m_animator;
    private int vida;
    private string damageStatus;
    private bool deathCharacter;
    private Rigidbody2D rb;
    public GameObject soldadoController;
    private bool activeHouse;
    [SerializeField]
    private GameEvent m_OnEnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        NumRandom();
        cc.OnPlayerDeath += PlayerDead;
        vida = Random.Range(1, 6);
        rb= GetComponent<Rigidbody2D>();

    }
    private void PlayerDead()
    {
        cc.death = true;
        deathCharacter = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (activeInv || deathCharacter || activeHouse)
            return;

        
        if (perseguirCh)
        {
            m_animator.enabled = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

            //Vector3 movimiento = new Vector3(moveX, moveY,0).normalized;
            //rb.AddForce(transform.position + (transform.position- movimiento.normalized) * V * Time.fixedDeltaTime);
           transform.position = Vector2.MoveTowards(transform.position,EPos, V*Time.deltaTime);


            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            
            if (cc.movimientoGet.sqrMagnitude == 0)
                moveY = 1;
            if(transform.position.y <cc.transform.position.y)
                moveY = 1;
            if (transform.position.y > cc.transform.position.y)
                moveY = -1;
            m_animator.SetFloat("Horizontal", moveX);
            m_animator.SetFloat("Vertical", moveY);
            m_animator.SetFloat("Speed", transform.position.sqrMagnitude);

        }
        else {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
        }



    }
    
    private void OnTriggerStay2D(Collider2D c){
        if (c.tag.Equals("Character"))
        {
            EPos = cc.transform.position;
            perseguirCh = true;
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag.Equals("Character"))
        {
            perseguirCh = false;
            NumRandom();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Bloque")
        {
            NumRandomOther();
        }
        if (c.tag == "Mountain-tree" || c.tag == "Casas") {
            NumRandom();        
        }
        if (c.gameObject.tag.Equals("Bullet"))
        {
            vida--;
            //m_animator.Play(damageStatus);
            Destroy(c.gameObject);
            if (vida == 0)
                OnEnemyDeath();
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MontañaDestructora")
        {
            Destroy(this.gameObject);
            
        }
        if (collision.gameObject.tag == "Mountain-tree")
        {
            y = -y;

        }
        if (collision.gameObject.tag == "Tree")
        {
            y = -y;
            NumRandomOther();

            
        }


    }

    private void NumRandom() {
        x = Random.Range(0, 2);
        if (x == 0)
        {x = 1;}
        else {x = -1;}
        y = Random.Range(0, 2);
        if (y == 0)
        { y = 1; }
        else { y = -1; }
    }

    private void NumRandomOther()
    {
        x = x * -1;
        y = y * -1;
    }

    public void OnMenuOpened()
    {
        activeInv = true;
    }

    public void OnMenuClosed()
    {
        activeInv = false;
    }
    public void OnHouseOpened()
    {
        activeHouse = true;
    }

    public void OnHouseClosed()
    {
        activeHouse = false;
    }
    public void OnEnemyDeath()
    {
        Destroy(this.gameObject);
        m_OnEnemyDeath.Raise();
    }
}
