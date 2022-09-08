using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    [SerializeField] private float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandchild in child)
            { 
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower towerPrefab, Vector3 transformPosition)
    {
        var bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(towerPrefab.gameObject, transformPosition, Quaternion.identity);
            bank.Withdraw(cost);
            return true;   
        }

        return false;
    }
}
