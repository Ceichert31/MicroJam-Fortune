using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening.Core.Easing;

public class RandomStat : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private MonoScript storeNumber;
    [SerializeField] public Button randomNum1, randomNum2, randomNum3, randomNum4, health, movementSpeed, attackDamage, energy;
    [SerializeField] public TextMeshProUGUI randomNum1Text, randomNum2Text, randomNum3Text, randomNum4Text, healthText, movementSpeedText, attackDamageText, energyText;

    public static List<int> randomNum = new List<int>();
    System.Random random = new System.Random();
    private Button selectedButton;
    public int storeNum;

    void Start()
    {
        randomNum1.onClick.AddListener(() => OnButtonClickRandNum(randomNum1));
        randomNum2.onClick.AddListener(() => OnButtonClickRandNum(randomNum2));
        randomNum3.onClick.AddListener(() => OnButtonClickRandNum(randomNum3));
        randomNum4.onClick.AddListener(() => OnButtonClickRandNum(randomNum4));

        health.onClick.AddListener(() => OnButtonClickStats(health));
        movementSpeed.onClick.AddListener(() => OnButtonClickStats(movementSpeed));
        attackDamage.onClick.AddListener(() => OnButtonClickStats(attackDamage));
        energy.onClick.AddListener(() => OnButtonClickStats(energy));

        healthText.text = GameManager.Instance.MaxHealth.ToString();
        movementSpeedText.text = GameManager.Instance.MovementSpeed.ToString();
        attackDamageText.text = GameManager.Instance.AttackDamage.ToString();
        energyText.text = GameManager.Instance.MaxEnergy.ToString();

        for (int i = 0; i < 4; i++)
        {
            int randomNumber = random.Next(-1, 3);
            randomNum.Add(randomNumber);
        }

        randomNum1Text.text = randomNum[0].ToString();
        randomNum2Text.text = randomNum[1].ToString();
        randomNum3Text.text = randomNum[2].ToString();
        randomNum4Text.text = randomNum[3].ToString();
    }

    private void FixedUpdate()
    {

    }

    public void OnButtonClickRandNum(Button clickedButton)
    {
        selectedButton = clickedButton;

        if (selectedButton == randomNum1)
        {
            randomNum1.interactable = false;
            storeNum = randomNum[0];
        }
        else if (selectedButton == randomNum2)
        {
            randomNum2.interactable = false;
            storeNum = randomNum[1];
        }
        else if (selectedButton == randomNum3)
        {
            randomNum3.interactable = false;
            storeNum = randomNum[2];
        }
        else if (selectedButton == randomNum4)
        {
            randomNum4.interactable = false;
            storeNum = randomNum[3];
        }

        Debug.Log("Selected random number: " + storeNum);
    }

    public void OnButtonClickStats(Button clickedButton)
    {
        selectedButton = clickedButton;

        if (selectedButton == health)
        {
            GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.Health, storeNum);
            healthText.text = GameManager.Instance.MaxHealth.ToString();
            health.interactable = false;
        }
        else if (selectedButton == movementSpeed)
        {
            GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.MovementSpeed, storeNum);
            movementSpeedText.text = GameManager.Instance.MovementSpeed.ToString();
            movementSpeed.interactable = false;
        }
        else if (selectedButton == attackDamage)
        {
            GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.AttackDamage, storeNum);
            attackDamageText.text = GameManager.Instance.AttackDamage.ToString();
            attackDamage.interactable = false; 
        }
        else if (selectedButton == energy)
        {
            GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.Energy, storeNum);
            energyText.text = GameManager.Instance.MaxEnergy.ToString();
            energy.interactable = false;
        }
        storeNum = 0;

        if (health.interactable == false && movementSpeed.interactable == false && attackDamage.interactable == false && energy.interactable == false)
        {
            canvas.enabled = false;
        }
    }

    public int ListAccess(int button)
    {
        return 1;
    }
}
