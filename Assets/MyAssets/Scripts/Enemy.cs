using UnityEngine;
using UnityEngine.UI;
using Unity.Burst;
using System.Collections;

[BurstCompile]
public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int valueMoney = 100;

    private Transform target;

    private bool isDead = false;

    public float startHealth;
    private float health;
    private int wavepointIndex = 0;
    private int doubleMoney;

    GameManager gameManager;
    public Animator animator;

    [Header("Unity Stuff")]
    public GameObject hBar;
    public Image healthBar;
    public Camera TDCamera;
    public Camera FPSCamera;
    public MoneyCounter moneyCounter;

    [Header("Droppings")]
    [SerializeField] GameObject[] drops;

    Transform[] wps;

    void Start()
    {
        int random = WaveSpawner.spawnIndex;

        switch (random)
        {
            case 0:
                wps = Waypoints.points;
                break;
            case 1:
                wps = Waypoints1.points;
                break;
            case 2:
                wps = Waypoints2.points;
                break;
            case 3:
                wps = Waypoints3.points;
                break;
            case 4:
                wps = Waypoints4.points;
                break;
        }

        gameManager = GameManager.instance;
        target = wps[0];
        health = startHealth;
        healthBar.fillAmount = health / startHealth;

        InvokeRepeating("SetToCamera", 0f, 0.5f);
    }

    public void SetToCamera()
    {
        hBar.transform.LookAt(hBar.transform.position + TDCamera.transform.rotation * Vector3.back, TDCamera.transform.rotation * Vector3.up);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;  
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        doubleMoney = PlayerPrefs.GetInt("Double", 1);
        gameObject.tag = "DeadEnemy";
        isDead = true;
        PlayerStats.Money += valueMoney * doubleMoney;
        moneyCounter.MoneyUpdate(valueMoney);

        WaveSpawner.EnemiesAlive--;
        speed = 0f;

        GetComponent<BoxCollider>().enabled = false;
        animator.SetBool("Dead", true);

        Destroy(gameObject, 5f);

        int dropIndex = Random.Range(0, 100);
        
        if (dropIndex > 80)
        {
            int index = Random.Range(0, 100);

            if (index < 50)
            {
                GameObject dropping = (GameObject)Instantiate(drops[0], transform.position + new Vector3(0, 1, 0), transform.rotation);
                Destroy(dropping, 100f);
            }
            if (index >= 50 && index < 70)
            {
                GameObject dropping = (GameObject)Instantiate(drops[1], transform.position + new Vector3(0, 1, 0), transform.rotation);
                Destroy(dropping, 100f);
            }
            if (index >= 70 && index < 95)
            {
                GameObject dropping = (GameObject)Instantiate(drops[2], transform.position + new Vector3(0, 1, 0), transform.rotation);
                Destroy(dropping, 100f);
            }
            if (index >= 95)
            {
                GameObject dropping = (GameObject)Instantiate(drops[3], transform.position + new Vector3(0, 1, 0), transform.rotation);
                Destroy(dropping, 100f);
            }
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= wps.Length-1) // waypoints
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = wps[wavepointIndex]; // waypoint 
        transform.LookAt(target);
    }

    public void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        if (PlayerStats.Lives <= 0)
        {
            gameManager.EndGame();
        }
    }
}
