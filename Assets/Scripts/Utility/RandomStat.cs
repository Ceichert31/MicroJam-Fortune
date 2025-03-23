using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening.Core.Easing;

public class RandomStat : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public Button randomNum1, randomNum2, randomNum3, randomNum4, randomNum5, randomNum6, randomNum7, randomNum8, randomNum9, 
        health, movementSpeed, attackDamage, carryingCapacity, vision, confidence, luck, swag, agility;
    [SerializeField] public TextMeshProUGUI randomNum1Text, randomNum2Text, randomNum3Text, randomNum4Text, randomNum5Text, randomNum6Text, randomNum7Text, randomNum8Text, randomNum9Text, 
        healthText, movementSpeedText, attackDamageText, carryingCapacityText, visionText, confidenceText, luckText, swagText, agilityText;

    public static List<int> randomNum = new List<int>();
    System.Random random = new System.Random();
    private Button selectedButton;
    public int storeNum, buttonDisabledCount;
    public bool allButtonsDisabled = false, randomStatClicked = false;

    [SerializeField] private SceneSender sceneSender;

    void Start()
    {
        randomNum1.onClick.AddListener(() => OnButtonClickRandNum(randomNum1));
        randomNum2.onClick.AddListener(() => OnButtonClickRandNum(randomNum2));
        randomNum3.onClick.AddListener(() => OnButtonClickRandNum(randomNum3));
        randomNum4.onClick.AddListener(() => OnButtonClickRandNum(randomNum4));
        randomNum5.onClick.AddListener(() => OnButtonClickRandNum(randomNum5));
        randomNum6.onClick.AddListener(() => OnButtonClickRandNum(randomNum6));
        randomNum7.onClick.AddListener(() => OnButtonClickRandNum(randomNum7));
        randomNum8.onClick.AddListener(() => OnButtonClickRandNum(randomNum8));
        randomNum9.onClick.AddListener(() => OnButtonClickRandNum(randomNum9));

        int randomNumber = random.Next(-3,0);

        GameManager.Instance.UpdateSideStat(GameManager.SideStats.Vision, randomNumber);
        randomNumber = random.Next(-3, 0);
        GameManager.Instance.UpdateSideStat(GameManager.SideStats.Confidence, randomNumber);
        randomNumber = random.Next(-3, 0);
        GameManager.Instance.UpdateSideStat(GameManager.SideStats.Luck, randomNumber);
        randomNumber = random.Next(0, 3);
        GameManager.Instance.UpdateSideStat(GameManager.SideStats.Swag, randomNumber);
        randomNumber = random.Next(-1, 2);
        GameManager.Instance.UpdateSideStat(GameManager.SideStats.Agility, randomNumber);

        health.onClick.AddListener(() => OnButtonClickStats(health));
        movementSpeed.onClick.AddListener(() => OnButtonClickStats(movementSpeed));
        attackDamage.onClick.AddListener(() => OnButtonClickStats(attackDamage));
        carryingCapacity.onClick.AddListener(() => OnButtonClickStats(carryingCapacity));
        vision.onClick.AddListener(() => OnButtonClickStats(vision));
        confidence.onClick.AddListener(() => OnButtonClickStats(confidence));
        luck.onClick.AddListener(() => OnButtonClickStats(luck));
        swag.onClick.AddListener(() => OnButtonClickStats(swag));
        agility.onClick.AddListener(() => OnButtonClickStats(agility));

        healthText.text = GameManager.Instance.MaxHealth.ToString();
        movementSpeedText.text = GameManager.Instance.MovementSpeed.ToString();
        attackDamageText.text = GameManager.Instance.AttackDamage.ToString();
        carryingCapacityText.text = GameManager.Instance.CarryingCapacity.ToString();
        visionText.text = GameManager.Instance.Vision.ToString();
        confidenceText.text = GameManager.Instance.Confidence.ToString();
        luckText.text = GameManager.Instance.Luck.ToString();
        swagText.text = GameManager.Instance.Swag.ToString();
        agilityText.text = GameManager.Instance.Agility.ToString();

        for (int i = 0; i < 10; i++)
        {
            randomNumber = random.Next(-1, 5);
            randomNum.Add(randomNumber);
        }

        randomNum1Text.text = randomNum[0].ToString();
        randomNum2Text.text = randomNum[1].ToString();
        randomNum3Text.text = randomNum[2].ToString();
        randomNum4Text.text = randomNum[3].ToString();
        randomNum5Text.text = randomNum[4].ToString();
        randomNum6Text.text = randomNum[5].ToString();
        randomNum7Text.text = randomNum[6].ToString();
        randomNum8Text.text = randomNum[7].ToString();
        randomNum9Text.text = randomNum[8].ToString();
    }

    private void Update()
    {
        if (allButtonsDisabled)
        {
            sceneSender.SendScene();
        }
    }
    private void FixedUpdate()
    {
        if (buttonDisabledCount == 18) 
        {
            allButtonsDisabled = true;
            canvas.enabled = false;
        }
    }

    public void OnButtonClickRandNum(Button clickedButton)
    {
        selectedButton = clickedButton;

        if (randomStatClicked == false)
        {
            if (selectedButton == randomNum1)
            {
                randomNum1.interactable = false;
                storeNum = randomNum[0];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum2)
            {
                randomNum2.interactable = false;
                storeNum = randomNum[1];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum3)
            {
                randomNum3.interactable = false;
                storeNum = randomNum[2];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum4)
            {
                randomNum4.interactable = false;
                storeNum = randomNum[3];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum5)
            {
                randomNum5.interactable = false;
                storeNum = randomNum[4];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum6)
            {
                randomNum6.interactable = false;
                storeNum = randomNum[5];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum7)
            {
                randomNum7.interactable = false;
                storeNum = randomNum[6];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum8)
            {
                randomNum8.interactable = false;
                storeNum = randomNum[7];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
            else if (selectedButton == randomNum9)
            {
                randomNum9.interactable = false;
                storeNum = randomNum[8];
                buttonDisabledCount++;
                randomStatClicked = true;
            }
        }
    }

    public void OnButtonClickStats(Button clickedButton)
    {
        selectedButton = clickedButton;

        if (randomStatClicked == true)
        {
            if (selectedButton == health)
            {
                GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.Health, storeNum);
                healthText.text = GameManager.Instance.MaxHealth.ToString();
                health.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == movementSpeed)
            {
                GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.MovementSpeed, storeNum);
                movementSpeedText.text = GameManager.Instance.MovementSpeed.ToString();
                movementSpeed.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == attackDamage)
            {
                GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.AttackDamage, storeNum);
                attackDamageText.text = GameManager.Instance.AttackDamage.ToString();
                attackDamage.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == carryingCapacity)
            {
                GameManager.Instance.UpdateCoreStat(GameManager.CoreStats.CarryingCapacity, storeNum);
                carryingCapacityText.text = GameManager.Instance.CarryingCapacity.ToString();
                carryingCapacity.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == vision)
            {
                GameManager.Instance.UpdateSideStat(GameManager.SideStats.Vision, storeNum);
                visionText.text = GameManager.Instance.Vision.ToString();
                vision.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == confidence)
            {
                GameManager.Instance.UpdateSideStat(GameManager.SideStats.Confidence, storeNum);
                confidenceText.text = GameManager.Instance.Confidence.ToString();
                confidence.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == luck)
            {
                GameManager.Instance.UpdateSideStat(GameManager.SideStats.Luck, storeNum);
                luckText.text = GameManager.Instance.Luck.ToString();
                luck.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == swag)
            {
                GameManager.Instance.UpdateSideStat(GameManager.SideStats.Swag, storeNum);
                swagText.text = GameManager.Instance.Swag.ToString();
                swag.interactable = false;
                buttonDisabledCount++;
            }
            else if (selectedButton == agility)
            {
                GameManager.Instance.UpdateSideStat(GameManager.SideStats.Agility, storeNum);
                agilityText.text = GameManager.Instance.Agility.ToString();
                agility.interactable = false;
                buttonDisabledCount++;
            }
        }
        storeNum = 0;
        randomStatClicked = false;
    }
}
