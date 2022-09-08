using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    
    [Tooltip("Adds amount to max hit points when enemy dies")]
    [SerializeField] private int difficultyRamp = 1;

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
        maxHitPoints += difficultyRamp;
        gameObject.SetActive(false);
        _enemy.RewardGold();
    }
}
