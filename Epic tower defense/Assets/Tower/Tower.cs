using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
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
