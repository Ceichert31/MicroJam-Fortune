using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;

public class RandomStat : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public Button randomNum1, randomNum2, randomNum3, randomNum4, health, movementSpeed, attackDamage, energy;
    public PlayerBaseStats playerBaseStats = new PlayerBaseStats();
    public PlayerStats playerStats = new PlayerStats();

    public static List<int> randomNum = new List<int>();
    System.Random random = new System.Random();
    public int storeNum;
    string text;

    void Start()
    {
        health.GetComponentInChildren<TextMeshProUGUI>().text = playerStats.maxHealth.ToString();
        movementSpeed.GetComponentInChildren<TextMeshProUGUI>().text = playerStats.movementSpeed.ToString();
        attackDamage.GetComponentInChildren<TextMeshProUGUI>().text = playerStats.attackDamage.ToString();
        energy.GetComponentInChildren<TextMeshProUGUI>().text = playerStats.maxEnergy.ToString();

        for (int i = 0; i < 4; i++)
        {
            int randomNumber = random.Next(-1, 3);
            randomNum.Add(randomNumber);
        }

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum1.GetComponentInChildren<TextMeshProUGUI>().text = text;
                    break;
                case 1:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum2.GetComponentInChildren<TextMeshProUGUI>().text = text;
                    break;
                case 2:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum3.GetComponentInChildren<TextMeshProUGUI>().text = text;
                    break;
                case 3:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum4.GetComponentInChildren<TextMeshProUGUI>().text = text;
                    break;
            }

        }
    }

    private void FixedUpdate()
    {
        randomNum1.onClick.Invoke();
        randomNum2.onClick.Invoke();
        randomNum3.onClick.Invoke();
        randomNum4.onClick.Invoke();
    }

    public int ListAccess(int button)
    {
        return 1;
    }
}
