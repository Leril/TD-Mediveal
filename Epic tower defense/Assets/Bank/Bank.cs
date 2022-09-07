using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;

    private int _currentBalance;
    public int CurrentBalance => _currentBalance;

    private void Awake()
    {
        _currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    { 
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
