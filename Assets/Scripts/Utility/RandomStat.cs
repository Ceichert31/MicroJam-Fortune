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

public class RandomStat : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private MonoScript storeNumber;
    [SerializeField] public Button randomNum1, randomNum2, randomNum3, randomNum4, health, movementSpeed, attackDamage, energy;
    [SerializeField] public TextMeshProUGUI randomNum1Text, randomNum2Text, randomNum3Text, randomNum4Text, healthText, movementSpeedText, attackDamageText, energyText;

    public static List<int> randomNum = new List<int>();
    System.Random random = new System.Random();
    public Button button;
    public int storeNum;
    string text;

    void Start()
    {
        healthText.text = GameManager.Instance.MaxHealth.ToString();
        movementSpeedText.text = GameManager.Instance.MovementSpeed.ToString();
        attackDamageText.text = GameManager.Instance.AttackDamage.ToString();
        energyText.text = GameManager.Instance.MaxEnergy.ToString();

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
                    randomNum1Text.text = text;
                    break;
                case 1:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum2Text.text = text;
                    break;
                case 2:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum3Text.text = text;
                    break;
                case 3:
                    text = string.Join("", randomNum.ElementAt(i));
                    randomNum4Text.text = text;
                    break;
            }

        }
    }

    private void FixedUpdate()
    {
        randomNum1.onClick.AddListener(OnButtonClick);
        randomNum2.onClick.AddListener(OnButtonClick);
        randomNum3.onClick.AddListener(OnButtonClick);
        randomNum4.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        button = GetComponent<Button>();
        storeNum = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    public int ListAccess(int button)
    {
        return 1;
    }
}
