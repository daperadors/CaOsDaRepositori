using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float m_velocity=1;

    private bool activeInv = false;
    public InventoryController inventory;
    
    private Camera cam;
    [SerializeField, Range(1f,20f)]private float rotationSpeed;

    private Animator m_animator;

    private float m_nextFire;
    public float FireRate = 2.0f;
    public GameObject Bullet;
    public float BulletSpeed;
    public string estatAnim;
    public bool death;
    public delegate void PlayerDeath();
    public event PlayerDeath OnPlayerDeath;
    private Vector2 movimiento;
    
    public Vector2 movimientoGet
    {
        get { return movimiento; }
        set { movimiento = value; }
    }

    [SerializeField] private GameObject WaypointMina1;
    [SerializeField] private GameObject WaypointMina2;
    [SerializeField] private SpriteRenderer sr;
    private float GranadaRate = 5.0f;
    private int numgranadas;
    [SerializeField] public GameObject Granada;
    private bool activeHouse;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        cam = Camera.main;
        death = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeInv || death || activeHouse)
            return;
        m_nextFire = m_nextFire + Time.fixedDeltaTime;

        if (Input.GetMouseButton(1) && m_nextFire > FireRate)
        {
            Vector3 m_mousePosition = Input.mousePosition;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            m_mousePosition.z = 0;

            float m_fireAngle = Vector2.Angle(m_mousePosition - this.transform.position, Vector2.up);

            if (m_mousePosition.x > this.transform.position.x)
            {
                m_fireAngle = -m_fireAngle;
            }
            
            m_nextFire = 0;

            GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;

            m_bullet.GetComponent<Rigidbody2D>().velocity = ((m_mousePosition - transform.position).normalized * BulletSpeed);

            m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);

        }

        if (numgranadas < 3)
        {
            if (Input.GetKeyDown(KeyCode.Q) && m_nextFire > GranadaRate && numgranadas < 3)
            {
                m_nextFire = 0;
                GameObject m_granada = Instantiate(Granada, transform.position, Quaternion.identity) as GameObject;
                numgranadas++;
            }

        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movimiento = new Vector2(moveX, moveY).normalized;

        m_animator.SetFloat("Horizontal", moveX);
        m_animator.SetFloat("Vertical", moveY);
        m_animator.SetFloat("Speed", movimiento.sqrMagnitude);


    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento.normalized * m_velocity * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Enemigo")
        {
            Death();
        }
        if (collision.gameObject.tag == "Mine")
        {
            transform.position = WaypointMina1.transform.position;
            sr.sortingOrder = 5;
        }
        if (collision.gameObject.tag == "BuildBaixar")
        {
            
            transform.position = WaypointMina2.transform.position;
            sr.sortingOrder = 3;
        }
        
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
    void Death()
    {
        OnPlayerDeath.Invoke();
        death = true;
        
    }

}
