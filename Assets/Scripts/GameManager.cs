using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Pathfinding))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_OnMenuOpened;
    [SerializeField]
    private GameEvent m_OnMenuClosed;
    [SerializeField]
    private GameEvent m_OnHouseOpened;
    [SerializeField]
    private GameEvent m_OnHouseClosed;
    [SerializeField]
    public Casa casa;
    private bool m_ActiveMenu = false;
    public static GameManager Instance { get; private set; }

    public GameObject m_ElementASpawnejar;
    [SerializeField]
    private float m_SpawnWait = 30f;
    public CharacterController cc;

    [SerializeField]
    private Transform [] Spawners;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
    private void Start()
    {
        m_OnMenuClosed.Raise();
        m_OnHouseClosed.Raise();
        cc.OnPlayerDeath += PlayerDead;

        for (int i = 0; i < Spawners.Length; i++) { 
           StartCoroutine(SpawnCoroutine(Spawners[i]));
        }

        StartCoroutine(IncreaseDifficulty());
    }
    private void PlayerDead()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("GameOver");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_ActiveMenu = !m_ActiveMenu;
            if (m_ActiveMenu)
                m_OnMenuOpened.Raise();
            else
                m_OnMenuClosed.Raise();
        }



        if (casa.CasaOpened)
        {
            m_OnHouseOpened.Raise();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_OnHouseClosed.Raise();
                casa.CasaOpened = false;
            }
        }
    }

    public void FindPath(Vector3 origen, Vector3 destino, out List<Vector3> camino)
    {
        GetComponent<Pathfinding>().FindPathWorldSpace(origen, destino, out camino);    
    }

    IEnumerator SpawnCoroutine(Transform PositionSpawn)
    {

        
        while (true)
        {

            GameObject spawned = Instantiate(m_ElementASpawnejar);
            spawned.GetComponent<EnemigoControlador>().cc = cc;
            spawned.transform.position = PositionSpawn.position;
            yield return new WaitForSeconds(m_SpawnWait);
        }
    }
    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            m_SpawnWait -= 0.5f;

            if (m_SpawnWait <= 3f)
            {
                m_SpawnWait = 3f;
            }

        }

    }
}
