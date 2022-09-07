using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform target;
    [SerializeField] private float range = 15f;
    [SerializeField] private ParticleSystem arrows;

    private void Update()
    {
        FindClosestTarget();
        AinWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    private void AinWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        
        Attack(targetDistance < range);
        
        weapon.LookAt(target);
    }

    private void Attack(bool isInRange)
    {
        var emissionModule = arrows.emission;
        emissionModule.enabled = isInRange;
    }
}
