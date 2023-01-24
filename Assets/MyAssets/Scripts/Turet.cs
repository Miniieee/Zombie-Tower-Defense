using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;
using System;

[BurstCompile]
public class Turet : MonoBehaviour
{
    private Transform target;

    [Header("Gameobject To Link")]
    public Transform partToRotate;
    public ParticleSystem shootEffect;
    public ParticleSystem shootEffect2;

    [Header("UI")]
    public Canvas ammoCanvas;
    public Image ammoBar;
    public TextMeshProUGUI ammoText;
    public Camera TDCamera;

    [Header("Attributes")]
    public int startAmmo = 100;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField] int damageOnHit = 10;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform firePoint;
    public float cameraScale = 20f;
    public AudioSource shootSound;

    private float fixedScale = 0.00015f;
    public int ammo;
    private float ammoFloat;
    private float startAmmoFloat;

    void Start()
    {
        ammo = startAmmo;
        ammoFloat = ammo;
        startAmmoFloat = startAmmo;
        InvokeRepeating("Scale", 0, 0.2f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        AmmoStatus();
        shootSound = gameObject.GetComponent<AudioSource>();
    }

    private void Scale()
    {
        var distance = (TDCamera.transform.position - ammoCanvas.transform.position).magnitude;
        var size = distance * fixedScale * cameraScale;
        ammoCanvas.transform.localScale = Vector3.one * size;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
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
        
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation,turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (fireCountdown <= 0f && ammo > 0)
        {
            Shoot();
            shootEffect.Play();
            if (shootEffect2 != null)
            {
                shootEffect2.Play();
            }
            ammo--;
            AmmoStatus();
            shootSound.Play();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public void AmmoStatus()
    {
        ammoFloat = ammo;
        ammoBar.fillAmount = ammoFloat / startAmmoFloat;
        ammoText.text = ammo + " / " + startAmmo;
    }

    public void MaxAmmo()
    {
        print("works");
        ammo = startAmmo;
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
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
    }
}
