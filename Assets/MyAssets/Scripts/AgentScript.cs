using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AgentScript : MonoBehaviour
{
    private Transform target;

    [Header("Gameobject To Link")]
    public Transform partToRotateY;
    public Transform partToRotateX;
    public ParticleSystem shootEffect;
    public Animator survivorAnimator;
    public Transform pointFrom;

    [Header("Attributes")]
    public float range = 15f;
    public float clipping = 5f;
    public float fireRate = 1f;
    public float clockingTime = 0.5f;
    private float fireCountdown = 0f;
    [SerializeField] int damageOnHit = 10;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 5f;
    public Transform firePoint;
    public AudioSource shootSound;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, clockingTime);
        shootSound = gameObject.GetComponent<AudioSource>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(pointFrom.transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy >= clipping)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range && shortestDistance >= clipping)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        //ráfordul az enemy-re

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotationY = Quaternion.Lerp(partToRotateY.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotateY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
        Vector3 rotationX = Quaternion.Lerp(partToRotateX.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotateX.rotation = Quaternion.Euler(rotationX.x, rotationY.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            survivorAnimator.Play("Shoot");
            shootEffect.Play();
            shootSound.Play();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damageOnHit);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointFrom.transform.position, range);
        Gizmos.color = Color.red;
    }
}
