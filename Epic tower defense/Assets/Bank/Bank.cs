using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private TMP_Text displayBalance;
    

    private int _currentBalance;
    public int CurrentBalance => _currentBalance;

    private void Awake()
    {
        _currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    { 
        _currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
        
        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + _currentBalance.ToString();
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
