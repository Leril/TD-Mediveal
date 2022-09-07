using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;

    private int _currentHitPoints;

    private Enemy _enemy; 
    
    void OnEnable()
    {
        _currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;

        if (_currentHitPoints == 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        gameObject.SetActive(false);
        _enemy.RewardGold();
    }
}
