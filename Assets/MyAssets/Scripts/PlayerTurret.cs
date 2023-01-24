using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class PlayerTurret : MonoBehaviour
{
    private Transform target;

    [Header("Gameobject To Link")]
    public Transform partToRotate;
    public Weapon weapon;

    [Header("Attributes")]

    public float range = 30f;
    private float fireRate;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    void Start()
    {
        if (FindObjectOfType<Weapon>().gameObject.activeSelf)
        {
            weapon = GetComponentInChildren<Weapon>();
        }
        fireRate = PlayerPrefs.GetFloat("FireRate", 1);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            weapon.Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
}
