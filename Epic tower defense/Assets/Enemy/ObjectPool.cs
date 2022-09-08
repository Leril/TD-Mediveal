using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField][Range(0.1f, 30f)] private float enemyInterval;
    [SerializeField][Range(0, 50)] private int poolSize;
    [SerializeField] private GameObject enemyPrefab;


    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        _pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            _pool[i] = Instantiate(enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(InstantiateEnemies());
    }

    private IEnumerator InstantiateEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(enemyInterval);
        }
    }

    void EnableObjectInPool()
    {
        foreach (var enemy in _pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }
}
